namespace MVVM.UI
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public abstract class RelayCommandBase : ICommand
    {
        private readonly Func<object, bool> _canExecute;

        protected RelayCommandBase(Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
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

        public abstract void Execute(object parameter);
    }

    public class RelayCommand : RelayCommandBase
    {
        public RelayCommand(EventHandler onExecute, Func<object, bool> canExecute) : base(canExecute)
        {
            OnExecute += onExecute ?? throw new ArgumentNullException(nameof(onExecute));
        }

        public override void Execute(object parameter)
        {
            OnExecute?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnExecute;
    }

    public class RelayCommandAsync : RelayCommandBase
    {
        private readonly Func<Task> _onExecuteAsync;

        public RelayCommandAsync(Func<Task> onExecuteAsync, Func<object, bool> canExecute) : base(canExecute)
        {
            _onExecuteAsync = onExecuteAsync ?? throw new ArgumentNullException(nameof(onExecuteAsync));
        }

        public override void Execute(object parameter)
        {
            _onExecuteAsync.Invoke();
        }
    }
}