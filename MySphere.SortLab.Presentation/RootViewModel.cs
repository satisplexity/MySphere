using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.SortLab.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "SortLab";
        Icon = GetIcon();
        Color = GetColor();
    }
}