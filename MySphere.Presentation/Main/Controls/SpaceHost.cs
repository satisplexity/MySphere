using MySphere.Framework.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MySphere.Presentation.Main.Controls;

public class SpaceHost
    : ContentControl
{
    public static readonly DependencyProperty SpaceHostManagerProperty =
        DependencyProperty.Register(
            nameof(SpaceManager),
            typeof(SpaceHostManager),
            typeof(SpaceHost),
            new PropertyMetadata(null, OnSpaceServiceChanged));

    public SpaceHostManager? SpaceManager
    {
        get => (SpaceHostManager?)GetValue(SpaceHostManagerProperty);
        set => SetValue(SpaceHostManagerProperty, value);
    }

    private static void OnSpaceServiceChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var host = (SpaceHost)d;

        if (e.OldValue is SpaceHostManager oldService)
            oldService.Detach(host);

        if (e.NewValue is SpaceHostManager newService)
            newService.Attach(host);
    }

    public static readonly DependencyProperty IsNewSpaceOpenedProperty =
        DependencyProperty.Register(
            nameof(IsNewSpaceOpened),
            typeof(bool),
            typeof(SpaceHost));

    public bool IsNewSpaceOpened
    {
        get => (bool)GetValue(IsNewSpaceOpenedProperty);
        set => SetValue(IsNewSpaceOpenedProperty, value);
    }

    public static readonly DependencyProperty PreviousSpaceImageProperty =
        DependencyProperty.Register(
            nameof(PreviousSpaceImage),
            typeof(BitmapSource),
            typeof(SpaceHost));

    public BitmapSource PreviousSpaceImage
    {
        get => (BitmapSource)GetValue(PreviousSpaceImageProperty);
        set => SetValue(PreviousSpaceImageProperty, value);
    }

    public async Task AnimateNewSpaceOpening()
    {
        this.UpdateLayout();
        PreviousSpaceImage = BitmapRenderer.Render(this);
        await Dispatcher.InvokeAsync(
        () => UpdateLayout(),
        DispatcherPriority.Render);

        IsNewSpaceOpened = true;

        TimerFactory.Run(() => IsNewSpaceOpened = false, 10);
    }
}