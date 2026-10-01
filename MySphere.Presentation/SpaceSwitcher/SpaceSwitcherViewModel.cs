using MySphere.Framework.Foundation.ViewModel;
using MySphere.Framework.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.SpaceSwitcher;

public sealed class SpaceSwitcherViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<SpacePreview> Previews { get; private set; } = new();

    private BitmapSource _currentPreview;
    public BitmapSource CurrentPreview
    {
        get => _currentPreview;
        set
        {
            _currentPreview = value;
            PlayOpenAnimation = true;
            OnPropertyChanged(nameof(PlayOpenAnimation));

            TimerFactory.Run(() =>
            {
                PreviewVisibility = Visibility.Collapsed;
                OnPropertyChanged(nameof(PreviewVisibility));
                PlayOpenAnimation = false;
            }, 0.5);
        }
    }

    public Visibility PreviewVisibility { get; private set; } = Visibility.Visible;
    public bool PlayOpenAnimation { get; set; }

    public SpaceSwitcherViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetPreview(BitmapSource preview, ViewModelBase viewModel)
    {
        CurrentPreview = preview;

        Previews.Add(new SpacePreview(preview, viewModel));

        Previews.Add(new SpacePreview(preview, viewModel, -1));
    }

    public void AnimateTransitionIn()
    {

    }

    public void AnimateTransitionOut()
    {

    }
}