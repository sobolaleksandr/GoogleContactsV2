namespace MVVM.UI.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MVVM.Models;
    using MVVM.UI.ViewModels;

    /// <summary>
    /// Иммитация сервиса для работы с <see cref="IGroup"/>.
    /// </summary>
    public class GroupServiceMock : IService<IGroup>
    {
        /// <inheritdoc />
        public async Task<IContact> CreateAsync(IGroup model)
        {
            return await Task.FromResult(model);
        }

        /// <inheritdoc />
        public async Task<IContact> DeleteAsync(IGroup model)
        {
            return await Task.FromResult(new Contact());
        }

        /// <inheritdoc />
        public async Task<List<IContact>> GetAsync()
        {
            var models = Enumerable.Range(100, 10).Select(item =>
                    (IContact)new GroupViewModel(null)
                    {
                        FormattedName = $"TestName {item}",
                    })
                .ToList();

            return await Task.FromResult(models);
        }

        /// <inheritdoc />
        public async Task<IContact> UpdateAsync(IGroup model)
        {
            return await Task.FromResult(model);
        }
    }
}