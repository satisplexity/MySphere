using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MySphere.Presentation.Spaces.Hub;
using MySphere.Presentation.Spaces.Shell;
using MySphere.Presentation.Spaces.Shell.Navigation;
using MySphere.Presentation.Spaces.Hub.Sections.Home;
using MySphere.Presentation.Spaces.Hub.Sections.Spaces;
using MySphere.Presentation.Spaces.Hub.Sections.Account;
using MySphere.Presentation.Spaces.Hub.Sections.Settings;
using MySphere.Presentation.Spaces.Semigraph;
using MySphere.Presentation.Spaces.PathLab;
using MySphere.Presentation.Spaces.SortLab;
using MySphere.Presentation.Spaces.Hub.Navigation;

namespace MySphere.Presentation;

public partial class App : Application
{
    public IServiceProvider ServiceProvider = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        Configure(services);

        ServiceProvider = services.BuildServiceProvider();

        ServiceProvider
            .GetRequiredService<ShellWindow>()
            .Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (ServiceProvider is IDisposable disposable)
            disposable.Dispose();

        base.OnExit(e);
    }

    private void Configure(IServiceCollection services)
    {
        services.AddSingleton<ShellNavigationStore>();
        services.AddSingleton<IShellNavigationService, ShellNavigationService>();

        services.AddSingleton<ShellViewModel>();

        services.AddSingleton<ShellWindow>(serviceProvider =>
            new ShellWindow()
            {
                DataContext = serviceProvider.GetRequiredService<ShellViewModel>()
            });

        ConfigureHub(services);

        services.AddTransient<SemigraphViewModel>();
        services.AddTransient<PathLabViewModel>();
        services.AddTransient<SortLabViewModel>();
    }

    private void ConfigureHub(IServiceCollection services)
    {
        services.AddSingleton<HubNavigationStore>();
        services.AddSingleton<IHubNavigationService, HubNavigationService>();

        services.AddTransient<HubViewModel>();

        services.AddTransient<HomeViewModel>();
        services.AddTransient<SpacesViewModel>();
        services.AddTransient<AccountViewModel>();
        services.AddTransient<SettingsViewModel>();
    }
}