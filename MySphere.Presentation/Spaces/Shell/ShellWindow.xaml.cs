using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Utilities;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace MySphere.Presentation.Spaces.Shell;

public partial class ShellWindow : Window
{
    public ShellWindow() => InitializeComponent();

    public void CaptureCurrentSpace()
    {
        PART_SpaceSwitcher.Capture(BitmapRenderer.Render(SpaceContent), (ViewModelBase)PART_Content.Content);

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

    private async void ToogleSwitcher(object sender, RoutedEventArgs e)
    {
        await ShowSwitcher();
    }

    private async Task ShowSwitcher()
    {
        PART_SpaceSwitcher.Toogle(BitmapRenderer.Render(SpaceContent), (ViewModelBase)PART_Content.Content);
    }

    private async void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if(Keyboard.IsKeyDown(Key.LeftCtrl) && e.Key == Key.Space)
        {
            await ShowSwitcher();
        }
    }
}