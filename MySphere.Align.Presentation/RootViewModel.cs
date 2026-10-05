using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Align.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Align";
        Icon = GetIcon();
        Color = GetColor();
    }
}