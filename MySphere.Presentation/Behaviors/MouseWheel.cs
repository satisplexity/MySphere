using System.Windows.Input;
using System.Windows;

namespace MySphere.Presentation.Behaviors;

public static class MouseWheel
{
    #region PreviousCommand

    public static readonly DependencyProperty PreviousCommandProperty =
    DependencyProperty.RegisterAttached(
        "PreviousCommand",
        typeof(ICommand),
        typeof(MouseWheel),
        new PropertyMetadata(null, OnCommandChanged));

    public static void SetPreviousCommand(DependencyObject element, ICommand? value) => 
        element.SetValue(PreviousCommandProperty, value);

    public static ICommand? GetPreviousCommand(DependencyObject element) => 
        (ICommand?)element.GetValue(PreviousCommandProperty);

    #endregion

    #region NextCommand

    public static readonly DependencyProperty NextCommandProperty =
        DependencyProperty.RegisterAttached(
            "NextCommand",
            typeof(ICommand),
            typeof(MouseWheel),
            new PropertyMetadata(null, OnCommandChanged));

    public static void SetNextCommand(DependencyObject element, ICommand? value) => 
        element.SetValue(NextCommandProperty, value);

    public static ICommand? GetNextCommand(DependencyObject element) =>
        (ICommand?)element.GetValue(NextCommandProperty);

    #endregion

    private static void OnCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is not UIElement element)
            return;

        // Removing the handler to avoid double subscription
        element.PreviewMouseWheel -= OnMouseWheel;

        if (GetPreviousCommand(element) is not null || GetNextCommand(element) is not null)
            element.PreviewMouseWheel += OnMouseWheel;
    }

    private static void OnMouseWheel(object sender, MouseWheelEventArgs wheel)
    {
        if (sender is not UIElement element)
            return;

        ICommand? command = wheel.Delta > 0
            ? GetPreviousCommand(element)
            : GetNextCommand(element);

        if (command?.CanExecute(null) != true)
            return;

        command.Execute(null);
        wheel.Handled = true;
    }
}