using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Presentation.Controls;

public class SpaceCard : Button
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(SpaceCard));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(Geometry),
            typeof(SpaceCard));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IsWIPProperty =
        DependencyProperty.Register(
            nameof(IsWIP),
            typeof(bool),
            typeof(SpaceCard));

    public bool IsWIP
    {
        get => (bool)GetValue(IsWIPProperty);
        set => SetValue(IsWIPProperty, value);
    }
}