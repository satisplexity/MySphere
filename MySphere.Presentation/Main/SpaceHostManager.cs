using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Utilities;
using MySphere.Presentation.Main.Controls;
using MySphere.Presentation.Main.Navigation;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.Main;

public sealed class SpaceHostManager
{
    private SpaceHost? _host;

    private readonly IMainNavigationService _mainViewModel;

    public SpaceHostManager(IMainNavigationService navigationService)
    {
        _mainViewModel = navigationService;
    }

    public void Attach(SpaceHost host)
    {
        _host = host;
    }

    public void Detach(SpaceHost host)
    {
        if (_host == host)
            host = null!;
    }

    public BitmapSource GetPreview()
        => BitmapRenderer.Render(_host);

    public async Task Open<T>()
        where T : ViewModelBase
    {
        if (_host is null)
            throw new InvalidOperationException();
        
        await _host.AnimateNewSpaceOpening();

        await _mainViewModel.NavigateTo<T>();
    }
}