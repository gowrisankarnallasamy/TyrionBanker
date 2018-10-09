using System;
using System.Windows.Input;

namespace TyrionBanker.FrontUI.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private Func<bool> _canExecute;
        private Action<T> _execute;

        public RelayCommand(Func<bool> canExecute, Action<T> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
