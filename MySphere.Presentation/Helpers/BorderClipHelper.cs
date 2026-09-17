using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Presentation.Helpers;

public static class BorderClipHelper
{
    public static readonly DependencyProperty EnableProperty =
        DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(BorderClipHelper),
            new PropertyMetadata(false, OnEnableChanged));

    public static void SetEnable(DependencyObject element, bool value)
        => element.SetValue(EnableProperty, value);

    public static bool GetEnable(DependencyObject element)
        => (bool)element.GetValue(EnableProperty);

    private static void OnEnableChanged(
        DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject is not Border border)
            return;

        if ((bool)args.NewValue)
        {
            border.SizeChanged += Border_SizeChanged;
            border.Loaded += Border_Loaded;

            UpdateClip(border);
        }
        else
        {
            border.SizeChanged -= Border_SizeChanged;
            border.Loaded -= Border_Loaded;

            border.Clip = null;
        }
    }

    private static void Border_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateClip((Border)sender);
    }

    private static void Border_SizeChanged(object sender,SizeChangedEventArgs e)
    {
        UpdateClip((Border)sender);
    }

    private static void UpdateClip(Border border)
    {
        double width = border.ActualWidth;
        double height = border.ActualHeight;

        if (width <= 0 || height <= 0)
        {
            border.Clip = null;
            return;
        }

        CornerRadius radius = border.CornerRadius;

        radius.TopLeft = Math.Min(radius.TopLeft, Math.Min(width, height) / 2);
        radius.TopRight = Math.Min(radius.TopRight, Math.Min(width, height) / 2);
        radius.BottomRight = Math.Min(radius.BottomRight, Math.Min(width, height) / 2);
        radius.BottomLeft = Math.Min(radius.BottomLeft, Math.Min(width, height) / 2);

        border.Clip = CreateGeometry(
            width,
            height,
            radius);
    }

    private static StreamGeometry CreateGeometry(
        double width,
        double height,
        CornerRadius radius)
    {
        StreamGeometry geometry = new();

        using StreamGeometryContext ctx = geometry.Open();

        ctx.BeginFigure(
            new Point(radius.TopLeft, 0),
            true,
            true);

        // Top
        ctx.LineTo(
            new Point(width - radius.TopRight, 0),
            true,
            false);

        // Top-right
        ctx.ArcTo(
            new Point(width, radius.TopRight),
            new Size(radius.TopRight, radius.TopRight),
            0,
            false,
            SweepDirection.Clockwise,
            true,
            false);

        // Right
        ctx.LineTo(
            new Point(width, height - radius.BottomRight),
            true,
            false);

        // Bottom-right
        ctx.ArcTo(
            new Point(width - radius.BottomRight, height),
            new Size(radius.BottomRight, radius.BottomRight),
            0,
            false,
            SweepDirection.Clockwise,
            true,
            false);

        // Bottom
        ctx.LineTo(
            new Point(radius.BottomLeft, height),
            true,
            false);

        // Bottom-left
        ctx.ArcTo(
            new Point(0, height - radius.BottomLeft),
            new Size(radius.BottomLeft, radius.BottomLeft),
            0,
            false,
            SweepDirection.Clockwise,
            true,
            false);

        // Left
        ctx.LineTo(
            new Point(0, radius.TopLeft),
            true,
            false);

        // Top-left
        ctx.ArcTo(
            new Point(radius.TopLeft, 0),
            new Size(radius.TopLeft, radius.TopLeft),
            0,
            false,
            SweepDirection.Clockwise,
            true,
            false);

        geometry.Freeze();

        return geometry;
    }
}
