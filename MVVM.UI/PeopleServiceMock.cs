namespace MVVM.UI
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.ViewModels;

    public class PeopleServiceMock : IService<IPerson>
    {
        public async Task<IContact> CreateAsync(IPerson model)
        {
            return await Task.FromResult(new Contact());
        }

        public async Task<IContact> DeleteAsync(IPerson model)
        {
            return await Task.FromResult(new Contact());
        }

        public async Task<List<IContact>> GetAsync()
        {
            var models = Enumerable.Range(0, 20).Select(item =>
                (IContact)new PersonViewModel(null, new List<GroupViewModel>())).ToList();

            return await Task.FromResult(models);
        }

        public async Task<IContact> UpdateAsync(IPerson model)
        {
            return await Task.FromResult(new Contact());
        }
    }
}