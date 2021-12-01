namespace MVVM.UI
{
    using System.Collections.Generic;
    using System.Linq;

    using MVVM.Models;

    /// <summary>
    /// Вью-модель приложения. 
    /// </summary>
    public class ApplicationViewModel : ViewModelBase
    {
        private IContact _selectedGroup;
        private IContact _selectedPerson;
        private bool _peopleTabSelected;

        public ApplicationViewModel(IReadOnlyCollection<PersonViewModel> peopleVm,
            IReadOnlyCollection<GroupViewModel> groupsVm)
        {
            People.AddRange(peopleVm);
            Groups.AddRange(groupsVm);

            SelectedPerson = peopleVm.FirstOrDefault();
            SelectedGroup = groupsVm.FirstOrDefault();
        }

        public ObservableCollectionRange<ContactViewModel> Groups { get; set; } =
            new ObservableCollectionRange<ContactViewModel>();

        public static string GroupTitle => "Группы";

        /// <summary>
        /// Контакты. 
        /// </summary>
        public ObservableCollectionRange<ContactViewModel> People { get; } =
            new ObservableCollectionRange<ContactViewModel>();

        public static string PeopleTitle => "Контакты";

        public IContact SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        public IContact SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        public bool PeopleTabSelected
        {
            get => _peopleTabSelected;
            set
            {
                _peopleTabSelected = value;
                OnPropertyChanged();
            }
        }
    }
}