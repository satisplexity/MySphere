using MySphere.Presentation.Framework.Foundation;

namespace MySphere.Presentation.Framework.Navigation;

public abstract class NavigationStore : ObservableObject
{
    private ViewModelBase? _currentViewModel;

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }
}