using MySphere.Framework.Foundation.Navigation;

namespace MySphere.Presentation.Hub.Navigation;

public sealed class HubNavigationService 
    : NavigationService, IHubNavigationService
{
    public HubNavigationService(HubNavigationStore store, IServiceProvider serviceProvider) 
        : base(store, serviceProvider) { }
}