namespace MySphere.Framework.Foundation.ViewModel;

public abstract class ViewModelBase : ObservableObject
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; protected set; } = "DEFAULT!!!";
}