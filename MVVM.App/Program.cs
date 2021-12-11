namespace MVVM.App
{
    using System;
    using System.Threading.Tasks;

    using MVVM.Services;
    using MVVM.UI.ViewModels;
    using MVVM.UI.Views;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var unitOfWork = new UnitOfWork();
            while (true)
            {
                if (unitOfWork.Disposed)
                    return;

                var viewModel = new ApplicationViewModel(unitOfWork);
                Task.Run(async () =>
                {
                    await viewModel.UpdateGroups();
                    await viewModel.UpdatePeople();
                }).GetAwaiter().GetResult();

                var window = new MainWindow
                {
                    DataContext = viewModel
                };

                if (window.ShowDialog() != true)
                    return;
            }
        }
    }
}