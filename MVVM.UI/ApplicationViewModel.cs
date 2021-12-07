namespace MVVM.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
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
        private bool _peopleTabTabSelected;
        private GroupViewModel _selectedGroup;
        private PersonViewModel _selectedPerson;

        public ApplicationViewModel(List<PersonViewModel> peopleVm,
            List<GroupViewModel> groupsVm, IService<IGroup> groupService)
        {
            _groupService = groupService;
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

            DeletePersonCommand = new RelayCommand(obj =>
                {
                    if (obj is PersonViewModel person)
                        People.Remove(person);
                },
                obj => People.Any());

            CreateGroupCommand = new RelayCommand(obj =>
            {
                var vm = new GroupViewModel(null);
                Groups.Add(vm);
                SelectedGroup = vm;
            });

            DeleteGroupCommand = new RelayCommand(obj =>
                {
                    if (obj is GroupViewModel group)
                        Groups.Remove(group);
                },
                obj => Groups.Any());
            UpdatePersonCommand = new RelayCommand(obj => SelectedPerson.ApplyFrom(SelectedPerson, Operation.Update),
                obj => string.IsNullOrWhiteSpace(SelectedPerson.Error));
            UpdateGroupCommand = new RelayCommand(obj => SelectedGroup.ApplyFrom(SelectedGroup, Operation.Update),
                obj => string.IsNullOrWhiteSpace(SelectedGroup.Error));
        }
        public RelayCommand UpdatePersonCommand { get; }
        public RelayCommand UpdateGroupCommand { get; }


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


        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public static string WindowTitle => "GoogleContacts";

        private async Task Test(object sender, EventArgs eventArgs)
        {
            switch (sender)
            {
                case GroupViewModel group:
                {
                    var result = await Update(group, _groupService);
                    ValidateResult(result);
                    //Groups.Remove(@group);
                    return;
                }
                case PersonViewModel person:
                    break;
            }
        }

        private static async Task<IContact> Update<T>(T contact, IService<T> service) where T : IContact
        {
            return contact.Operation switch
            {
                Operation.Create => await service.CreateAsync(contact),
                Operation.Update => await service.UpdateAsync(contact),
                Operation.Delete => await service.DeleteAsync(contact),
                Operation.None => await Task.FromResult(new Contact()),
                _ => throw new ArgumentOutOfRangeException()
            };
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