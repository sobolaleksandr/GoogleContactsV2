namespace MVVM.UI
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;

        private readonly Func<Task> _command;

        public AsyncCommand(Func<Task> command, Func<object, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _command = command;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) != false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await _command();
        }
    }
}