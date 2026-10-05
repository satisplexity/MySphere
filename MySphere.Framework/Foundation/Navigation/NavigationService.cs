using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media;

namespace MySphere.Framework.Foundation.Navigation;

public abstract class NavigationService 
    : ObservableObject, INavigationService
{
    private readonly NavigationStore _store;

    private readonly IServiceProvider _serviceProvider;

    private readonly Stack<ViewModelBase> _history = new();

    public ViewModelBase CurrentViewModel 
        => _store.CurrentViewModel;

    public bool CanGoBack 
        => _history.Count > 0;

    public string CurrentTitle => _store.CurrentTitle;
    public Geometry CurrentIcon => _store.CurrentIcon;
    public LinearGradientBrush CurrentColor => _store.CurrentColor;


    public NavigationService(NavigationStore store, IServiceProvider serviceProvider)
    {
        _store = store;
        _serviceProvider = serviceProvider;

        _store.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationStore.CurrentViewModel))
                OnPropertyChanged(nameof(CurrentViewModel));

            if (args.PropertyName == nameof(NavigationStore.CurrentTitle))
                OnPropertyChanged(nameof(CurrentTitle));

            if (args.PropertyName == nameof(NavigationStore.CurrentIcon))
                OnPropertyChanged(nameof(CurrentIcon));

            if (args.PropertyName == nameof(NavigationStore.CurrentColor))
                OnPropertyChanged(nameof(CurrentColor));
        };
    }

    public Task NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase
        => NavigateTo(
            typeof(TViewModel),
            null, 
            addToHistory: true);

    public Task NavigateTo<TViewModel, TParameter>(TParameter parameter)
        where TViewModel : ViewModelBase
        => NavigateTo(
            typeof(TViewModel),
            parameter, 
            addToHistory: true);


    public Task ReplaceWith<TViewModel>()
        where TViewModel : ViewModelBase
        => NavigateTo(
            typeof(TViewModel),
            null,
            addToHistory: false);

    public Task ReplaceWith(ViewModelBase viewModel)
    {
        _store.CurrentViewModel = viewModel;

        OnPropertyChanged(nameof(CanGoBack));

        return Task.CompletedTask;
    }

    public Task GoBack()
    {
        if (!CanGoBack)
            return Task.CompletedTask;

        _store.CurrentViewModel = _history.Pop();

        OnPropertyChanged(nameof(CanGoBack));

        return Task.CompletedTask;
    }

    private Task NavigateTo(Type viewModelType, object? parameter, bool addToHistory)
    {
        if (addToHistory && _store.CurrentViewModel is not null)
            _history.Push(_store.CurrentViewModel);

        _store.CurrentViewModel = parameter is null
            ? (ViewModelBase)_serviceProvider.GetRequiredService(viewModelType)
            : (ViewModelBase)ActivatorUtilities.CreateInstance(_serviceProvider, viewModelType, parameter);

        OnPropertyChanged(nameof(CanGoBack));

        return Task.CompletedTask;
    }
}