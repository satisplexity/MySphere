using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.SpaceSwitcher;

public class SpacePreview : ObservableObject
{
    public BitmapSource Preview { get; set; }

    public ViewModelBase ViewModel { get; set; }

    private int _index;

    public int Index
    {
        get => _index;
        set
        {
            if (_index != value)
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }
    }

    public double Width { get; set; }
    public double Height { get; set; }

    public SpacePreview(BitmapSource preview, ViewModelBase viewModel, int index = 0)
    {
        Preview = preview;
        ViewModel = viewModel;
        Index = index;
        Width = preview.PixelWidth / 3.0;
        Height = preview.PixelHeight / 3.0;
    }
}
