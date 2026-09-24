using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MySphere.Presentation.Spaces.Hub;
using MySphere.Presentation.Spaces.Shell;
using MySphere.Presentation.Spaces.Shell.Navigation;
using MySphere.Presentation.Spaces.Semigraph;
using MySphere.Presentation.Spaces.PathLab;
using MySphere.Presentation.Spaces.SortLab;
using MySphere.Presentation.Spaces.Hub.Navigation;
using MySphere.Presentation.Spaces.FractalLab;
using MySphere.Presentation.Spaces.Semigraph.Navigation;

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
        ConfigureSemigraph(services);
        services.AddTransient<PathLabViewModel>();
        services.AddTransient<SortLabViewModel>();

        services.AddTransient<FractalLabViewModel>();
    }

    private void ConfigureHub(IServiceCollection services)
    {
        services.AddSingleton<HubNavigationStore>();
        services.AddSingleton<IHubNavigationService, HubNavigationService>();

        services.AddTransient<HubViewModel>();

        services.AddTransient<Spaces.Hub.Sections.Home.HomeViewModel>();
        services.AddTransient<Spaces.Hub.Sections.Spaces.SpacesViewModel>();
        services.AddTransient<Spaces.Hub.Sections.Account.AccountViewModel>();
        services.AddTransient<Spaces.Hub.Sections.Settings.SettingsViewModel>();
    }

    private void ConfigureSemigraph(IServiceCollection services)
    {
        services.AddTransient<SemigraphNavigationStore>();
        services.AddTransient<ISemigraphNavigationService, SemigraphNavigationService>();

        services.AddTransient<SemigraphViewModel>();

        services.AddTransient<Spaces.Semigraph.Sections.Home.HomeViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Learn.LearnViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Graph.GraphViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Create.CreateViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Storage.StorageViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Analysis.AnalysisViewModel>();
        services.AddTransient<Spaces.Semigraph.Sections.Settings.SettingsViewModel>();
    }
}