namespace MVVM.App
{
    using System;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.Services;
    using MVVM.UI;
    using MVVM.UI.ViewModels;
    using MVVM.UI.Views;

    internal static class Program
    {
        private const bool DEBUG = false;

        [STAThread]
        private static void Main()
        {
            using var unitOfWork = UnitOfWorkFactory.Create(DEBUG);
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

    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork Create(bool isDebug)
        {
            return isDebug ? (IUnitOfWork)new UnitOfWorkMock() : new UnitOfWork();
        }
    }
}