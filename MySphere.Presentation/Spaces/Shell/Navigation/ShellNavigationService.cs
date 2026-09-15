using MySphere.Presentation.Framework.Navigation;

namespace MySphere.Presentation.Spaces.Shell.Navigation;

public sealed class ShellNavigationService : NavigationService, IShellNavigationService
{
    public ShellNavigationService(ShellNavigationStore store, IServiceProvider services) : base (store, services) { }
}