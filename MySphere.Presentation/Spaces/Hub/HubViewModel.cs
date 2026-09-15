using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Spaces.Hub.Navigation;
using MySphere.Presentation.Spaces.Hub.Sections.Account;
using MySphere.Presentation.Spaces.Hub.Sections.Home;
using MySphere.Presentation.Spaces.Hub.Sections.Settings;
using MySphere.Presentation.Spaces.Hub.Sections.Spaces;

namespace MySphere.Presentation.Spaces.Hub;

public enum HubSection
{
    Home, Spaces, Account, Settings
}

public sealed class HubViewModel : ViewModelBase
{
    public IHubNavigationService Navigation { get; }

    private HubSection _selectedSection = HubSection.Home;

    public bool IsHomeSelected => _selectedSection == HubSection.Home;
    public bool IsSpacesSelected => _selectedSection == HubSection.Spaces;
    public bool IsAccountSelected => _selectedSection == HubSection.Account;
    public bool IsSettingsSelected => _selectedSection == HubSection.Settings;

    private void SelectSection(HubSection section)
    {
        if (!SetProperty(ref _selectedSection, section))
            return;

        OnPropertyChanged(nameof(IsHomeSelected));
        OnPropertyChanged(nameof(IsSpacesSelected));
        OnPropertyChanged(nameof(IsAccountSelected));
        OnPropertyChanged(nameof(IsSettingsSelected));
    }

    public RelayCommand ShowHomeCommand { get; }
    public RelayCommand ShowSpacesCommand { get; }
    public RelayCommand ShowAccountCommand { get; }
    public RelayCommand ShowSettingsCommand { get; }
    public HubViewModel(IHubNavigationService navigaton)
    {
        Navigation = navigaton;

        ShowHomeCommand = new(action =>
        {
            Navigation.NavigateTo<HomeViewModel>();
            SelectSection(HubSection.Home);
        });

        ShowSpacesCommand = new(action =>
        {
            Navigation.NavigateTo<SpacesViewModel>();
            SelectSection(HubSection.Spaces);
        });

        ShowAccountCommand = new(action =>
        {
            Navigation.NavigateTo<AccountViewModel>();
            SelectSection(HubSection.Account);
        });

        ShowSettingsCommand = new(action =>
        {
            Navigation.NavigateTo<SettingsViewModel>();
            SelectSection(HubSection.Settings);
        });

        Navigation.NavigateTo<HomeViewModel>();
        SelectSection(HubSection.Home);
    }
}