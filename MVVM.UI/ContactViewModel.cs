namespace MVVM.UI
{
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
            if (contact == null)
                return;

            ResourceName = contact.ResourceName;
            ETag = contact.ETag;
            Name = contact switch
            {
                IPerson person => person.GivenName + " (" + person.PhoneNumber + ")",
                IGroup group => group.FormattedName + " (" + group.MemberCount + ")",
                _ => Name,
            };

            CreateCommand = new UpdateCommand(this, Operation.Create);
            UpdateCommand = new UpdateCommand(this, Operation.Update);
            DeleteCommand = new UpdateCommand(this, Operation.Delete);
        }

        public UpdateCommand CreateCommand { get; }

        public UpdateCommand DeleteCommand { get; }

        public bool IsChanged { get; set; }

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

        public UpdateCommand UpdateCommand { get; }

        public string ETag { get; set; }

        public Operation Operation { get; protected set; } = Operation.None;

        public string ResourceName { get; set; }

        public virtual void ApplyFrom(IContact contact, Operation operation)
        {
            if (Operation != Operation.Create)
                Operation = operation;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            IsChanged = true;
            base.OnPropertyChanged(propertyName);
            UpdateCommand?.RaiseCanExecuteChanged();
        }
    }
}