using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.ChessSharp.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel()
    {
        Name = "Chess#";
        Icon = GetIcon("ChessSharp");
        Color = GetColor("ChessSharp");
    }
}