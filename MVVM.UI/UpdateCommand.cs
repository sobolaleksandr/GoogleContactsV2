namespace MVVM.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    using MVVM.UI.ViewModels;

    public class UpdateCommand : ICommand
    {
        private readonly ContactViewModel _contact;

        public UpdateCommand(ContactViewModel contact)
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
            _contact.ApplyFrom(_contact);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}