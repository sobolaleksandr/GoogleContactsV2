namespace MVVM.Models
{
    public interface IContact
    {
        /// <summary>
        /// Идентификатор модели.
        /// </summary>
        string ETag { get; set; }

        /// <summary>
        /// Наименование ресурса.
        /// </summary>
        string ResourceName { get; set; }

        void ApplyFrom(IContact contact);
    }
}