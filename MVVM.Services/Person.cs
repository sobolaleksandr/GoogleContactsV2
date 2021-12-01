namespace MVVM.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Google.Apis.PeopleService.v1.Data;

    using MVVM.Models;

    /// <summary>
    /// Модель контакта.
    /// </summary>
    public class Person : Contact, IPerson
    {
        public Person(Google.Apis.PeopleService.v1.Data.Person person, string error = "") : base(error)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            var name = person.Names?.FirstOrDefault();
            var email = person.EmailAddresses?.FirstOrDefault();
            var phoneNumber = person.PhoneNumbers?.FirstOrDefault();
            var organization = person.Organizations?.FirstOrDefault();
            var membership = person.Memberships?.FirstOrDefault();

            ResourceName = person.ResourceName;
            ETag = person.ETag;
            GivenName = name?.GivenName ?? string.Empty;
            FamilyName = name?.FamilyName ?? string.Empty;
            PhoneNumber = phoneNumber?.Value ?? string.Empty;
            Email = email?.Value ?? string.Empty;
            Organization = organization?.Name ?? string.Empty;
            GroupResourceName = membership?.ContactGroupMembership?.ContactGroupResourceName;
        }

        /// <summary>
        /// Членство в группах.
        /// </summary>
        public string GroupResourceName { get; }

        /// <summary>
        /// Адрес электронной почты. 
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FamilyName { get; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// Организация. 
        /// </summary>
        public string Organization { get; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// Преобразовать в объект для работы с GoogleContacts. 
        /// </summary>
        /// <returns> Объект для работы с GoogleContacts. </returns>
        public Google.Apis.PeopleService.v1.Data.Person Map()
        {
            return new Google.Apis.PeopleService.v1.Data.Person
            {
                ResourceName = ResourceName,
                ETag = ETag,
                Names = new List<Name> { new Name { GivenName = GivenName, FamilyName = FamilyName } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Value = PhoneNumber } },
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Value = Email } },
                Organizations = new List<Organization> { new Organization { Name = Organization } },
                Memberships = new List<Membership>
                {
                    new Membership
                    {
                        ContactGroupMembership = new ContactGroupMembership
                            { ContactGroupResourceName = GroupResourceName }
                    }
                }
            };
        }
    }
}