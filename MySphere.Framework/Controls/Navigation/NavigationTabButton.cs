using System.Windows;

namespace MySphere.Framework.Controls.Navigation;

public sealed class NavigationTabButton : NavigationButtonBase
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(NavigationTabButton));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}