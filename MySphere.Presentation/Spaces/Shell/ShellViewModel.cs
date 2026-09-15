using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Spaces.Hub;
using MySphere.Presentation.Spaces.Shell.Navigation;

namespace MySphere.Presentation.Spaces.Shell;

public sealed class ShellViewModel : ViewModelBase
{
    public IShellNavigationService Navigation { get; }

    public AsyncRelayCommand ToggleSpaceSwitcherCommand { get; }

    public ShellViewModel(IShellNavigationService navigation)
    {
        Navigation = navigation;

        Navigation.NavigateTo<HubViewModel>();
    }
}