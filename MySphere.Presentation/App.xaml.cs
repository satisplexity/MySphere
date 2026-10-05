using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using MySphere.Presentation.Shell;
using MySphere.Presentation.Main;

using MySphere.Presentation.Shell.Navigation;
using MySphere.Presentation.Main.Navigation;
using MySphere.Presentation.Hub.Navigation;
using MySphere.Presentation.Hub.Sections.Home;
using MySphere.Presentation.Hub.Sections.Spaces;
using MySphere.Presentation.Hub.Sections.Account;
using MySphere.Presentation.Hub.Sections.Settings;
using MySphere.Presentation.SpaceSwitcher;
using MySphere.Presentation.Hub.Sections.Messages;
using MySphere.Presentation.Hub.Sections.Community;
using MySphere.Presentation.Hub;

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
        
        services.AddTransient<SnakePlusPlus.Presentation.RootViewModel>();
        services.AddTransient<CellautoLab.Presentation.RootViewModel>();
        services.AddTransient<SlideForge.Presentation.RootViewModel>();
        services.AddTransient<ChessSharp.Presentation.RootViewModel>();
        services.AddTransient<FractalLab.Presentation.RootViewModel>();
        services.AddTransient<KittyCatch.Presentation.RootViewModel>();
        services.AddTransient<Semigraph.Presentation.RootViewModel>();
        services.AddTransient<CodeScope.Presentation.RootViewModel>();
        services.AddTransient<Mergery3d.Presentation.RootViewModel>();
        services.AddTransient<Weatherly.Presentation.RootViewModel>();
        services.AddTransient<HexaCards.Presentation.RootViewModel>();
        services.AddTransient<Prismify.Presentation.RootViewModel>();
        services.AddTransient<Mazeform.Presentation.RootViewModel>();
        services.AddTransient<Magnify.Presentation.RootViewModel>();
        services.AddTransient<Mergery.Presentation.RootViewModel>();
        services.AddTransient<PathLab.Presentation.RootViewModel>();
        services.AddTransient<BoidLab.Presentation.RootViewModel>();
        services.AddTransient<SortLab.Presentation.RootViewModel>();
        services.AddTransient<Sonum.Presentation.RootViewModel>();
        services.AddTransient<Align.Presentation.RootViewModel>();        
        services.AddTransient<Kanso.Presentation.RootViewModel>();

        services.AddTransient<SnakePlusPlus.Presentation.RootView>();
        services.AddTransient<CellautoLab.Presentation.RootView>();
        services.AddTransient<SlideForge.Presentation.RootView>();
        services.AddTransient<ChessSharp.Presentation.RootView>();
        services.AddTransient<FractalLab.Presentation.RootView>();
        services.AddTransient<KittyCatch.Presentation.RootView>();
        services.AddTransient<Semigraph.Presentation.RootView>();
        services.AddTransient<CodeScope.Presentation.RootView>();
        services.AddTransient<Mergery3d.Presentation.RootView>();
        services.AddTransient<Weatherly.Presentation.RootView>();
        services.AddTransient<HexaCards.Presentation.RootView>();
        services.AddTransient<Prismify.Presentation.RootView>();
        services.AddTransient<Mazeform.Presentation.RootView>();
        services.AddTransient<Magnify.Presentation.RootView>();
        services.AddTransient<Mergery.Presentation.RootView>();
        services.AddTransient<PathLab.Presentation.RootView>();
        services.AddTransient<BoidLab.Presentation.RootView>();
        services.AddTransient<SortLab.Presentation.RootView>();
        services.AddTransient<Sonum.Presentation.RootView>();
        services.AddTransient<Align.Presentation.RootView>();
        services.AddTransient<Kanso.Presentation.RootView>();
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
        services.AddSingleton<HubView>();

        services.AddSingleton<HomeViewModel>();
        services.AddSingleton<HomeView>();

        services.AddSingleton<SpacesViewModel>();
        services.AddTransient<HomeView>();

        services.AddSingleton<AccountViewModel>();
        services.AddTransient<AccountView>();

        services.AddSingleton<SettingsViewModel>();
        services.AddTransient<SettingsView>();

        services.AddSingleton<MessagesViewModel>();
        services.AddTransient<MessagesView>();

        services.AddSingleton<CommunityViewModel>();
        services.AddTransient<CommunityView>();
    }
}