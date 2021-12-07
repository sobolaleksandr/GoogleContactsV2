﻿namespace MVVM.UI
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    using MVVM.Models;

    public class UpdateCommand : ICommand
    {
        private readonly ContactViewModel _contact;
        private readonly Operation _operation;

        public UpdateCommand(ContactViewModel contact, Operation operation)
        {
            _contact = contact;
            _operation = operation;
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
            _contact.ApplyFrom(_contact, _operation);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}