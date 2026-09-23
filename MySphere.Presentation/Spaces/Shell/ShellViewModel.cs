using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Spaces.Hub;
using MySphere.Presentation.Spaces.Shell.Navigation;
using System.Windows;

namespace MySphere.Presentation.Spaces.Shell;

public sealed class ShellViewModel : ViewModelBase
{
    public IShellNavigationService Navigation { get; }

    public AsyncRelayCommand ToggleSpaceSwitcherCommand { get; }

    public RelayCommand ShutdownCommand { get; }

    public RelayCommand CollapseCommand { get; }

    public ShellViewModel(IShellNavigationService navigation)
    {
        Navigation = navigation;

        Navigation.NavigateTo<HubViewModel>();

        ShutdownCommand = new(_ =>
        {
            Application.Current.Shutdown();
        });

        CollapseCommand = new(_ =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });
    }
}