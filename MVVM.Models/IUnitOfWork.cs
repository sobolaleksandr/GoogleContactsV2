namespace MVVM.Models
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        IService<IGroup> GroupService { get; }

        /// <summary>
        /// Сервис для работы с контактами.
        /// </summary>
        IService<IPerson> PeopleService { get; }
    }
}