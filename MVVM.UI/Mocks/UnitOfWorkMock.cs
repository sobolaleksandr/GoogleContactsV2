namespace MVVM.UI.Mocks
{
    using MVVM.Models;

    /// <summary>
    /// Иммитация единицы работы.
    /// </summary>
    public class UnitOfWorkMock : IUnitOfWork
    {
        /// <summary>
        /// Иммитация единицы работы.
        /// </summary>
        public UnitOfWorkMock()
        {
            PeopleService = new PeopleServiceMock();
            GroupService = new GroupServiceMock();
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public IService<IGroup> GroupService { get; }

        /// <inheritdoc />
        public IService<IPerson> PeopleService { get; }
    }
}