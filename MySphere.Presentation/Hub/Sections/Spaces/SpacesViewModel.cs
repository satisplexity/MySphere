using Microsoft.Extensions.DependencyInjection;
using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Foundation.Command;
using MySphere.Presentation.Main;

namespace MySphere.Presentation.Hub.Sections.Spaces;

public sealed class SpacesViewModel : ViewModelBase
{
    #region Commands

    public RelayCommand NavigateToSnakePlusPlusCommand { get; }
    public RelayCommand NavigateToCellautoLabCommand { get; }
    public RelayCommand NavigateToSlideForgeCommand { get; }
    public RelayCommand NavigateToChessSharpCommand { get; }
    public RelayCommand NavigateToFractalLabCommand { get; }
    public RelayCommand NavigateToKittyCatchCommand { get; }
    public RelayCommand NavigateToSemigraphCommand { get; }
    public RelayCommand NavigateToCodeScopeCommand { get; }
    public RelayCommand NavigateToMergery3dCommand { get; }
    public RelayCommand NavigateToWeatherlyCommand { get; }
    public RelayCommand NavigateToHexaCardsCommand { get; }
    public RelayCommand NavigateToPrismifyCommand { get; }
    public RelayCommand NavigateToMazeformCommand { get; }
    public RelayCommand NavigateToMagnifyCommand { get; }
    public RelayCommand NavigateToMergeryCommand { get; }
    public RelayCommand NavigateToPathLabCommand { get; }
    public RelayCommand NavigateToBoidLabCommand { get; }
    public RelayCommand NavigateToSortLabCommand { get; }
    public RelayCommand NavigateToSonumCommand { get; }
    public RelayCommand NavigateToAlignCommand { get; }
    public RelayCommand NavigateToKansoCommand { get; }

    #endregion

    private readonly SpaceHostManager _spaceManager;
    private readonly MainViewModel _mainViewModel;

    public SpacesViewModel(IServiceProvider serviceProvider)
    {
        _spaceManager = serviceProvider.GetRequiredService<SpaceHostManager>();
        _mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();

        #region Commands initialization

        NavigateToSnakePlusPlusCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<SnakePlusPlus.Presentation.RootViewModel>();
        });

        NavigateToCellautoLabCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<CellautoLab.Presentation.RootViewModel>();
        });

        NavigateToSlideForgeCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<SlideForge.Presentation.RootViewModel>();
        });

        NavigateToChessSharpCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<ChessSharp.Presentation.RootViewModel>();
        });

        NavigateToFractalLabCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<FractalLab.Presentation.RootViewModel>();
        });

        NavigateToKittyCatchCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<KittyCatch.Presentation.RootViewModel>();
        });

        NavigateToSemigraphCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Semigraph.Presentation.RootViewModel>();
        });

        NavigateToCodeScopeCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<CodeScope.Presentation.RootViewModel>();
        });

        NavigateToMergery3dCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Mergery3d.Presentation.RootViewModel>();
        });

        NavigateToWeatherlyCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Weatherly.Presentation.RootViewModel>();
        });

        NavigateToHexaCardsCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<HexaCards.Presentation.RootViewModel>();
        });

        NavigateToPrismifyCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Prismify.Presentation.RootViewModel>();
        });

        NavigateToMazeformCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Mazeform.Presentation.RootViewModel>();
        });

        NavigateToMagnifyCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Magnify.Presentation.RootViewModel>();
        });

        NavigateToMergeryCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Mergery.Presentation.RootViewModel>();
        });

        NavigateToPathLabCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<PathLab.Presentation.RootViewModel>();
        });

        NavigateToBoidLabCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<BoidLab.Presentation.RootViewModel>();
        });
        
        NavigateToSortLabCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<SortLab.Presentation.RootViewModel>();
        });

        NavigateToSonumCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Sonum.Presentation.RootViewModel>();
        });

        NavigateToAlignCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Align.Presentation.RootViewModel>();
        });

        NavigateToKansoCommand = new(_ =>
        {
            _mainViewModel.CaptureCurrentSpace();
            _spaceManager.Open<Kanso.Presentation.RootViewModel>();
        });

        #endregion
    }
}