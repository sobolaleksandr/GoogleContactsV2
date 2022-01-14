namespace MVVM.UI
{
    using MVVM.Models;

    // ReSharper disable once UnusedType.Global
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkMock : IUnitOfWork
    {
        public UnitOfWorkMock()
        {
            PeopleService = new PeopleServiceMock();
            GroupService = new GroupServiceMock();
        }

        public IService<IGroup> GroupService { get; }
        public IService<IPerson> PeopleService { get; }

        public void Dispose()
        {
        }
    }
}