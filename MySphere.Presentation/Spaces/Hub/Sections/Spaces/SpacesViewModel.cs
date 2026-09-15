using Microsoft.Extensions.DependencyInjection;
using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Spaces.Hub.Navigation;
using MySphere.Presentation.Spaces.Hub.Sections.Home;
using MySphere.Presentation.Spaces.PathLab;
using MySphere.Presentation.Spaces.Semigraph;
using MySphere.Presentation.Spaces.Shell;
using MySphere.Presentation.Spaces.Shell.Navigation;
using MySphere.Presentation.Spaces.SortLab;

namespace MySphere.Presentation.Spaces.Hub.Sections.Spaces;

public class SpacesViewModel : ViewModelBase
{
    public RelayCommand OpenSemigraphCommand { get; }
    public RelayCommand OpenPathLabCommand { get; }

    public RelayCommand OpenSortLabCommand { get; }

    public SpacesViewModel(IServiceProvider services)
    {
        IShellNavigationService shellNavigationService = services.GetRequiredService<IShellNavigationService>();
        ShellWindow shellWindow = services.GetRequiredService<ShellWindow>();
        IHubNavigationService hubNavigation = services.GetRequiredService<IHubNavigationService>();

        OpenSemigraphCommand = new(action =>
        {
            shellWindow.CaptureCurrentSpace();
            shellNavigationService.ReplaceWith<SemigraphViewModel>();
            hubNavigation.NavigateTo<SpacesViewModel>();
        });

        OpenPathLabCommand = new(action =>
        {
            shellWindow.CaptureCurrentSpace();
            shellNavigationService.ReplaceWith<PathLabViewModel>();
            hubNavigation.NavigateTo<SpacesViewModel>();
        });

        OpenSortLabCommand = new(action =>
        {
            shellWindow.CaptureCurrentSpace();
            shellNavigationService.ReplaceWith<SortLabViewModel>();
            hubNavigation.NavigateTo<SpacesViewModel>();
        });
    }
}