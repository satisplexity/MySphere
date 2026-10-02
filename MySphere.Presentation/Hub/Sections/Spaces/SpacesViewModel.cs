using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.Command;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Presentation.Main;
using MySphere.Semigraph.Presentation;

namespace MySphere.Presentation.Hub.Sections.Spaces;

public sealed class SpacesViewModel : ViewModelBase
{
    private readonly SpaceHostManager _spaceManager;

    public RelayCommand OpenSemigraphCommand { get; }

    public SpacesViewModel(IServiceProvider serviceProvider)
    {
        _spaceManager = serviceProvider.GetRequiredService<SpaceHostManager>();

        OpenSemigraphCommand = new(() =>
        {
            serviceProvider.GetRequiredService<MainViewModel>().CaptureCurrentSpace();
            _spaceManager.Open<SemigraphViewModel>();
        });
    }
}