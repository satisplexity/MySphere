using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Presentation.Controls;

public class NavigationButton : RadioButton
{
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(Geometry),
        typeof(NavigationButton));

    public Geometry? Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty HighlightingColorProperty = DependencyProperty.Register(
        nameof(HighlightingColor),
        typeof(Color),
        typeof(NavigationButton));

    public Color? HighlightingColor
    {
        get => (Color)GetValue(HighlightingColorProperty);
        set => SetValue(HighlightingColorProperty, value);
    }
}