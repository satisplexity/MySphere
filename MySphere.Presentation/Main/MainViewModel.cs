using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main.Navigation;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Shell;
using MySphere.Presentation.Hub;
using MySphere.Presentation.SpaceSwitcher;

namespace MySphere.Presentation.Main;

public sealed class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public SpaceHostManager SpaceManager { get; }

    public IMainNavigationService Navigation { get; }

    public RelayCommand MinimizeWindowCommand { get; }

    public RelayCommand ShutdownCommand { get; }

    public RelayCommand NavigateToSpaceSwitcherCommand { get; }

    private bool _isSwitcherVisible = false;

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

        NavigateToSpaceSwitcherCommand = new(_ =>
        {
            SpaceSwitcherViewModel spaceSwitcher = _serviceProvider.GetRequiredService<SpaceSwitcherViewModel>();

            if(!_isSwitcherVisible)
            {
                spaceSwitcher.SetPreview(SpaceManager.GetPreview(), SpaceManager.GetPreview(RenderSize.Third), Navigation.CurrentViewModel);

                spaceSwitcher.AnimateTransitionIn();

                Navigation.ReplaceWith<SpaceSwitcherViewModel>();

                _isSwitcherVisible = true;
            }

            else
            {
                _isSwitcherVisible = false;

                spaceSwitcher.AnimateTransitionOut();
            }
        });
    }

    private void MinimizeWindow()
    {
        TimerFactory.Run(
            () => _serviceProvider.GetRequiredService<ShellViewModel>().HideWindow(),
            0.2);
    }

    public void CaptureCurrentSpace()
    {
        SpaceSwitcherViewModel spaceSwitcher = _serviceProvider.GetRequiredService<SpaceSwitcherViewModel>();
        spaceSwitcher.SetPreview(SpaceManager.GetPreview(), SpaceManager.GetPreview(RenderSize.Third), Navigation.CurrentViewModel);
    }
}