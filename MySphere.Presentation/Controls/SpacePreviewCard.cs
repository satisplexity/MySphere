using MySphere.Presentation.Framework.Foundation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.Controls;

public class SpacePreviewCard : Button
{
    private BitmapSource _preview;
    public BitmapSource Preview
    {
        get => _preview;
        set
        {
            _preview = value;
            _contentImage.Source = _preview;
        }
    }
    public ViewModelBase Space { get; set; }
    public ScaleTransform Scale { get; set; } = new ScaleTransform();
    private Image _contentImage = new Image();
    public SpacePreviewCard()
    {
        this.RenderTransform = Scale;
        Content = _contentImage;

        this.Effect = new DropShadowEffect()
        {
            BlurRadius = 10,
            Color = Color.FromArgb(255, 20, 20, 20),
            ShadowDepth = 0,
            Opacity = 0.5,
            Direction = 230
        };
    }

    public void Set(BitmapSource preview, ViewModelBase space)
    {
        Preview = preview;
        Space = space;

        this.Width = Preview.Width / 2;
        this.Height = Preview.Height / 2;
    }
}