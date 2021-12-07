namespace MVVM.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;
    using Google.Apis.Services;

    using MVVM.Models;

    /// <summary>
    /// Сервия для работы с <see cref="Group"/>
    /// </summary>
    public class GroupService : IService<IGroup>
    {
        /// <summary>
        /// Ресурс для работы с <see cref="Group"/> 
        /// </summary>
        private readonly ContactGroupsResource _groupsResource;

        /// <summary>
        /// Сервия для работы с <see cref="Group"/>
        /// </summary>
        /// <param name="service"> Ресурс для работы с <see cref="Person"/> </param>
        public GroupService(IClientService service)
        {
            _groupsResource = new ContactGroupsResource(service);
        }

        /// <summary>
        /// Преобразовать в объект для работы с GoogleContacts. 
        /// </summary>
        /// <returns> Объект для работы с GoogleContacts. </returns>
        private static ContactGroup Map(IGroup group)
        {
            return new ContactGroup
            {
                Name = group.FormattedName ?? string.Empty,
                ETag = group.ETag ?? string.Empty
            };
        }

        public async Task<IContact> CreateAsync(IGroup model)
        {
            if (model == null)
                return new Contact("Empty model");

            var group = Map(model);
            var request = new CreateContactGroupRequest
            {
                ContactGroup = group
            };

            var createRequest = _groupsResource.Create(request);

            try
            {
                var response = await createRequest.ExecuteAsync();
                return response != null
                    ? new Group(response)
                    : new Contact("Unexpected error");
            }
            catch (Exception exception)
            {
                return new Contact(exception.ToString());
            }
        }

        public async Task<IContact> DeleteAsync(IGroup model)
        {
            if (model == null)
                return new Contact("Empty model");

            var request = _groupsResource.Delete(model.ResourceName);

            try
            {
                await request.ExecuteAsync();
                return new Contact();
            }
            catch (Exception exception)
            {
                return new Contact(exception.ToString());
            }
        }

        public async Task<List<IContact>> GetAsync()
        {
            var request = _groupsResource.List();

            try
            {
                var response = await request.ExecuteAsync();
                var groups = response.ContactGroups.Where(group => group.GroupType != "SYSTEM_CONTACT_GROUP");
                return groups
                    .Select(group => (IContact)new Group(group))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<IContact>();
            }
        }

        public async Task<IContact> UpdateAsync(IGroup model)
        {
            if (model == null)
                return new Contact("Empty model");

            var request = new UpdateContactGroupRequest
            {
                ContactGroup = Map(model)
        };

            var updateRequest = _groupsResource.Update(request, model.ResourceName);

            try
            {
                var response = await updateRequest.ExecuteAsync();
                return response != null
                    ? new Group(response)
                    : new Contact("Unexpected error");
            }
            catch (Exception exception)
            {
                return new Contact(exception.ToString());
            }
        }
    }
}