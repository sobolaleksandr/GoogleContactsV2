namespace MVVM.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Сервис для работы с контактами.
    /// </summary>
    /// <typeparam name="T"> Модель контакта. </typeparam>
    public interface IService<in T> where T : IContact
    {
        /// <summary>
        /// Создать асинхронно.
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<IContact> CreateAsync(T model);

        /// <summary>
        /// Удалить асинхронно. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Сообщение об ошибке. </returns>
        Task<IContact> DeleteAsync(T model);

        /// <summary>
        /// Получить список контактов асинхронно.
        /// </summary>
        /// <returns> Список контактов. </returns>
        Task<List<IContact>> GetAsync();

        /// <summary>
        /// Обновить контакт асинхронно. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<IContact> UpdateAsync(T model);
    }
}