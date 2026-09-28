using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Foundation.Command;

using MySphere.Presentation.Hub.Navigation;
using MySphere.Presentation.Hub.Sections.Home;
using MySphere.Presentation.Hub.Sections.Spaces;
using MySphere.Presentation.Hub.Sections.Account;
using MySphere.Presentation.Hub.Sections.Settings;

namespace MySphere.Presentation.Hub;

public enum HubSection
{
    Home,
    Spaces,
    Account,
    Settings
}

public sealed class HubViewModel : ViewModelBase
{
    public IHubNavigationService Navigation { get; }

    private HubSection _selectedSection;

    public bool IsHomeSelected => _selectedSection == HubSection.Home;
    public bool IsSpacesSelected => _selectedSection == HubSection.Spaces;
    public bool IsAccountSelected => _selectedSection == HubSection.Account;
    public bool IsSettingsSelected => _selectedSection == HubSection.Settings;

    public RelayCommand NavigateToHomeCommand { get; }
    public RelayCommand NavigateToSpacesCommand { get; }
    public RelayCommand NavigateToAccountCommand { get; }
    public RelayCommand NavigateToSettingsCommand { get; }

    public HubViewModel(IHubNavigationService navigation)
    {
        Navigation = navigation;

        NavigateToHomeCommand = new(_ =>
        {
            Navigation.NavigateTo<HomeViewModel>();
            Select(HubSection.Home);
        });

        NavigateToSpacesCommand = new(_ =>
        {
            Navigation.NavigateTo<SpacesViewModel>();
            Select(HubSection.Spaces);
        });

        NavigateToAccountCommand = new(_ =>
        {
            Navigation.NavigateTo<AccountViewModel>();
            Select(HubSection.Account);
        });

        NavigateToSettingsCommand = new(_ =>
        {
            Navigation.NavigateTo<SettingsViewModel>();
            Select(HubSection.Settings);
        });

        Navigation.NavigateTo<HomeViewModel>();
        Select(HubSection.Home);
    }

    private void Select(HubSection section)
    {
        if (!SetProperty(ref _selectedSection, section))
            return;

        OnPropertyChanged(nameof(IsHomeSelected));
        OnPropertyChanged(nameof(IsSpacesSelected));
        OnPropertyChanged(nameof(IsAccountSelected));
        OnPropertyChanged(nameof(IsSettingsSelected));
    }
}