namespace MVVM.Models
{
    using System;

    /// <summary>
    /// Интерфейс единицы работы.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        IService<IGroup> GroupService { get; }

        /// <summary>
        /// Сервис для работы с пользователями.
        /// </summary>
        IService<IPerson> PeopleService { get; }
    }
}