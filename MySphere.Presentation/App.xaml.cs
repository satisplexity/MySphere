using Microsoft.Extensions.DependencyInjection;
using MySphere.Presentation.Shell;
using System.Windows;

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
        services.AddSingleton<ShellWindow>();
    }
}