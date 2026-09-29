using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Presentation.Shell.Controls;

public sealed class WindowControlButton : Button
{
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(Geometry),
            typeof(WindowControlButton));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}