namespace MVVM.App
{
    using System;
    using System.Threading.Tasks;

    using MVVM.UI.ViewModels;
    using MVVM.UI.Views;

    /// <summary>
    /// Входная точка программы.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Режим отладки.
        /// </summary>
        private const bool DEBUG = false;

        /// <summary>
        /// Вызывающий метод.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using var unitOfWork = UnitOfWorkFactory.Create(DEBUG);

            var viewModel = new ApplicationViewModel(unitOfWork);
            Task.Run(async () =>
            {
                await viewModel.GroupController.UpdateGroupsAsync();
                await viewModel.PeopleController.UpdatePeopleAsync();
            }).GetAwaiter().GetResult();

            var window = new MainWindow
            {
                DataContext = viewModel,
            };

            window.ShowDialog();
        }
    }
}