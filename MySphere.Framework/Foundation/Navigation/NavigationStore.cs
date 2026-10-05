using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media;

namespace MySphere.Framework.Foundation.Navigation;

public class NavigationStore
    : ObservableObject
{
    private ViewModelBase _currentViewModel = null!;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            if (_currentViewModel == value)
                return;

            _currentViewModel = value;

            CurrentTitle = value.Name;
            CurrentIcon = value.Icon;
            CurrentColor = value.Color;

            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    private string _currentTitle = "DEFAULT";

    public string CurrentTitle
    {
        get => _currentTitle;
        set => SetProperty(ref _currentTitle, value);
    }

    private Geometry _currentIcon = null!;

    public Geometry CurrentIcon
    {
        get => _currentIcon;
        set => SetProperty(ref _currentIcon, value);
    }

    private LinearGradientBrush _currentColor = null!;

    public LinearGradientBrush CurrentColor
    {
        get => _currentColor;
        set => SetProperty(ref _currentColor, value);
    }
}