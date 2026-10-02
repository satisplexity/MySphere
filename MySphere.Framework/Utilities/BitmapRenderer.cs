using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MySphere.Framework.Utilities;

public enum RenderSize
{
    Full = 1,
    Half = 2,
    Third = 3
}

public static class BitmapRenderer
{
    public static BitmapSource Render(FrameworkElement element, RenderSize renderSize = RenderSize.Full)
    {
        if (element is null)
            throw new ArgumentNullException(nameof(element));

        int width = (int)element.ActualWidth;
        int height = (int)element.ActualHeight;

        if (width <= 0 || height <= 0)
            throw new InvalidOperationException("Element has invalid size.");

        RenderTargetBitmap bitmap = new RenderTargetBitmap(width / (int)renderSize, height / (int)renderSize, 96 / (int)renderSize, 96 / (int)renderSize, PixelFormats.Pbgra32);

        bitmap.Render(element);
        bitmap.Freeze();

        return bitmap;
    }
}