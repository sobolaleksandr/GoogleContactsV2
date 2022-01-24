namespace MVVM.UI.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.ViewModels;

    /// <summary>
    /// Иммитация сервиса для работы с <see cref="IPerson"/>.
    /// </summary>
    public class PeopleServiceMock : IService<IPerson>
    {
        /// <inheritdoc />
        public async Task<IContact> CreateAsync(IPerson model)
        {
            return await Task.FromResult(model);
        }

        /// <inheritdoc />
        public async Task<IContact> DeleteAsync(IPerson model)
        {
            return await Task.FromResult(new Contact());
        }

        /// <inheritdoc />
        public async Task<List<IContact>> GetAsync()
        {
            var models = Enumerable.Range(0, 20)
                .Select(item => (IContact)new PersonViewModel(null, new ObservableCollectionRange<GroupViewModel>())
                {
                    FamilyName = $"TestName {item}",
                    GivenName = $"TestGiven {item}",
                    Email = $"TestEmail {item}",
                    Organization = $"TestOrganization {item}",
                    PhoneNumber = $"TestNumber {item}",
                })
                .ToList();

            return await Task.FromResult(models);
        }

        /// <inheritdoc />
        public async Task<IContact> UpdateAsync(IPerson model)
        {
            return await Task.FromResult(model);
        }
    }
}