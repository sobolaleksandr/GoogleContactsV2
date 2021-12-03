namespace MVVM.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MVVM.Models;

    /// <summary>
    /// Сервис для работы с контактами.
    /// </summary>
    /// <typeparam name="T"> Модель контакта. </typeparam>
    public interface IService<in T> where T : IContact
    {
        /// <summary>
        /// Создать.
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<Contact> CreateAsync(T model);

        /// <summary>
        /// Удалить. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Сообщение об ошибке. </returns>
        Task<Contact> DeleteAsync(T model);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        /// <returns> Список контактов. </returns>
        Task<List<IContact>> GetAsync();

        /// <summary>
        /// Обновить контакт. 
        /// </summary>
        /// <param name="model"> Модель контакта. </param>
        /// <returns> Модель контакта. </returns>
        Task<Contact> UpdateAsync(T model);
    }
}