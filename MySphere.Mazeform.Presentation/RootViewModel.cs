using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Mazeform.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Mazeform";
        Icon = GetIcon();
        Color = GetColor();
    }
}