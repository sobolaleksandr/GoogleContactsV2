namespace MVVM.Models
{
    /// <summary>
    /// Базовая модель контакта.
    /// </summary>
    public class Contact : IContact
    {
        /// <summary>
        /// Базовая модель контакта.
        /// </summary>
        /// <param name="error"> Ошибка модели. </param>
        public Contact(string error = "")
        {
            Error = error;
        }

        /// <summary>
        /// Ошибка модели.
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        public string ETag { get; protected set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        public string ResourceName { get; protected set; }
    }
}