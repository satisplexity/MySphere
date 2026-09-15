using MySphere.Presentation.Framework.Navigation;

namespace MySphere.Presentation.Spaces.Hub.Navigation;

public interface IHubNavigationService : INavigationService { }

public sealed class HubNavigationService : NavigationService, IHubNavigationService 
{
    public HubNavigationService(HubNavigationStore store, IServiceProvider services) : base (store, services) { }
}
