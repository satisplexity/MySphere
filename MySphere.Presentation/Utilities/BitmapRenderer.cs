using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Presentation.Utilities;

public enum RenderQuality
{
    Low = 48,
    Medium = 64,
    High = 96
}

public class BitmapRenderer
{
    public static BitmapSource Render(FrameworkElement element, RenderQuality quality = RenderQuality.High)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        int width = (int)Math.Ceiling(element.ActualWidth);
        int height = (int)Math.Ceiling(element.ActualHeight);

        if (width <= 0 || height <= 0)
            throw new InvalidOperationException("Element has invalid size.");

        RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, (int)quality, (int)quality, PixelFormats.Pbgra32);

        bitmap.Render(element);
        bitmap.Freeze();

        return bitmap;
    }
}