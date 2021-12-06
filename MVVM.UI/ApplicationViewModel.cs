namespace MVVM.UI
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using MVVM.Models;

    /// <summary>
    /// Вью-модель приложения. 
    /// </summary>
    public class ApplicationViewModel : ViewModelBase
    {
        private bool _peopleTabTabSelected;
        private GroupViewModel _selectedGroup;
        private PersonViewModel _selectedPerson;

        public ApplicationViewModel(List<PersonViewModel> peopleVm,
            List<GroupViewModel> groupsVm, bool peopleTabSelected)
        {
            People = new ObservableCollection<PersonViewModel>(peopleVm);
            Groups = new ObservableCollection<GroupViewModel>(groupsVm);

            SelectedPerson = peopleVm.FirstOrDefault();
            SelectedGroup = groupsVm.FirstOrDefault();
            PeopleTabTabSelected = peopleTabSelected;

            CreatePersonCommand = new RelayCommand(obj =>
            {
                var vm = new PersonViewModel(null, Groups);
                People.Add(vm);
                SelectedPerson = vm;
            });

            DeletePersonCommand = new RelayCommand(obj =>
                {
                    if (obj is PersonViewModel person)
                        People.Remove(person);
                },
                obj => People.Count > 0);
            UpdatePersonCommand = new UpdateCommand(SelectedPerson, Operation.Update);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            SelectedPerson.IsChanged = true;
            base.OnPropertyChanged(propertyName);
            UpdatePersonCommand?.RaiseCanExecuteChanged();
        }

        public RelayCommand DeletePersonCommand { get; }
        public UpdateCommand UpdatePersonCommand { get; }
        public RelayCommand CreatePersonCommand { get; }

        public ObservableCollection<GroupViewModel> Groups { get; }

        public static string GroupTitle => "Группы";

        /// <summary>
        /// Контакты. 
        /// </summary>
        public ObservableCollection<PersonViewModel> People { get; }

        public bool PeopleTabTabSelected
        {
            get => _peopleTabTabSelected;
            set
            {
                _peopleTabTabSelected = value;
                OnPropertyChanged();
            }
        }

        public static string PeopleTitle => "Контакты";

        public GroupViewModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel SelectedPerson
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
    }
}