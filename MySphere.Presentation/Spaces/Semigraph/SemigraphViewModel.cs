using MySphere.Presentation.Spaces.Semigraph.Sections.Settings;
using MySphere.Presentation.Spaces.Semigraph.Sections.Storage;
using MySphere.Presentation.Spaces.Semigraph.Sections.Create;
using MySphere.Presentation.Spaces.Semigraph.Sections.Graph;
using MySphere.Presentation.Spaces.Semigraph.Sections.Learn;
using MySphere.Presentation.Spaces.Semigraph.Sections.Home;
using MySphere.Presentation.Spaces.Semigraph.Navigation;
using MySphere.Presentation.Framework.Foundation;

namespace MySphere.Presentation.Spaces.Semigraph;

public enum SemigraphSection
{
    Home,
    Create,
    Learn,
    Storage,
    Graph,
    Analysis,
    Settings
}

public class SemigraphViewModel : ViewModelBase
{
    public ISemigraphNavigationService Navigation { get;  }

    private SemigraphSection _selectedSection = SemigraphSection.Home;

    public bool IsHomeSelected => _selectedSection == SemigraphSection.Home;
    public bool IsGraphSelected => _selectedSection == SemigraphSection.Graph;
    public bool IsLearnSelected => _selectedSection == SemigraphSection.Learn;
    public bool IsCreateSelected => _selectedSection == SemigraphSection.Create;
    public bool IsStorageSelected => _selectedSection == SemigraphSection.Storage;
    public bool IsAnalysisSelected => _selectedSection == SemigraphSection.Analysis;
    public bool IsSettingsSelected => _selectedSection == SemigraphSection.Settings;

    public RelayCommand NavigateToHomeCommand { get; }
    public RelayCommand NavigateToGraphCommand { get; }
    public RelayCommand NavigateToLearnCommand { get; }
    public RelayCommand NavigateToCreateCommand { get; }
    public RelayCommand NavigateToStorageCommand { get; }
    public RelayCommand NavigateToAnalysisCommand { get; }
    public RelayCommand NavigateToSettingsCommand { get; }

    public SemigraphViewModel(ISemigraphNavigationService navigation)
    {
        Navigation = navigation;

        NavigateToHomeCommand = new(_ =>
        {
            Navigation.NavigateTo<HomeViewModel>();
            Select(SemigraphSection.Home);
        });

        NavigateToGraphCommand = new(_ =>
        {
            Navigation.NavigateTo<GraphViewModel>();
            Select(SemigraphSection.Graph);
        });

        NavigateToLearnCommand = new(_ =>
        {
            Navigation.NavigateTo<LearnViewModel>();
            Select(SemigraphSection.Learn);
        });

        NavigateToCreateCommand = new(_ =>
        {
            Navigation.NavigateTo<CreateViewModel>();
            Select(SemigraphSection.Create);
        });

        NavigateToStorageCommand = new(_ =>
        {
            Navigation.NavigateTo<StorageViewModel>();
            Select(SemigraphSection.Storage);
        });

        NavigateToSettingsCommand = new(_ =>
        {
            Navigation.NavigateTo<SettingsViewModel>();
            Select(SemigraphSection.Settings);
        });

        Navigation.NavigateTo<HomeViewModel>();
        Select(SemigraphSection.Home);
    }

    private void Select(SemigraphSection section)
    {
        if (!SetProperty(ref _selectedSection, section))
            return;

        OnPropertyChanged(nameof(IsHomeSelected));
        OnPropertyChanged(nameof(IsGraphSelected));
        OnPropertyChanged(nameof(IsLearnSelected));
        OnPropertyChanged(nameof(IsCreateSelected));
        OnPropertyChanged(nameof(IsStorageSelected));
        OnPropertyChanged(nameof(IsAnalysisSelected));
        OnPropertyChanged(nameof(IsSettingsSelected));
    }
}