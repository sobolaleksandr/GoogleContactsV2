namespace MVVM.UI.ViewModels
{
    using MVVM.Models;

    /// <summary>
    /// Базовая вью-модель контакта.
    /// </summary>
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

        /// <summary>
        /// Вью-модель изменена.
        /// </summary>
        public bool IsChanged { get; set; }

        /// <summary>
        /// Вью-модель создана.
        /// </summary>
        public bool IsCreated { get; private set; }

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

        /// <inheritdoc />
        public string ETag { get; private set; }

        /// <inheritdoc />
        public string ResourceName { get; private set; }

        /// <summary>
        /// Принять изменения из вне.
        /// </summary>
        /// <param name="contact"> Контакт. </param>
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

        /// <inheritdoc />
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            IsChanged = true;
        }

        /// <summary>
        /// Установить свойства объекта.
        /// </summary>
        /// <param name="contact"></param>
        protected abstract void SetProperties(IContact contact);
    }
}