namespace MVVM.UI
{
    using System.Runtime.CompilerServices;

    using MVVM.Models;

    public abstract class ContactViewModel : ViewModelBase, IContact
    {
        /// <summary>
        /// Поле свойства <see cref="Name"/>
        /// </summary>
        private string _name;

        /// <summary>
        /// Базовая модель контакта.
        /// </summary>
        protected ContactViewModel(IContact contact)
        {
            UpdateCommand = new UpdateCommand(this, Operation.Update);
            if (contact == null)
                return;

            ApplyFrom(contact);
        }

        private void ApplyFrom(IContact contact)
        {
            ResourceName = contact.ResourceName;
            ETag = contact.ETag;
            Name = contact switch
            {
                IPerson person => person.GivenName + " (" + person.PhoneNumber + ")",
                IGroup group => @group.FormattedName + " (" + @group.MemberCount + ")",
                _ => Name,
            };
        }

        public UpdateCommand UpdateCommand { get; }

        /// <summary>
        /// Наименование модели.
        /// </summary>
        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            UpdateCommand?.RaiseCanExecuteChanged();
        }

        public string ETag { get; set; }

        public Operation Operation { get; protected set; } = Operation.None;

        public string ResourceName { get; set; }

        public virtual void ApplyFrom(IContact contact, Operation operation)
        {
            if (Operation != Operation.Create)
                Operation = operation;
            ApplyFrom(contact);
        }
    }
}