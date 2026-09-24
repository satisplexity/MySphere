using MySphere.Presentation.Framework.Navigation;

namespace MySphere.Presentation.Spaces.Semigraph.Navigation;

public interface ISemigraphNavigationService : INavigationService { }

public class SemigraphNavigationService : NavigationService, ISemigraphNavigationService
{
    public SemigraphNavigationService(SemigraphNavigationStore store, IServiceProvider services) : base(store, services) { }
}
