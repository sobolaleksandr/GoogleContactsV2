namespace MVVM.UI.Commands
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Асинхронная команда.
    /// </summary>
    public class AsyncCommand : CommandBase
    {
        /// <summary>
        /// Асинхронная функция.
        /// </summary>
        private readonly Func<Task> _command;

        /// <summary>
        /// Асинхронная команда.
        /// </summary>
        /// <param name="command"> Асинхронная функция. </param>
        /// <param name="canExecute"> Функция проверки выполнения команды. </param>
        public AsyncCommand(Func<Task> command, Func<object, bool> canExecute = null) : base(canExecute)
        {
            _command = command;
        }

        /// <inheritdoc />
        public override async void Execute(object parameter)
        {
            await _command();
        }
    }
}