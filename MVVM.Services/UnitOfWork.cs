﻿namespace MVVM.Services
{
    using System;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.Services;

    using MVVM.Models;

    /// <summary>
    /// Единица работы для пользователей и групп.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";

        /// <summary>
        /// Сервис для работы с пользователями.
        /// </summary>
        private readonly PeopleServiceService _service;

        /// <summary>
        /// Ресурсы освобождены.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Единица работы для пользователей и групп.
        /// </summary>
        public UnitOfWork()
        {
            const string authorizedUser = "user";
            var cancellationToken = CancellationToken.None;
            var scopes = new[] { "https://www.googleapis.com/auth/contacts" };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = M_CLIENT_ID,
                    ClientSecret = M_CLIENT_SECRET,
                },
                scopes,
                authorizedUser,
                cancellationToken).Result;

            _service = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleContacts",
            });

            PeopleService = new PeopleService(_service);
            GroupService = new GroupService(_service);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        public IService<IGroup> GroupService { get; }

        /// <summary>
        /// Сервис для работы с пользователями.
        /// </summary>
        public IService<IPerson> PeopleService { get; }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _service?.Dispose();
            _disposed = true;
        }

        /// <summary>
        /// Финализатор.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}