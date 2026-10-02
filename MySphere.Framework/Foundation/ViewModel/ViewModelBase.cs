using System.Windows.Media;

namespace MySphere.Framework.Foundation.ViewModel;

public abstract class ViewModelBase : ObservableObject
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    private string _name = "[ NO NAME ]";

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private Geometry? _icon;

    public Geometry? Icon
    {
        get => _icon;
        set
        {
            _icon = value;
            OnPropertyChanged(nameof(Icon));
        }
    }
}