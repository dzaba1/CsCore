using Dzaba.Utils;
using System;

namespace Dzaba.Mvvm
{
    public class DelegateCommand<T> : DelegateCommandBase
    {
        private Action<T> execute;
        private Predicate<T> canExecute;

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            Require.NotNull(execute, nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            T realParam = (T)parameter;
            return canExecute?.Invoke(realParam) ?? true;
        }

        public override void Execute(object parameter)
        {
            T realParam = (T)parameter;
            execute(realParam);
        }
    }

    public class DelegateCommand : DelegateCommandBase
    {
        private Action execute;
        private Func<bool> canExecute;

        public DelegateCommand(Action execute)
            : this(execute, null)
        {

        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            Require.NotNull(execute, nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
