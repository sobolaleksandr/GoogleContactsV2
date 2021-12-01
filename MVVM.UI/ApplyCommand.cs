namespace MVVM.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    using MVVM.Models;

    public class ApplyCommand : ICommand
    {
        private readonly IContact _contact;

        public ApplyCommand(IContact contact)
        {
            _contact = contact;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is IDataErrorInfo vm)
                return string.IsNullOrWhiteSpace(vm.Error);

            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!(parameter is IContact contact))
                return;

            _contact.ApplyFrom(contact);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}