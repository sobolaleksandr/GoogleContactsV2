namespace MVVM.Services
{
    using System;

    using MVVM.Models;

    /// <summary>
    /// Базовая модель контакта.
    /// </summary>
    public class Contact : IContact
    {
        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        public string ETag { get; set; }

        /// <summary>
        /// Базовая модель контакта.
        /// </summary>
        /// <param name="error"></param>
        public Contact(string error = "")
        {
            Error = error;
        }

        /// <summary>
        /// Ошибка модели.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string ResourceName { get; set; }

        public virtual void ApplyFrom(IContact contact)
        {
        }
    }
}