using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main.Navigation;
using MySphere.Presentation.Hub;
using MySphere.Framework.Foundation.Command;
using Microsoft.Extensions.DependencyInjection;
using MySphere.Presentation.Shell;
using MySphere.Framework.Utilities;

namespace MySphere.Presentation.Main;

public sealed class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public IMainNavigationService Navigation { get; }

    public RelayCommand MinimizeWindowCommand { get; }

    public RelayCommand ShutdownCommand { get; }

    public MainViewModel(IMainNavigationService navigation, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        Navigation = navigation;

        Navigation.NavigateTo<HubViewModel>();

        MinimizeWindowCommand = new(_ =>
        {
            TimerFactory.Run(() => _serviceProvider.GetRequiredService<ShellViewModel>().HideWindow(), 0.2);
        });

        ShutdownCommand = new(_ =>
        {
            TimerFactory.Run(() => App.Current.Shutdown(), 0.2);
        });
    }
}