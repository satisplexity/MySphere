using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.PathLab.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "PathLab";
        Icon = GetIcon();
        Color = GetColor();
    }
}