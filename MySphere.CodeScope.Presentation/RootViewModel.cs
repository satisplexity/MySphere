using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.CodeScope.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "CodeScope";
        Icon = GetIcon();
        Color = GetColor();
    }
}