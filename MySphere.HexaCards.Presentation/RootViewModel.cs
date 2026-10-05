using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.HexaCards.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "HexaCards";
        Icon = GetIcon();
        Color = GetColor();
    }
}
