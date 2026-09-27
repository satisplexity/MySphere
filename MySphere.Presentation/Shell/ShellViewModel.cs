using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Shell.Navigation;
using MySphere.Presentation.Main;

namespace MySphere.Presentation.Shell;

public sealed class ShellViewModel : ViewModelBase
{
    public IShellNavigationService Navigation { get; }

    public ShellViewModel(IShellNavigationService navigation)
    {
        Navigation = navigation;

        Navigation.NavigateTo<MainViewModel>();
    }
}