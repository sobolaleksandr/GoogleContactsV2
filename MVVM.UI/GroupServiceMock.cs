namespace MVVM.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.ViewModels;

    public class GroupServiceMock : IService<IGroup>
    {
        public async Task<IContact> CreateAsync(IGroup model)
        {
            return await Task.FromResult(new Contact());
        }

        public async Task<IContact> DeleteAsync(IGroup model)
        {
            return await Task.FromResult(new Contact());
        }

        public async Task<List<IContact>> GetAsync()
        {
            var models = Enumerable.Range(100, 10).Select(item =>
                    (IContact)new GroupViewModel(null))
                .ToList();

            return await Task.FromResult(models);
        }

        public async Task<IContact> UpdateAsync(IGroup model)
        {
            return await Task.FromResult(new Contact());
        }
    }
}