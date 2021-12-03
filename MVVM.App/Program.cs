﻿namespace MVVM.App
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using MVVM.Models;
    using MVVM.Services;
    using MVVM.UI;

    internal static class Program
    {
        private const bool DEBUG = true;
        private static bool _peopleSelected;
        private static async Task<ApplicationViewModel> CreateApplicationVm(UnitOfWork unitOfWork)
        {
            var groupService = unitOfWork.GroupService;
            var groups = await groupService.GetAsync();
            var groupsVm = groups.Select(group => new GroupViewModel(group)).ToList();

            var peopleService = unitOfWork.PeopleService;
            var people = await peopleService.GetAsync();
            var peopleVm = people.Select(person => new PersonViewModel(person, groupsVm)).ToList();

            return new ApplicationViewModel(peopleVm, groupsVm, _peopleSelected);
        }

        [STAThread]
        private static void Main()
        {
            using var unitOfWork = new UnitOfWork(DEBUG);
            while (true)
            {
                if (unitOfWork.Disposed)
                    return;

                var vm = Task.Run(async () => await CreateApplicationVm(unitOfWork)).GetAwaiter().GetResult();
                var window = new MainWindow
                {
                    DataContext = vm
                };

                if (window.ShowDialog() != true)
                    return;

                _peopleSelected = vm.PeopleTabTabSelected;
                Task.Run(async () => await UpdateData(vm, unitOfWork)).GetAwaiter().GetResult();
            }
        }

        private static async Task<Contact> Update<T>(T contact, IService<T> service) where T : IContact
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


        private static async Task UpdateData(ApplicationViewModel applicationViewModel, UnitOfWork unitOfWork)
        {
            var groupService = unitOfWork.GroupService;
            var selectedGroup = applicationViewModel.SelectedGroup;
            await UpdateData(selectedGroup, groupService);

            var peopleService = unitOfWork.PeopleService;
            var selectedPerson = applicationViewModel.SelectedPerson;
            await UpdateData(selectedPerson, peopleService);
        }

        private static async Task UpdateData<T>(T selectedGroup, IService<T> groupService) where T : IContact
        {
            var result = await Update(selectedGroup, groupService);
            ValidateResult(result);
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
        private static bool ValidateResult(Contact result)
        {
            var error = result.Error;
            return ValidateError(error);
        }
    }
}