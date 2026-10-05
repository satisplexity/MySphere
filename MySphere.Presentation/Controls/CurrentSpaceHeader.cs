using MySphere.Framework.Utilities;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;

namespace MySphere.Presentation.Controls;

public class CurrentSpaceHeader : Button
{
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(
            nameof(Icon),
            typeof(Geometry),
            typeof(CurrentSpaceHeader));

    public Geometry Icon
    {
        get => (Geometry)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty SpaceColorProperty =
        DependencyProperty.Register(
            nameof(SpaceColor),
            typeof(LinearGradientBrush),
            typeof(CurrentSpaceHeader));

    public LinearGradientBrush SpaceColor
    {
        get => (LinearGradientBrush)GetValue(SpaceColorProperty);
        set => SetValue(SpaceColorProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(CurrentSpaceHeader),
            new PropertyMetadata("MySphere", OnTextChanged));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty IsSpaceUpdatedProperty =
        DependencyProperty.Register(
            nameof(IsSpaceUpdated),
            typeof(bool),
            typeof(CurrentSpaceHeader));

    public bool IsSpaceUpdated
    {
        get => (bool)GetValue(IsSpaceUpdatedProperty);
        set => SetValue(IsSpaceUpdatedProperty, value);
    }

    private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        Debug.WriteLine("TEXT CHANGEDDDD");

        CurrentSpaceHeader header = (CurrentSpaceHeader)sender;
        header.IsSpaceUpdated = true;

        TimerFactory.Run(() => header.IsSpaceUpdated = false, 0.5);
    }
}