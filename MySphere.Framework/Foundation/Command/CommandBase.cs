using System.Windows.Input;

namespace MySphere.Framework.Foundation.Command;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public abstract bool CanExecute(object? parameter);

    public abstract void Execute(object? parameter);

    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}