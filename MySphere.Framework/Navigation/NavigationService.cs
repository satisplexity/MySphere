using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Framework.Navigation;

public abstract class NavigationService : ObservableObject, INavigationService
{
    private readonly NavigationStore _store;

    private readonly IServiceProvider _services;

    private readonly Stack<ViewModelBase> _history = new();

    public ViewModelBase? CurrentViewModel => _store.CurrentViewModel;

    public bool CanGoBack => _history.Count > 0;

    public NavigationService(NavigationStore store, IServiceProvider services)
    {
        _store = store;
        _services = services;

        _store.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(NavigationStore.CurrentViewModel))
                OnPropertyChanged(nameof(CurrentViewModel));
        };
    }

    public Task NavigateTo<TViewModel>()
        where TViewModel : ViewModelBase =>
        NavigateTo(typeof(TViewModel), null, addToHistory: true);

    public Task NavigateTo<TViewModel, TParameter>(TParameter parameter)
        where TViewModel : ViewModelBase =>
        NavigateTo(typeof(TViewModel), parameter, addToHistory: true);


    public Task ReplaceWith<TViewModel>()
        where TViewModel : ViewModelBase =>
        NavigateTo(typeof(TViewModel), null, addToHistory: false);

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
            ? (ViewModelBase)_services.GetRequiredService(viewModelType)
            : (ViewModelBase)ActivatorUtilities.CreateInstance(_services, viewModelType, parameter);

        OnPropertyChanged(nameof(CanGoBack));

        return Task.CompletedTask;
    }
}