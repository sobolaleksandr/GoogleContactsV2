namespace MVVM.UI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.Commands;
    using MVVM.UI.ViewModels;

    /// <summary>
    /// Контроллер для <see cref="GroupViewModel"/>.
    /// </summary>
    public class GroupController : ViewModelBase
    {
        /// <summary>
        /// Сервис для работы с <see cref="IGroup"/>.
        /// </summary>
        private readonly IService<IGroup> _groupService;

        /// <summary>
        /// Поле свойтсва <see cref="SelectedGroup"/>.
        /// </summary>
        private GroupViewModel _selectedGroup;

        /// <summary>
        /// Контроллер для <see cref="GroupViewModel"/>.
        /// </summary>
        /// <param name="unitOfWork"> Единица работы. </param>
        public GroupController(IUnitOfWork unitOfWork)
        {
            Groups = new ObservableCollectionRange<GroupViewModel>();
            CreateCommand =
                new RelayCommand(CreateGroup, obj => !SelectedGroup?.IsCreated == true || !Groups.Any());

            DeleteCommand = new AsyncCommand(DeleteGroupAsync, obj => Groups.Any());
            UpdateCommand = new AsyncCommand(UpdateGroupAsync,
                obj => string.IsNullOrWhiteSpace(SelectedGroup?.Error) && SelectedGroup?.IsChanged == true);

            _groupService = unitOfWork.GroupService;
        }

        /// <summary>
        /// Команда создания группы.
        /// </summary>
        public RelayCommand CreateCommand { get; }

        /// <summary>
        /// Команда удаления группы.
        /// </summary>
        public AsyncCommand DeleteCommand { get; }

        /// <summary>
        /// Группы.
        /// </summary>
        public ObservableCollectionRange<GroupViewModel> Groups { get; }

        /// <summary>
        /// Контроллер для <see cref="PersonViewModel"/>.
        /// </summary>
        public PeopleController PeopleController { get; set; }

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        public GroupViewModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда обновления группы.
        /// </summary>
        public AsyncCommand UpdateCommand { get; }

        /// <summary>
        /// Обновить группы асинхронно.
        /// </summary>
        public async Task UpdateGroupsAsync()
        {
            var groups = await _groupService.GetAsync();
            var groupsVm = groups.Select(group => new GroupViewModel(group)).ToList();

            var resourceNames = PeopleController.People.Select(person => person.SelectedGroup.ResourceName).ToList();
            Groups.AddRange(groupsVm);
            SelectedGroup = Groups.FirstOrDefault();
            PeopleController.UpdateGroups(resourceNames);
        }

        /// <summary>
        /// Создать группу.
        /// </summary>
        private void CreateGroup(object sender, EventArgs eventArgs)
        {
            var vm = new GroupViewModel(null);
            Groups.Add(vm);
            SelectedGroup = vm;
        }

        /// <summary>
        /// Удалить группу асинхронно.
        /// </summary>
        /// <param name="groupViewModel"> Вью-модель группы. </param>
        /// <returns> <see langword="true"/> если получилось удалить, иначе <see langword="false"/>. </returns>
        private async Task<bool> DeleteAsync(GroupViewModel groupViewModel)
        {
            if (groupViewModel.IsCreated)
                return false;

            var result = await _groupService.DeleteAsync(groupViewModel);
            return !result.Validate();
        }

        /// <summary>
        /// Удалить группу асинхронно.
        /// </summary>
        private async Task DeleteGroupAsync()
        {
            if (await DeleteAsync(SelectedGroup))
                return;

            Groups.Remove(SelectedGroup);
            SelectedGroup = Groups.FirstOrDefault();
            await PeopleController.UpdatePeopleAsync();
        }

        /// <summary>
        /// Обновить группу асинхронно.
        /// </summary>
        private async Task UpdateGroupAsync()
        {
            SelectedGroup.IsChanged = false;
            var result = SelectedGroup.IsCreated
                ? await _groupService.CreateAsync(SelectedGroup)
                : await _groupService.UpdateAsync(SelectedGroup);

            if (!result.Validate())
                return;

            SelectedGroup.ApplyFrom(result);
            await PeopleController.UpdatePeopleAsync();
        }
    }
}