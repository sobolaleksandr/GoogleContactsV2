namespace MVVM.App
{
    using MVVM.Models;
    using MVVM.Services;
    using MVVM.UI.Mocks;

    /// <summary>
    /// Фабрика для создания <see cref="IUnitOfWork"/>.
    /// </summary>
    public static class UnitOfWorkFactory
    {
        /// <summary>
        /// Создать экземпляр <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <param name="isDebug"> Режим отладки. </param>
        /// <returns> Возвращает экземпляр <see cref="IUnitOfWork"/>. </returns>
        public static IUnitOfWork Create(bool isDebug)
        {
            return isDebug ? (IUnitOfWork)new UnitOfWorkMock() : new UnitOfWork();
        }
    }
}