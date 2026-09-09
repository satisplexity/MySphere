using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MySphere.Presentation.Controls;

public class NavigationButton : RadioButton
{
    public static readonly DependencyProperty IconDataProperty = DependencyProperty.Register(
        nameof(IconData),
        typeof(Geometry),
        typeof(NavigationButton));

    public Geometry? IconData
    {
        get => (Geometry)GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
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