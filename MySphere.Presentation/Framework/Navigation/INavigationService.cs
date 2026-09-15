using MySphere.Presentation.Framework.Foundation;

namespace MySphere.Presentation.Framework.Navigation;

public interface INavigationService
{
    ViewModelBase? CurrentViewModel { get; }
    
    bool CanGoBack { get; }
    
    Task NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase;

    Task NavigateTo<TViewModel, TParameter>(TParameter parameter)
        where TViewModel : ViewModelBase;

    Task ReplaceWith<TViewModel>()
        where TViewModel : ViewModelBase;

    Task ReplaceWith(ViewModelBase viewModel);

    Task GoBack();
}