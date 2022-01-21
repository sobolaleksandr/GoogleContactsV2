namespace MVVM.Models
{
    /// <summary>
    /// Интерфейс контакта.
    /// </summary>
    public interface IContact
    {
        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        string ETag { get; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        string ResourceName { get; }
    }
}