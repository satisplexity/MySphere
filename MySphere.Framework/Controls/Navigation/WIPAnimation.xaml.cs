using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows;

namespace MySphere.Framework.Controls.Navigation;

public partial class WIPAnimation : UserControl
{
    private readonly Stopwatch _clock = Stopwatch.StartNew();
    private double _lastTime;

    private  double[] _angles = new double[1];
    private  TranslateTransform[] _positions = new TranslateTransform[1];
    private  ScaleTransform[] _scales = new ScaleTransform[1];
    private  Ellipse[] _ellipses = new Ellipse[1];

    private  Point[] _orbits;

    public WIPAnimation()
    {
        InitializeComponent();

        CreateEllipses(3);

        CompositionTarget.Rendering += OnRendering;
    }

    private void CreateEllipses(int ellipses)
    {
        _positions = new TranslateTransform[ellipses];
        _scales = new ScaleTransform[ellipses];
        _angles = new double[ellipses];
        _ellipses = new Ellipse[ellipses];
        _orbits = new Point[ellipses];

        for(int index = 0; index < _angles.Length; index++)
        {
            _orbits[index] = new(200 + 700 / (index + 1), 100 / (index + 1));

            _angles[index] = GetStartAngle(index, ellipses);

            TranslateTransform translate = new();
            ScaleTransform scale = new();

            _positions[index] = translate;
            _scales[index] = scale;

            TransformGroup group = new TransformGroup();

            group.Children.Add(translate);
            group.Children.Add(scale);

            Ellipse ellipse = new Ellipse()
            {
                RenderTransform = group,
                Fill =  PART_CenterEllipse.Fill,
                Width = 50,
                Height = 50,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            _ellipses[index] = ellipse;

            PART_Container.Children.Add(ellipse);
        }
    }

    double GetStartAngle(int index, int count)
    {
        return 2 * Math.PI * index / count;
    } 

    private void OnRendering(object? sender, EventArgs e)
    {
        double currentTime = _clock.Elapsed.TotalSeconds;
        double deltaTime = currentTime - _lastTime;
        _lastTime = currentTime;

        Update(deltaTime);
    }

    private void Update(double deltaTime)
    {
        for(int index = 0; index < _angles.Length; index++)
        {
            _angles[index] += 0.4 * deltaTime;

            Point position = GetOrbitPosition(_angles[index], new Point(0, 0), _orbits[index].X, _orbits[index].Y);

            _positions[index].X = position.X;
            _positions[index].Y = position.Y;

            if(position.Y > 0)
            {
                _ellipses[index].SetValue(Panel.ZIndexProperty, 100);
            }
            else
            {
                _ellipses[index].SetValue(Panel.ZIndexProperty, 0);
            }

            double depth = (Math.Sin(_angles[index]) + 1) / 2;

            double scale = 0.6 + depth * 0.4;

            _scales[index].ScaleX = _scales[index].ScaleY = scale;
        }
    }

    private static Point GetOrbitPosition(double angle, Point center, double radiusX, double radiusY)
    {
        return new Point(center.X + radiusX * Math.Cos(angle), center.Y + radiusY * Math.Sin(angle));
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        PART_CenterEllipse.Width = this.ActualHeight / 4;
        PART_CenterEllipse.Height = this.ActualHeight / 4;
    }
}