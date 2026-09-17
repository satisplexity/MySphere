using MySphere.Presentation.Framework.Foundation;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

using System.Windows.Media.Imaging;

namespace MySphere.Presentation.Spaces.Shell;

public partial class ShellWindow : Window
{
    public ShellWindow()
    {
        InitializeComponent();
    }

    public void CaptureCurrentSpace()
    {
        PART_SpaceSwitcher.Capture(RenderElement(SpaceContent), (ViewModelBase)PART_Content.Content);

        PART_Container.BeginAnimation(OpacityProperty, new DoubleAnimation()
        {
            From = 0.1,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.3)
        });

        PART_ContainerScale.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation()
        {
            From = 0.8,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseOut,
                Power = 2
            }
        });

        PART_ContainerScale.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation()
        {
            From = 0.8,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.3),
            EasingFunction = new PowerEase()
            {
                EasingMode = EasingMode.EaseOut,
                Power = 2
            }
        });
    }

    public void ReplaceWithTo(ViewModelBase viewModel)
    {
        ShellViewModel shellViewModel = (ShellViewModel)DataContext;

        shellViewModel.Navigation.ReplaceWith(viewModel);
    }

    public BitmapSource RenderElement(FrameworkElement element)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        //element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        //element.Arrange(new Rect(element.DesiredSize));
        //element.UpdateLayout();

        int width = (int)Math.Ceiling(element.ActualWidth);
        int height = (int)Math.Ceiling(element.ActualHeight);

        if (width <= 0 || height <= 0)
            throw new InvalidOperationException("Element has invalid size.");

        var bitmap = new RenderTargetBitmap(
            width,
            height,
            96,
            96,
            PixelFormats.Pbgra32);

        bitmap.Render(element);
        bitmap.Freeze();

        return bitmap;
    }

    private async void ToogleSwitcher(object sender, RoutedEventArgs e)
    {
        await ShowSwitcher();
    }

    private async Task ShowSwitcher()
    {
        PART_SpaceSwitcher.Toogle(RenderElement(SpaceContent), (ViewModelBase)PART_Content.Content);
    }

    private async void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if(Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.Space)
        {
            await ShowSwitcher();
        }
    }
}