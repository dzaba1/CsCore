using Dzaba.Utils;
using System;

namespace Dzaba.Mvvm
{
    public class RelayCommand<T> : RelayCommandBase
    {
        private Action<T> execute;
        private Predicate<T> canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            Require.NotNull(execute, nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            T realParam = (T)parameter;
            return canExecute == null ? true : canExecute(realParam);
        }

        public override void Execute(object parameter)
        {
            T realParam = (T)parameter;
            execute(realParam);
        }
    }

    public class RelayCommand : RelayCommandBase
    {
        private Action execute;
        private Func<bool> canExecute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            Require.NotNull(execute, nameof(execute));

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute();
        }

        public override void Execute(object parameter)
        {
            execute();
        }
    }
}
