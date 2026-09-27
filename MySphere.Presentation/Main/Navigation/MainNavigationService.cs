using MySphere.Framework.Foundation.Navigation;

namespace MySphere.Presentation.Main.Navigation;

public sealed class MainNavigationService : NavigationService, IMainNavigationService
{
    public MainNavigationService(MainNavigationStore store, IServiceProvider serviceProvider) : base(store, serviceProvider) { }
}