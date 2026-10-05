using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Semigraph.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Semigraph";
        Icon = GetIcon();
        Color = GetColor();
    }
}