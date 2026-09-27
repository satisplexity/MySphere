using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using MySphere.Presentation.Shell;
using MySphere.Presentation.Main;

using MySphere.Presentation.Shell.Navigation;
using MySphere.Presentation.Main.Navigation;

namespace MySphere.Presentation;

public partial class App : Application
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
        services.AddSingleton<ShellNavigationStore>();
        services.AddSingleton<IShellNavigationService, ShellNavigationService>();

        services.AddSingleton<ShellViewModel>();

        services.AddSingleton<ShellWindow>(serviceProvider =>
            new ShellWindow()
            {
                DataContext = serviceProvider.GetRequiredService<ShellViewModel>()
            });

        services.AddSingleton<MainNavigationStore>();
        services.AddSingleton<IMainNavigationService, MainNavigationService>();

        services.AddSingleton<MainViewModel>();

        services.AddTransient<MainView>();
    }
}