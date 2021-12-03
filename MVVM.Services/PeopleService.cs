namespace MVVM.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;
    using Google.Apis.PeopleService.v1.Data;

    using MVVM.Models;

    /// <summary>
    /// Сервия для работы с <see cref="Person"/>
    /// </summary>
    public class PeopleService : IService<IPerson>
    {
        /// <summary>
        /// Поля для запроса данных контакта.
        /// </summary>
        private const string PERSON_FIELDS = "names,emailAddresses,phoneNumbers,organizations,memberships";

        /// <summary>
        /// Ресурс для работы с <see cref="Person"/>
        /// </summary>
        private readonly PeopleServiceService _service;

        /// <summary>
        /// Сервия для работы с <see cref="Person"/>
        /// </summary>
        /// <param name="service"> Ресурс для работы с <see cref="Person"/> </param>
        public PeopleService(PeopleServiceService service)
        {
            _service = service;
        }

        public async Task<Contact> CreateAsync(IPerson model)
        {
            if (model == null)
                return new Contact("Empty model");

            var person = Map(model);
            var request = _service.People.CreateContact(person);

            try
            {
                var response = await request.ExecuteAsync();
                return response != null
                    ? new Person(response)
                    : new Contact("Unexpected error");
            }
            catch (Exception exception)
            {
                return new Contact(exception.ToString());
            }
        }

        public async Task<Contact> DeleteAsync(IPerson model)
        {
            if (model == null)
                return new Contact("Empty model");

            var request = _service.People.DeleteContact(model.ResourceName);

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
            var request = _service.People.Connections.List("people/me");
            request.PersonFields = PERSON_FIELDS;

            try
            {
                var connectionsResponse = await request.ExecuteAsync();
                var connections = connectionsResponse.Connections;
                return connections
                    .Select(person => (IContact)new Person(person))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<IContact>();
            }
        }

        public async Task<Contact> UpdateAsync(IPerson model)
        {
            if (model == null)
                return new Contact("Empty model");

            var person = Map(model);
            var request = _service.People.UpdateContact(person, model.ResourceName);
            request.UpdatePersonFields = PERSON_FIELDS;

            try
            {
                var response = await request.ExecuteAsync();
                return response != null
                    ? new Person(response)
                    : new Contact("Unexpected error");
            }
            catch (Exception exception)
            {
                return new Contact(exception.ToString());
            }
        }

        /// <summary>
        /// Преобразовать в объект для работы с GoogleContacts. 
        /// </summary>
        /// <returns> Объект для работы с GoogleContacts. </returns>
        private static Google.Apis.PeopleService.v1.Data.Person Map(IPerson person)
        {
            return new Google.Apis.PeopleService.v1.Data.Person
            {
                ResourceName = person.ResourceName,
                ETag = person.ETag,
                Names = new List<Name> { new Name { GivenName = person.GivenName, FamilyName = person.FamilyName } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = person.PhoneNumber } },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Value = person.Email } },
                Organizations = new List<Organization> { new Organization { Name = person.Organization } },
                Memberships = new List<Membership>
                {
                    new Membership
                    {
                        ContactGroupMembership = new ContactGroupMembership
                            { ContactGroupResourceName = person.GroupResourceName }
                    }
                }
            };
        }
    }
}