using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media;

namespace MySphere.Framework.Foundation.Navigation;

public interface INavigationService
{
    ViewModelBase CurrentViewModel { get; }

    string CurrentTitle { get; }

    Geometry CurrentIcon { get; }

    LinearGradientBrush CurrentColor { get; }

    bool CanGoBack { get; }

    Task NavigateTo<TViewModel, TParameter>(TParameter parameter)
        where TViewModel : ViewModelBase;

    Task NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase;

    Task ReplaceWith<TViewModel>()
        where TViewModel : ViewModelBase;

    Task ReplaceWith(ViewModelBase viewModel);

    Task GoBack();
}