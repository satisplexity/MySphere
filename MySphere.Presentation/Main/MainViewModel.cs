using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main.Navigation;
using MySphere.Presentation.Hub;

namespace MySphere.Presentation.Main;

public sealed class MainViewModel : ViewModelBase
{
    public IMainNavigationService Navigation { get; }

    public MainViewModel(IMainNavigationService navigation)
    {
        Navigation = navigation;

        Navigation.NavigateTo<HubViewModel>();
    }
}