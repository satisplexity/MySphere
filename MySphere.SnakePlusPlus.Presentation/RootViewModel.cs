using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.SnakePlusPlus.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Snake++";
        Icon = GetIcon("SnakePlusPlus");
        Color = GetColor("SnakePlusPlus");
    }
}