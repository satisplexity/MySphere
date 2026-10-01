using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MySphere.Presentation.Behaviors;

public class MouseWheel
{
    public static readonly DependencyProperty PreviousCommandProperty =
    DependencyProperty.RegisterAttached(
        "PreviousCommand",
        typeof(ICommand),
        typeof(MouseWheel),
        new PropertyMetadata(null, OnCommandChanged));

    public static void SetPreviousCommand(
        DependencyObject element,
        ICommand value)
        => element.SetValue(PreviousCommandProperty, value);

    public static ICommand GetPreviousCommand(
        DependencyObject element)
        => (ICommand)element.GetValue(PreviousCommandProperty);


    public static readonly DependencyProperty NextCommandProperty =
        DependencyProperty.RegisterAttached(
            "NextCommand",
            typeof(ICommand),
            typeof(MouseWheel),
            new PropertyMetadata(null, OnCommandChanged));

    public static void SetNextCommand(
        DependencyObject element,
        ICommand value)
        => element.SetValue(NextCommandProperty, value);

    public static ICommand GetNextCommand(
        DependencyObject element)
        => (ICommand)element.GetValue(NextCommandProperty);


    private static void OnCommandChanged(
        DependencyObject sender,
        DependencyPropertyChangedEventArgs args)
    {
        if (sender is not UIElement element)
            return;

        element.PreviewMouseWheel += OnMouseWheel;
    }

    private static void OnMouseWheel(
        object sender,
        MouseWheelEventArgs args)
    {
        if (sender is not UIElement element)
            return;

        var command = args.Delta > 0
            ? GetPreviousCommand(element)
            : GetNextCommand(element);

        if (command?.CanExecute(null) == true)
            command.Execute(null);

        args.Handled = true;
    }

}
