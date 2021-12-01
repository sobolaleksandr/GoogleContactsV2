namespace MVVM.UI
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public sealed class RelayCommand : ICommand
    {
        public RelayCommand()
        {
        }

        public RelayCommand(EventHandler onExecute)
        {
            OnExecute += onExecute ?? throw new ArgumentNullException(nameof(onExecute));
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        [DebuggerStepThrough]
        public void Execute(object parameter)
        {
            var arguments = new ExecuteRelayCommandEventArgs(parameter);
            OnExecute?.Invoke(this, arguments);
        }

        public event EventHandler OnExecute;
    }
}