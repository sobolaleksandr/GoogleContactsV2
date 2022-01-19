namespace MVVM.UI
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(EventHandler onExecute, Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
            OnExecute += onExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) != false;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            OnExecute?.Invoke(parameter, EventArgs.Empty);
        }

        public event EventHandler OnExecute;
    }
}