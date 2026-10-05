using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MySphere.Framework.Controls;

public sealed class PlanetSystemAnimation : Button
{
    public static readonly DependencyProperty InfoProperty =
        DependencyProperty.Register(
            nameof(Info),
            typeof(string),
            typeof(PlanetSystemAnimation));

    public string Info
    {
        get => (string)GetValue(InfoProperty);
        set => SetValue(InfoProperty, value);
    }

    public static readonly DependencyProperty PlanetsProperty =
        DependencyProperty.Register(
            nameof(Planets),
            typeof(ObservableCollection<Ellipse>),
            typeof(PlanetSystemAnimation));

    public ObservableCollection<Ellipse> Planets
    {
        get => (ObservableCollection<Ellipse>)GetValue(PlanetsProperty);
        set => SetValue(PlanetsProperty, value);
    }
}