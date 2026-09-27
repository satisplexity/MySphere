using MySphere.Framework.Foundation.Navigation;

namespace MySphere.Presentation.Shell.Navigation;

public sealed class ShellNavigationService : NavigationService, IShellNavigationService
{
    public ShellNavigationService(ShellNavigationStore store, IServiceProvider serviceProvider) : base(store, serviceProvider) { } 
}