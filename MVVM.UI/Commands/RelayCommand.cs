namespace MVVM.UI.Commands
{
    using System;

    /// <summary>
    /// Команда.
    /// </summary>
    public class RelayCommand : CommandBase
    {
        /// <summary>
        /// Команда.
        /// </summary>
        /// <param name="onExecute"> Событие при выполнении команды. </param>
        /// <param name="canExecute">  Функция проверки выполнения команды. </param>
        public RelayCommand(EventHandler onExecute, Func<object, bool> canExecute = null) : base(canExecute)
        {
            OnExecute += onExecute;
        }

        /// <inheritdoc />
        public override void Execute(object parameter)
        {
            OnExecute?.Invoke(parameter, EventArgs.Empty);
        }

        /// <summary>
        /// Событие при выполнении команды.
        /// </summary>
        public event EventHandler OnExecute;
    }
}