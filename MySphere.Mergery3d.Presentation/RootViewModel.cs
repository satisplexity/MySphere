using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Mergery3d.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Mergery 3D";
        Icon = GetIcon("Mergery3d");
        Color = GetColor("Mergery3d");
    }
}