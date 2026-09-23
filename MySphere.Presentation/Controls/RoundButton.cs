using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MySphere.Presentation.Controls;

public class RoundButton : Button
{
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(RoundButton));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}