namespace MVVM.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.PeopleService.v1;

    using MVVM.Models;

    /// <summary>
    /// Сервия для работы с <see cref="Services.Person"/>
    /// </summary>
    public class PeopleService : IService<Person>
    {
        /// <summary>
        /// Поля для запроса данных контакта.
        /// </summary>
        private const string PERSON_FIELDS = "names,emailAddresses,phoneNumbers,organizations,memberships";

        /// <summary>
        /// Ресурс для работы с <see cref="Services.Person"/>
        /// </summary>
        private readonly PeopleServiceService _service;

        /// <summary>
        /// Сервия для работы с <see cref="Services.Person"/>
        /// </summary>
        /// <param name="service"> Ресурс для работы с <see cref="Services.Person"/> </param>
        public PeopleService(PeopleServiceService service)
        {
            _service = service;
        }

        public async Task<Contact> CreateAsync(Person model)
        {
            if (model == null)
                return new Contact("Empty model");

            var person = model.Map();
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

        public async Task<string> DeleteAsync(Person model)
        {
            if (model == null)
                return "Empty model";

            var request = _service.People.DeleteContact(model.ResourceName);

            try
            {
                await request.ExecuteAsync();
                return string.Empty;
            }
            catch (Exception exception)
            {
                return exception.ToString();
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

        public async Task<Contact> UpdateAsync(Person model)
        {
            if (model == null)
                return new Contact("Empty model");

            var person = model.Map();
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
    }
}