using System.Windows.Input;
using System.Windows;

namespace MySphere.Framework.Behaviors;

public static class LoadedBehavior
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(LoadedBehavior),
            new PropertyMetadata(null, OnCommandChanged));

    public static void SetCommand(DependencyObject element, ICommand value)
        => element.SetValue(CommandProperty, value);

    public static ICommand GetCommand(DependencyObject element)
        => (ICommand)element.GetValue(CommandProperty);

    private static void OnCommandChanged(DependencyObject dependecyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependecyObject is not FrameworkElement element)
            return;

        element.Loaded += (_, _) =>
        {
            ICommand command = GetCommand(element);

            if (command?.CanExecute(null) == true)
                command.Execute(null);
        };
    }
}