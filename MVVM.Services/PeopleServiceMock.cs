namespace MVVM.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1.Data;

    using MVVM.Models;

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
                (IContact)new Person(
                    new Google.Apis.PeopleService.v1.Data.Person
                    {
                        Names = new List<Name>
                        {
                            new Name
                            {
                                GivenName = $"TestPerson{item}",
                            }
                        },
                        PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = item.ToString() } }
                    })).ToList();

            return await Task.FromResult(models);
        }

        public async Task<IContact> UpdateAsync(IPerson model)
        {
            return await Task.FromResult(new Contact());
        }
    }
}