namespace MVVM.UI.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using MVVM.Models;

    public class PersonViewModel : ContactViewModel, IPerson, IDataErrorInfo
    {
        /// <summary>
        /// Поле свойства <see cref="Email"/>
        /// </summary>
        private string _email;

        /// <summary>
        /// Поле свойства <see cref="FamilyName"/>
        /// </summary>
        private string _familyName;

        /// <summary>
        /// Поле свойства <see cref="GivenName"/>
        /// </summary>
        private string _givenName;

        /// <summary>
        /// Поле свойства <see cref="Organization"/>
        /// </summary>
        private string _organization;

        /// <summary>
        /// Поле свойства <see cref="PhoneNumber"/>
        /// </summary>
        private string _phoneNumber;

        /// <summary>
        /// Поле свойства <see cref="SelectedGroup"/>
        /// </summary>
        private IContact _selectedGroup;

        /// <summary>
        /// Вью-модель <see cref="IPerson"/>
        /// </summary>
        /// <param name="contact"> </param>
        /// <param name="groups"> Группы. </param>
        public PersonViewModel(IContact contact, ObservableCollection<GroupViewModel> groups) : base(contact)
        {
            Groups = groups;
            if (contact == null)
            {
                CreateContact();
                return;
            }

            SetProperties(contact);
            if (string.IsNullOrEmpty(GroupResourceName))
                return;

            SelectedGroup = groups.FirstOrDefault(group => group.ResourceName == GroupResourceName);
        }

        /// <summary>
        /// Наименование атрибута <see cref="Email"/>
        /// </summary>
        public static string EmailTitle => "Адрес электронной почты";

        /// <summary>
        /// Наименование атрибута <see cref="FamilyName"/>
        /// </summary>
        public static string FamilyNameTitle => "Фамилия";

        /// <summary>
        /// Наименование атрибута <see cref="GivenName"/>
        /// </summary>
        public static string GivenNameTitle => "Имя";

        /// <summary>
        /// Группы.
        /// </summary>
        public ObservableCollection<GroupViewModel> Groups { get; }

        /// <summary>
        /// Наименование атрибута <see cref="Groups"/>
        /// </summary>
        public static string GroupTitle => "Группы";

        /// <summary>
        /// Наименование атрибута <see cref="Organization"/>
        /// </summary>
        public static string OrganizationTitle => "Организация";

        /// <summary>
        /// Наименование атрибута <see cref="PhoneNumber"/>
        /// </summary>
        public static string PhoneNumberTitle => "Номер телефона";

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public IContact SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                GroupResourceName = value?.ResourceName;
                OnPropertyChanged();
            }
        }

        public string Error => this[nameof(Email)] + this[nameof(FamilyName)] + this[nameof(PhoneNumber)] +
                               this[nameof(SelectedGroup)] + this[nameof(Organization)];

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                            error = $"Поле {EmailTitle} не должно быть пустым!";
                        break;
                    case nameof(FamilyName):
                        if (string.IsNullOrEmpty(FamilyName))
                            error = $"Поле {FamilyNameTitle} не должно быть пустым!";
                        break;
                    case nameof(PhoneNumber):
                        if (string.IsNullOrEmpty(PhoneNumber))
                            error = $"Поле {PhoneNumberTitle} не должно быть пустым!";
                        break;
                    case nameof(GivenName):
                        if (string.IsNullOrEmpty(GivenName))
                            error = $"Поле {GivenNameTitle} не должно быть пустым!";
                        break;
                    case nameof(SelectedGroup):
                        error = string.Empty;
                        break;
                    case nameof(Organization):
                        if (string.IsNullOrEmpty(Organization))
                            error = $"Поле {OrganizationTitle} не должно быть пустым!";
                        break;
                }

                return error;
            }
        }

        /// <summary>
        /// Адрес электронной почты. 
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string GivenName
        {
            get => _givenName;
            set
            {
                _givenName = value;
                OnPropertyChanged();
            }
        }

        public string GroupResourceName { get; private set; }

        /// <summary>
        /// Организация. 
        /// </summary>
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public override void ApplyFrom(IContact contact)
        {
            base.ApplyFrom(contact);
            SetProperties(contact);
        }

        private void CreateContact()
        {
            Email = string.Empty;
            FamilyName = string.Empty;
            GivenName = string.Empty;
            Organization = string.Empty;
            PhoneNumber = string.Empty;
            GroupResourceName = string.Empty;
            ResourceName = string.Empty;
            IsCreated = true;
        }

        private void SetProperties(IContact contact)
        {
            if (!(contact is IPerson person))
                return;

            Email = person.Email;
            FamilyName = person.FamilyName;
            GivenName = person.GivenName;
            Organization = person.Organization;
            PhoneNumber = person.PhoneNumber;
            GroupResourceName = person.GroupResourceName;
        }
    }
}