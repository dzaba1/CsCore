using System;
using System.Windows.Input;

namespace Dzaba.Mvvm
{
    public abstract class DelegateCommandBase : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
