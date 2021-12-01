namespace MVVM.UI
{
    using System;

    using MVVM.Models;

    public class ContactViewModel : ViewModelBase, IContact
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
                throw new ArgumentNullException(nameof(contact));

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
        /// Наименование модели.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ETag { get; set; }

        public string ResourceName { get; set; }

        public virtual void ApplyFrom(IContact contact)
        {
            ResourceName = contact.ResourceName;
            ETag = contact.ETag;
            Name = contact switch
            {
                IPerson person => person.GivenName + " (" + person.PhoneNumber + ")",
                IGroup group => group.FormattedName + " (" + group.MemberCount + ")",
                _ => Name,
            };
        }
    }
}