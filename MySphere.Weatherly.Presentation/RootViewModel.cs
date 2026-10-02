using MySphere.Framework.Foundation.ViewModel;

namespace MySphere.Weatherly.Presentation;

public sealed class RootViewModel : ViewModelBase
{
    public RootViewModel() =>
        Name = "Weatherly";
}