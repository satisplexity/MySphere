using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MySphere.Framework.Utilities;

namespace MySphere.Framework.Controls
{
    public partial class WorkInProgressAnimationContainer : UserControl
    {
        private readonly SolidColorBrush[] _colors =
        {
            Brushes.LightCoral,
            Brushes.LightBlue,
            Brushes.LightCyan,
            Brushes.LightGreen,
            Brushes.LightPink,
            Brushes.LightYellow,
            Brushes.LightSalmon
        };

        private readonly DispatcherTimer _timer = new()
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        private readonly DoubleAnimation _opacityInAnimation =
            new()
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut}
            };

        private readonly DoubleAnimation _heightInAnimation =
            new()
            {
                To = 20,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut }
            };

        private readonly DoubleAnimation _heightOutAnimation =
            new()
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut }
            };

        private readonly Random _random = new();

        private readonly PowerEase _easingIn =
            new()
            {
                EasingMode = EasingMode.EaseInOut,
                Power = 4
            };

        private readonly List<Border> _borders = new(10);

        private readonly List<int> _tabs = new(10);

        public WorkInProgressAnimationContainer()
        {
            InitializeComponent();

            _timer.Tick += AnimateNextStep;
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void ControlUnloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        private void AnimateNextStep(object? sender, EventArgs e)
        {
            if(_borders.Count > 10)
            {
                _borders.RemoveAt(0);
                _tabs.RemoveAt(0);

                _borders.First().BeginAnimation(HeightProperty, _heightOutAnimation);

                TimerFactory.Run(() => PART_Container.Children.RemoveAt(0), 0.1);
            }

            int tabs = CalculateTab();
            double size = _random.NextDouble() * (500 - tabs * 20) + 20;

            Border border = new()
            {
                Height = 0,
                Width = 20,
                Opacity = 0,
                CornerRadius = new(10),
                Background = _colors[_random.Next(0, _colors.Length - 1)],
                Margin = new(tabs * 20, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            PART_Container.Children.Add(border);
            _borders.Add(border);
            _tabs.Add(tabs);

            border.BeginAnimation(OpacityProperty, _opacityInAnimation);

            border.BeginAnimation(HeightProperty, _heightInAnimation);
            border.BeginAnimation(WidthProperty, new DoubleAnimation()
            {
                To = size,
                Duration = TimeSpan.FromMilliseconds(400),
                EasingFunction = _easingIn
            });
        }

        private int CalculateTab()
        {
            if (_tabs.Count < 5)
                return 0;

            int last = _tabs.Last();

            double zeroChance =
                last == 2 
                ? 0 
                : 0.4;

            double twoChance =
                last == 0
                ? 0
                : 0.4;

            zeroChance /= CalculateInRow(last);
            twoChance /= CalculateInRow(last);

            double random = _random.NextDouble();

            if (zeroChance > random)
                return 0;

            if (twoChance < random)
                return 2;

            return 1;
        }

        private int CalculateInRow(int number)
        {
            for(int index = _tabs.Count - 2; index >= 0; index--)
                if (_tabs[index] != number)
                    return index;

            return 1;
        }
    }
}