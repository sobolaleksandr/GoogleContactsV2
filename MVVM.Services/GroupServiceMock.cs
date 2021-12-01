namespace MVVM.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1.Data;

    using MVVM.Models;

    public class GroupServiceMock : IService<Group>
    {
        public async Task<Contact> CreateAsync(Group model)
        {
            return await Task.FromResult(model);
        }

        public async Task<string> DeleteAsync(Group model)
        {
            return await Task.FromResult(string.Empty);
        }

        public async Task<List<IContact>> GetAsync()
        {
            var models = Enumerable.Range(100, 10).Select(item =>
                    (IContact)new Group(new ContactGroup { FormattedName = $"TestGroup{item}", MemberCount = item }))
                .ToList();

            return await Task.FromResult(models);
        }

        public async Task<Contact> UpdateAsync(Group model)
        {
            return await Task.FromResult(model);
        }
    }
}