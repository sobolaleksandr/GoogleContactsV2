namespace MVVM.UI.ViewModels
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
            {
                IsCreated = true;
                return;
            }

            ResourceName = contact.ResourceName;
            ETag = contact.ETag;
            Name = contact switch
            {
                IPerson person => person.GivenName + " (" + person.PhoneNumber + ")",
                IGroup group => group.FormattedName + " (" + group.MemberCount + ")",
                _ => Name,
            };
        }

        public bool IsChanged { get; set; }

        public bool IsCreated { get; set; }

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

        public string ETag { get; set; }

        public string ResourceName { get; set; }

        public void ApplyFrom(IContact contact)
        {
            ResourceName = contact.ResourceName;
            ETag = contact.ETag;
            Name = contact switch
            {
                IPerson person => person.GivenName + " (" + person.PhoneNumber + ")",
                IGroup group => group.FormattedName + " (" + group.MemberCount + ")",
                _ => Name,
            };

            SetProperties(contact);
            IsChanged = false;
            IsCreated = false;
        }

        public override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            IsChanged = true;
        }

        protected abstract void SetProperties(IContact contact);
    }
}