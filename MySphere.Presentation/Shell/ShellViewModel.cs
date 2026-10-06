using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Shell.Navigation;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Main;
using System.Windows;

namespace MySphere.Presentation.Shell;

public sealed class ShellViewModel 
    : ViewModelBase
{
    public IShellNavigationService Navigation { get; }

    private WindowState _windowState;

    public WindowState WindowState
    {
        get => _windowState;
        set
        {
            if (_windowState == value)
                return;

            _windowState = value;
            OnPropertyChanged();

            IsWindowCollapsed = value == WindowState.Minimized;
        }
    }

    private bool _isWindowCollapsed = true;

    public bool IsWindowCollapsed
    {
        get => _isWindowCollapsed;
        private set
        {
            if (_isWindowCollapsed == value)
                return;

            _isWindowCollapsed = value;



            OnPropertyChanged();
        }
    }

  

    public RelayCommand LoadedCommand { get; }

    public ShellViewModel(IShellNavigationService navigation)
    {
        Navigation = navigation;
        Navigation.NavigateTo<MainViewModel>();

        LoadedCommand = new(_ =>
        {
            WindowState = WindowState.Maximized;
        });
    }

    public void HideWindow()
    {
        IsWindowCollapsed = true;

        TimerFactory.Run(
            () => WindowState = WindowState.Minimized,
            0.4);
    }
}