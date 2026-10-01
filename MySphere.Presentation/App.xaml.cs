using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using MySphere.Presentation.Shell;
using MySphere.Presentation.Main;
using MySphere.Presentation.Hub;

using MySphere.Presentation.Shell.Navigation;
using MySphere.Presentation.Main.Navigation;
using MySphere.Presentation.Hub.Navigation;
using MySphere.Presentation.Hub.Sections.Home;
using MySphere.Presentation.Hub.Sections.Spaces;
using MySphere.Presentation.Hub.Sections.Account;
using MySphere.Presentation.Hub.Sections.Settings;
using MySphere.Semigraph.Presentation;
using MySphere.Presentation.SpaceSwitcher;

namespace MySphere.Presentation;

public partial class App 
    : Application
{
    private IServiceProvider _serviceProvider = null!;

    protected override void OnStartup(StartupEventArgs args)
    {
        base.OnStartup(args);

        ServiceCollection services = new();

        Configure(services);

        _serviceProvider = services.BuildServiceProvider();

        _serviceProvider.GetRequiredService<ShellWindow>().Show();
    }

    protected override void OnExit(ExitEventArgs args)
    {
        if (_serviceProvider is IDisposable disposable)
            disposable.Dispose();

        base.OnExit(args);
    }

    private void Configure(ServiceCollection services)
    {
        services.AddSingleton<SpaceHostManager>();
        services.AddSingleton<SpaceSwitcherViewModel>();
        services.AddSingleton<SpaceSwitcherView>();

        ConfigureShell(services);
        ConfigureMain(services);
        ConfigureHub(services);

        services.AddTransient<SemigraphViewModel>();
        services.AddTransient<SemigraphView>();
    }

    private void ConfigureShell(ServiceCollection services)
    {
        services.AddSingleton<ShellNavigationStore>();
        services.AddSingleton<IShellNavigationService, ShellNavigationService>();

        services.AddSingleton<ShellViewModel>();

        services.AddSingleton<ShellWindow>(serviceProvider =>
            new ShellWindow()
            {
                DataContext = serviceProvider.GetRequiredService<ShellViewModel>()
            });
    }

    private void ConfigureMain(ServiceCollection services)
    {
        services.AddSingleton<MainNavigationStore>();
        services.AddSingleton<IMainNavigationService, MainNavigationService>();

        services.AddSingleton<MainViewModel>();

        services.AddTransient<MainView>();
    }

    private void ConfigureHub(ServiceCollection services)
    {
        services.AddSingleton<HubNavigationStore>();
        services.AddSingleton<IHubNavigationService, HubNavigationService>();

        services.AddSingleton<HubViewModel>();

        services.AddTransient<HubView>();

        services.AddSingleton<HomeViewModel>();
        services.AddTransient<HomeView>();

        services.AddSingleton<SpacesViewModel>();
        services.AddTransient<HomeView>();

        services.AddSingleton<AccountViewModel>();
        services.AddTransient<AccountView>();

        services.AddSingleton<SettingsViewModel>();
        services.AddTransient<SettingsView>();
    }
}