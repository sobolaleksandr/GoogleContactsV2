namespace MVVM.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

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
            List<GroupViewModel> groupsVm, IService<IGroup> groupService, IService<IPerson> peopleService)
        {
            People = new ObservableCollection<PersonViewModel>(peopleVm);
            Groups = new ObservableCollection<GroupViewModel>(groupsVm);

            SelectedPerson = peopleVm.FirstOrDefault();
            SelectedGroup = groupsVm.FirstOrDefault();

            CreatePersonCommand = new RelayCommand(obj =>
            {
                var vm = new PersonViewModel(null, Groups);
                People.Add(vm);
                SelectedPerson = vm;
            });

            DeletePersonCommand = new RelayCommand(async obj =>
                {
                    var result = await peopleService.DeleteAsync(SelectedPerson);
                    if (ValidateResult(result))
                        People.Remove(SelectedPerson);
                },
                obj => People.Any());

            CreateGroupCommand = new RelayCommand(obj =>
            {
                var vm = new GroupViewModel(null);
                Groups.Add(vm);
                SelectedGroup = vm;
            });

            DeleteGroupCommand = new RelayCommand(async obj =>
                {
                    var result = await groupService.DeleteAsync(SelectedGroup);
                    if (ValidateResult(result))
                        Groups.Remove(SelectedGroup);
                },
                obj => Groups.Any());

            UpdatePersonCommand = new RelayCommand(async obj =>
                {
                    var result = SelectedPerson.IsCreated
                        ? await peopleService.CreateAsync(SelectedPerson)
                        : await peopleService.UpdateAsync(SelectedPerson);

                    if (ValidateResult(result))
                        SelectedPerson.ApplyFrom(result);
                },
                obj => string.IsNullOrWhiteSpace(SelectedPerson.Error));

            UpdateGroupCommand = new RelayCommand(async obj =>
                {
                    var result = SelectedGroup.IsCreated
                        ? await groupService.CreateAsync(SelectedGroup)
                        : await groupService.UpdateAsync(SelectedGroup);

                    if (ValidateResult(result))
                        SelectedGroup.ApplyFrom(result);
                },
                obj => string.IsNullOrWhiteSpace(SelectedGroup.Error));
        }

        public RelayCommand CreateGroupCommand { get; }

        public RelayCommand CreatePersonCommand { get; }

        public RelayCommand DeleteGroupCommand { get; }

        public RelayCommand DeletePersonCommand { get; }

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

        public RelayCommand UpdateGroupCommand { get; }
        public RelayCommand UpdatePersonCommand { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        /// <summary>
        /// Проверка ошибки результата.
        /// </summary>
        /// <param name="error"> Сообщение об ошибке. </param>
        /// <returns> True, если ошибка пуста, иначе вывод сообщения на экран. </returns>
        private static bool ValidateError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return true;

            MessageBox.Show(error);
            return false;
        }

        /// <summary>
        /// Проверка полученных результатов. 
        /// </summary>
        /// <param name="result"> Результат операции. </param>
        /// <returns> True, если проверка пройдена. </returns>
        private static bool ValidateResult(IContact result)
        {
            if (!(result is Contact contact))
                return false;

            var error = contact.Error;
            return ValidateError(error);
        }
    }
}