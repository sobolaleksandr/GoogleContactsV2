namespace MVVM.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using MVVM.Models;

    /// <summary>
    /// Вью-модель приложения. 
    /// </summary>
    public class ApplicationViewModel : ViewModelBase
    {
        private readonly IService<IGroup> _groupService;
        private readonly IService<IPerson> _peopleService;
        private List<GroupViewModel> _groupsVm;
        private bool _peopleTabTabSelected;
        private GroupViewModel _selectedGroup;
        private PersonViewModel _selectedPerson;

        public ApplicationViewModel(IUnitOfWork unitOfWork)
        {
            _groupService = unitOfWork.GroupService;
            _peopleService = unitOfWork.PeopleService;

            People = new ObservableCollection<PersonViewModel>();
            Groups = new ObservableCollection<GroupViewModel>();

            CreatePersonCommand =
                new RelayCommand(CreatePerson, obj => !SelectedPerson?.IsCreated == true || !People.Any());

            CreateGroupCommand =
                new RelayCommand(CreateGroup, obj => !SelectedGroup?.IsCreated == true || !Groups.Any());

            DeletePersonCommand = new RelayCommandAsync(DeletePerson, obj => People.Any());

            DeleteGroupCommand = new RelayCommandAsync(DeleteGroup, obj => Groups.Any());

            UpdatePersonCommand = new RelayCommandAsync(UpdatePerson,
                obj => string.IsNullOrWhiteSpace(SelectedPerson?.Error) && SelectedPerson?.IsChanged == true);

            UpdateGroupCommand = new RelayCommandAsync(UpdateGroup,
                obj => string.IsNullOrWhiteSpace(SelectedGroup?.Error) && SelectedGroup?.IsChanged == true);

            UpdateCommand = new RelayCommandAsync(async () =>
            {
                await UpdateGroups();
                await UpdatePeople();
            }, null);
        }

        public RelayCommand CreateGroupCommand { get; }

        public RelayCommand CreatePersonCommand { get; }

        public RelayCommandAsync DeleteGroupCommand { get; }

        public RelayCommandAsync DeletePersonCommand { get; }

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

        public RelayCommandAsync UpdateCommand { get; }

        public RelayCommandAsync UpdateGroupCommand { get; }

        public RelayCommandAsync UpdatePersonCommand { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        public async Task UpdateGroups()
        {
            var groups = await _groupService.GetAsync();
            _groupsVm = groups.Select(group => new GroupViewModel(group)).ToList();

            if (Groups.SequenceEqual(_groupsVm))
                return;

            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(new GroupViewModel(group));
            }

            SelectedGroup = Groups.FirstOrDefault();
        }

        public async Task UpdatePeople()
        {
            var groups = await _groupService.GetAsync();
            _groupsVm = groups.Select(group => new GroupViewModel(group)).ToList();

            var people = await _peopleService.GetAsync();
            People.Clear();
            foreach (var person in people)
            {
                People.Add(new PersonViewModel(person, _groupsVm));
            }

            SelectedPerson = People.FirstOrDefault();
        }

        private void CreateGroup(object sender, EventArgs eventArgs)
        {
            var vm = new GroupViewModel(null);
            Groups.Add(vm);
            SelectedGroup = vm;
        }

        private void CreatePerson(object sender, EventArgs eventArgs)
        {
            var vm = new PersonViewModel(null, _groupsVm ?? new List<GroupViewModel>());
            People.Add(vm);
            SelectedPerson = vm;
        }

        private async Task DeleteGroup()
        {
            if (!SelectedGroup.IsCreated)
            {
                var result = await _groupService.DeleteAsync(SelectedGroup);
                if (!ValidateResult(result))
                    return;
            }

            Groups.Remove(SelectedGroup);
            SelectedGroup = Groups.FirstOrDefault();
        }

        private async Task DeletePerson()
        {
            if (!SelectedPerson.IsCreated)
            {
                var result = await _peopleService.DeleteAsync(SelectedPerson);
                if (!ValidateResult(result))
                    return;
            }

            People.Remove(SelectedPerson);
            SelectedPerson = People.FirstOrDefault();
        }

        private async Task UpdateGroup()
        {
            SelectedGroup.IsChanged = false;
            var result = SelectedGroup.IsCreated
                ? await _groupService.CreateAsync(SelectedGroup)
                : await _groupService.UpdateAsync(SelectedGroup);

            if (!ValidateResult(result))
                return;

            SelectedGroup.ApplyFrom(result);
            await UpdatePeople();
        }

        private async Task UpdatePerson()
        {
            SelectedPerson.IsChanged = false;
            var result = SelectedPerson.IsCreated
                ? await _peopleService.CreateAsync(SelectedPerson)
                : await _peopleService.UpdateAsync(SelectedPerson);

            if (!ValidateResult(result))
                return;

            SelectedPerson.ApplyFrom(result);
            await UpdateGroups();
        }

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
        /// <returns> <see langword="true"/>, если проверка пройдена. </returns>
        private static bool ValidateResult(IContact result)
        {
            if (!(result is Contact contact))
                return false;

            var error = contact.Error;
            return ValidateError(error);
        }
    }
}