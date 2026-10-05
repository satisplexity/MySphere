using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.SlideForge.Presentation;

public class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "SlideForge";
        Icon = GetIcon();
        Color = GetColor();
    }
}