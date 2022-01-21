namespace MVVM.UI.ViewModels
{
    using MVVM.Models;
    using MVVM.UI.Commands;
    using MVVM.UI.Controllers;

    /// <summary>
    /// Вью-модель приложения. 
    /// </summary>
    public class ApplicationViewModel : ViewModelBase
    {
        /// <summary>
        /// Поле свойтсва <see cref="PeopleTabTabSelected"/>.
        /// </summary>
        private bool _peopleTabTabSelected;

        /// <summary>
        /// Вью-модель приложения. 
        /// </summary>
        /// <param name="unitOfWork"> Единица работы. </param>
        public ApplicationViewModel(IUnitOfWork unitOfWork)
        {
            PeopleController = new PeopleController(unitOfWork);
            GroupController = new GroupController(unitOfWork)
            {
                PeopleController = PeopleController,
            };
            PeopleController.GroupController = GroupController;

            UpdateCommand = new AsyncCommand(async () =>
            {
                await GroupController.UpdateGroupsAsync();
                await PeopleController.UpdatePeopleAsync();
            });
        }

        /// <summary>
        /// Контроллер для <see cref="GroupViewModel"/>.
        /// </summary>
        public GroupController GroupController { get; }

        /// <summary>
        /// Заголовок вкладки групп.
        /// </summary>
        public static string GroupTitle => "Группы";

        /// <summary>
        /// Контроллер для <see cref="PersonViewModel"/>.
        /// </summary>
        public PeopleController PeopleController { get; }

        /// <summary>
        /// Выбрана влкадка пользователей.
        /// </summary>
        public bool PeopleTabTabSelected
        {
            get => _peopleTabTabSelected;
            set
            {
                _peopleTabTabSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовок вкладки пользователей.
        /// </summary>
        public static string PeopleTitle => "Контакты";

        /// <summary>
        /// Команда обновления приложения.
        /// </summary>
        public AsyncCommand UpdateCommand { get; }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";
    }
}