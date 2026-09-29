using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main.Navigation;
using MySphere.Presentation.Hub;
using MySphere.Framework.Foundation.Command;
using Microsoft.Extensions.DependencyInjection;
using MySphere.Presentation.Shell;

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
            _serviceProvider.GetRequiredService<ShellViewModel>().HideWindow();
        });

        ShutdownCommand = new(_ =>
        {
            App.Current.Shutdown();
        });
    }
}