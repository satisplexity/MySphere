using MySphere.Framework.Foundation.Navigation;

namespace MySphere.Presentation.Shell.Navigation;

public sealed class ShellNavigationService : NavigationService, IShellNavigationService
{
    public ShellNavigationService(ShellNavigationStore store, IServiceProvider services) : base(store, services) { } 
}