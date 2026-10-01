using MySphere.Framework.Foundation.ViewModel;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.SpaceSwitcher;

public class SpacePreview : ObservableObject
{
    public BitmapSource Preview { get; set; }

    public ViewModelBase ViewModel { get; set; }

    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }

    public SpacePreview(BitmapSource preview, ViewModelBase viewModel, int index = 0)
    {
        Preview = preview;
        ViewModel = viewModel;
        Index = index;
        Width = preview.PixelWidth / 2.0;
        Height = preview.PixelHeight / 2.0;
    }
}
