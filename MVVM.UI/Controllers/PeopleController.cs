namespace MVVM.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.Commands;
    using MVVM.UI.ViewModels;

    /// <summary>
    /// Контроллер для <see cref="PersonViewModel"/>.
    /// </summary>
    public class PeopleController : ViewModelBase
    {
        /// <summary>
        /// Сервис для работы с <see cref="IGroup"/>.
        /// </summary>
        private readonly IService<IGroup> _groupService;

        /// <summary>
        /// Сервис для работы с <see cref="IPerson"/>.
        /// </summary>
        private readonly IService<IPerson> _peopleService;

        /// <summary>
        /// Спиоск вью-моделей групп.
        /// </summary>
        private List<GroupViewModel> _groupsVm;

        /// <summary>
        /// Поле свойтсва <see cref="SelectedPerson"/>.
        /// </summary>
        private PersonViewModel _selectedPerson;

        /// <summary>
        /// Контроллер для <see cref="PersonViewModel"/>.
        /// </summary>
        /// <param name="unitOfWork"> Единица работы. </param>
        public PeopleController(IUnitOfWork unitOfWork)
        {
            People = new ObservableCollection<PersonViewModel>();
            CreateCommand =
                new RelayCommand(CreatePerson, obj => !SelectedPerson?.IsCreated == true || !People.Any());

            DeleteCommand = new AsyncCommand(DeletePersonAsync, obj => People.Any());
            UpdateCommand = new AsyncCommand(UpdatePersonAsync,
                obj => string.IsNullOrWhiteSpace(SelectedPerson?.Error) && SelectedPerson?.IsChanged == true);

            _groupService = unitOfWork.GroupService;
            _peopleService = unitOfWork.PeopleService;
        }

        /// <summary>
        /// Команда создания пользователя.
        /// </summary>
        public RelayCommand CreateCommand { get; }

        /// <summary>
        /// Команда удаления пользователя.
        /// </summary>
        public AsyncCommand DeleteCommand { get; }

        /// <summary>
        /// Контроллер для <see cref="GroupViewModel"/>.
        /// </summary>
        public GroupController GroupController { get; set; }

        /// <summary>
        /// Контакты. 
        /// </summary>
        public ObservableCollection<PersonViewModel> People { get; }

        /// <summary>
        /// Выбранный пользователь.
        /// </summary>
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
        /// Команда обновления пользователя.
        /// </summary>
        public AsyncCommand UpdateCommand { get; }

        /// <summary>
        /// Обновить пользователей асинхронно.
        /// </summary>
        public async Task UpdatePeopleAsync()
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

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        private void CreatePerson(object sender, EventArgs eventArgs)
        {
            var vm = new PersonViewModel(null, _groupsVm ?? new List<GroupViewModel>());
            People.Add(vm);
            SelectedPerson = vm;
        }

        /// <summary>
        /// Удалить пользователя асинхронно.
        /// </summary>
        /// <param name="personViewModel"> Вью-модель пользователя. </param>
        /// <returns> <see langword="true"/> если получилось удалить, иначе <see langword="false"/>. </returns>
        private async Task<bool> DeleteAsync(PersonViewModel personViewModel)
        {
            if (personViewModel.IsCreated)
                return false;

            var result = await _peopleService.DeleteAsync(personViewModel);
            return !result.Validate();
        }

        /// <summary>
        /// Удалить пользователя асинхронно.
        /// </summary>
        private async Task DeletePersonAsync()
        {
            if (await DeleteAsync(SelectedPerson))
                return;

            People.Remove(SelectedPerson);
            SelectedPerson = People.FirstOrDefault();
            await GroupController.UpdateGroupsAsync();
        }

        /// <summary>
        /// Обновить пользователя асинхронно.
        /// </summary>
        private async Task UpdatePersonAsync()
        {
            SelectedPerson.IsChanged = false;
            var result = SelectedPerson.IsCreated
                ? await _peopleService.CreateAsync(SelectedPerson)
                : await _peopleService.UpdateAsync(SelectedPerson);

            if (!result.Validate())
                return;

            SelectedPerson.ApplyFrom(result);
            await GroupController.UpdateGroupsAsync();
        }
    }
}