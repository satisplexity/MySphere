using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MySphere.Framework.Controls.Navigation;

public abstract class NavigationButtonBase : RadioButton
{
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(Geometry),
            typeof(NavigationButtonBase));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}