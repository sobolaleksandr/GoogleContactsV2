namespace MVVM.UI
{
    using System.Collections.Generic;
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
        public PersonViewModel(IContact contact, List<GroupViewModel> groups) : base(contact)
        {
            if (!(contact is IPerson person))
                return;

            Email = person.Email;
            FamilyName = person.FamilyName;
            GivenName = person.GivenName;
            Organization = person.Organization;
            PhoneNumber = person.PhoneNumber;
            Groups = new ObservableCollection<GroupViewModel>(groups);
            var groupResourceName = contact.ResourceName;
            if (string.IsNullOrEmpty(groupResourceName))
                return;

            SelectedGroup = groups.FirstOrDefault(group => group.ResourceName == groupResourceName);
        }

        /// <summary>
        /// Наименование атрибута <see cref="Email"/>
        /// </summary>
        public static string EmailTitle => "Адрес электронной почты";

        public string ETag { get; set; }

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
        public ObservableCollection<GroupViewModel> Groups { get; set; }

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

        public string ResourceName { get; set; }

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public IContact SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "Окно редактирования контакта";

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
    }
}