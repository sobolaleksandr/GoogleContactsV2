namespace MVVM.App
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Services;
    using MVVM.UI;

    internal static class Program
    {
        private const bool DEBUG = true;
        
        [STAThread]
        private static void Main()
        {
            using var unitOfWork = new UnitOfWork(DEBUG);
            while (true)
            {
                var groupService = unitOfWork.GroupService;
                var groups = Task.Run(async () => await groupService.GetAsync()).GetAwaiter().GetResult();
                var groupsVm = groups.Select(group => new GroupViewModel(group)).ToList();

                var peopleService = unitOfWork.PeopleService;
                var people = Task.Run(async () => await peopleService.GetAsync()).GetAwaiter().GetResult();
                var peopleVm = people.Select(person => new PersonViewModel(person, groupsVm)).ToList();

                var vm = new ApplicationViewModel(peopleVm, groupsVm);
                var window = new MainWindow
                {
                    DataContext = vm
                };

                if (window.ShowDialog() != true)
                    return;
            }
        }
    }
}