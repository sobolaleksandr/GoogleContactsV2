namespace MVVM.Services
{
    using System;
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.PeopleService.v1;
    using Google.Apis.Services;

    using MVVM.Models;

    /// <summary>
    /// Единица работы для контактов и групп.
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private const string M_CLIENT_ID = "217336154173-tdce9e8b3c9hjfsd9abnfb7q0ef4q9ab.apps.googleusercontent.com";
        private const string M_CLIENT_SECRET = "uavwQnDWY6bUEFf75pXtP0m6";

        /// <summary>
        /// Сервис для работы с контактами.
        /// </summary>
        private readonly PeopleServiceService _service;

        /// <summary>
        /// Ресурсы освобождены.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Единица работы для контактов и групп.
        /// </summary>
        /// <param name="isDebug"> Запуск в режиме отладки. </param>
        public UnitOfWork(bool isDebug)
        {
            const string authorizedUser = "user";
            var cancellationToken = CancellationToken.None;
            var scopes = new[] { "https://www.googleapis.com/auth/contacts" };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = M_CLIENT_ID,
                    ClientSecret = M_CLIENT_SECRET
                },
                scopes,
                authorizedUser,
                cancellationToken).Result;

            _service = new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleContacts",
            });

            CreateServices(isDebug);
        }

        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        public IService<IGroup> GroupService { get; private set; }

        /// <summary>
        /// Сервис для работы с контактами.
        /// </summary>
        public IService<IPerson> PeopleService { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Функция создания сервисов.
        /// </summary>
        /// <param name="isDebug">  Запуск в режиме отладки. </param>
        private void CreateServices(in bool isDebug)
        {
            if (isDebug)
            {
                PeopleService = new PeopleServiceMock();
                GroupService = new GroupServiceMock();
                return;
            }

            PeopleService = new PeopleService(_service);
            GroupService = new GroupService(_service);
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            _service?.Dispose();
            Disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}