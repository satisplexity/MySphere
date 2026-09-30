using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main.Navigation;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Shell;
using MySphere.Presentation.Hub;

namespace MySphere.Presentation.Main;

public sealed class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public SpaceHostManager SpaceManager { get; }

    public IMainNavigationService Navigation { get; }

    public RelayCommand MinimizeWindowCommand { get; }

    public RelayCommand ShutdownCommand { get; }

    public MainViewModel(
        IMainNavigationService navigation, 
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        SpaceManager = _serviceProvider.GetRequiredService<SpaceHostManager>();

        Navigation = navigation;

        Navigation.NavigateTo<HubViewModel>();

        MinimizeWindowCommand = new(_ => MinimizeWindow());

        ShutdownCommand = new(_ =>
        {
            TimerFactory.Run(
                () => App.Current.Shutdown(), 
                0.2);
        });
    }

    private void MinimizeWindow()
    {
        TimerFactory.Run(
            () => _serviceProvider.GetRequiredService<ShellViewModel>().HideWindow(),
            0.2);
    }
}