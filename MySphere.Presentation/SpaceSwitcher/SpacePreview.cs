using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.SpaceSwitcher;

public class SpacePreview : ObservableObject
{
    public BitmapSource Preview { get; set; }

    public ViewModelBase ViewModel { get; set; }

    public string Title { get; set; }

    private int _positionIndex;

    public int PositionIndex
    {
        get => _positionIndex;
        set
        {
            if (SetProperty(ref _positionIndex, value))
            {
                OnPropertyChanged(nameof(ZIndex));
            }
        }
    }

    public int ZIndex => Math.Max(0, 100 - Math.Abs(PositionIndex));

    public double Width { get; set; }
    public double Height { get; set; }

    public SpacePreview(BitmapSource preview, ViewModelBase viewModel, int index = 0)
    {
        Preview = preview;
        ViewModel = viewModel;
        PositionIndex = index;
        Width = preview.PixelWidth;
        Height = preview.PixelHeight;
        Title = viewModel.Name;
    }
}
