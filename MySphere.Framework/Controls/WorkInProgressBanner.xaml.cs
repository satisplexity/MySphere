using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows;

namespace MySphere.Framework.Controls;

public partial class WorkInProgressBanner : UserControl
{
    private const int ELLIPSES_COUNT = 4;
    private const double PLANET_SIZE_DIVIDOR = 4;
    private const double SATELLITE_SIZE_DIVIDOR = 10;

    public string Info
    {
        get => (string)GetValue(InfoProperty);
        set => SetValue(InfoProperty, value);
    }

    public static readonly DependencyProperty InfoProperty =
        DependencyProperty.Register(
            nameof(Info),
            typeof(string),
            typeof(WorkInProgressBanner),
            new PropertyMetadata(string.Empty));

    private readonly Stopwatch _clock = Stopwatch.StartNew();
    private double _lastTime;

    private TranslateTransform[] _positions = new TranslateTransform[ELLIPSES_COUNT];
    private ScaleTransform[] _scales = new ScaleTransform[ELLIPSES_COUNT];
    private Ellipse[] _satellites = new Ellipse[ELLIPSES_COUNT];
    private double[] _angles = new double[ELLIPSES_COUNT];
    private Point[] _orbits = new Point[ELLIPSES_COUNT];

    public WorkInProgressBanner() =>
        InitializeComponent();

    private void Root_Loaded(object sender, RoutedEventArgs e)
    {
        InitializeSatellites();
        
        CompositionTarget.Rendering += OnRendering;
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        double currentTime = _clock.Elapsed.TotalSeconds;
        double deltaTime = currentTime - _lastTime;
        _lastTime = currentTime;

        Update(deltaTime);
    }

    private void InitializeSatellites()
    {
        double planetSize = Area.ActualHeight / PLANET_SIZE_DIVIDOR;

        Planet.Width = Planet.Height = planetSize;

        double satelliteSize = Area.ActualHeight / SATELLITE_SIZE_DIVIDOR;

        double minOrbit = planetSize + satelliteSize;
        double maxOrbit = Area.ActualWidth - minOrbit * 2 - satelliteSize;

        double step = (maxOrbit - minOrbit) / ELLIPSES_COUNT;

        for (int index = 0; index < ELLIPSES_COUNT; index++)
        {
            _orbits[index] = new(minOrbit + step * index, 100 / (index + 1));

            _angles[index] = GetStartAngle(index, ELLIPSES_COUNT);

            TranslateTransform translate = new();
            ScaleTransform scale = new();

            _positions[index] = translate;
            _scales[index] = scale;

            TransformGroup group = new TransformGroup();

            group.Children.Add(translate);
            group.Children.Add(scale);

            Ellipse satellite = new Ellipse()
            {
                RenderTransform = group,
                Fill = Planet.Fill,
                Width = satelliteSize,
                Height = satelliteSize
            };

            _satellites[index] = satellite;

            Area.Children.Add(satellite);
        }
    }

    double GetStartAngle(int index, int count) => 
        2 * Math.PI * index / count;

    private void Update(double deltaTime)
    {
        for (int index = 0; index < ELLIPSES_COUNT; index++)
        {
            _angles[index] += 0.4 * deltaTime;

            Point position = GetOrbitPosition(_angles[index], _orbits[index].X, _orbits[index].Y);

            _positions[index].X = position.X;
            _positions[index].Y = position.Y;

            if (position.Y > 0)
            {
                _satellites[index].SetValue(Panel.ZIndexProperty, 100);
            }
            else
            {
                _satellites[index].SetValue(Panel.ZIndexProperty, 0);
            }

            double depth = (Math.Sin(_angles[index]) + 1) / 2;

            double scale = 0.6 + depth * 0.4;

            _scales[index].ScaleX = _scales[index].ScaleY = scale;
        }
    }

    private static Point GetOrbitPosition(double angle, double radiusX, double radiusY) =>
        new(radiusX * Math.Cos(angle), radiusY * Math.Sin(angle));
}