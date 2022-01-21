namespace MVVM.UI.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Базовый класс команды.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Функция проверки выполнения команды.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Базовый класс команды.
        /// </summary>
        /// <param name="canExecute">  Функция проверки выполнения команды. </param>
        protected CommandBase(Func<object, bool> canExecute)
        {
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) != false;
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        public abstract void Execute(object parameter);
    }
}