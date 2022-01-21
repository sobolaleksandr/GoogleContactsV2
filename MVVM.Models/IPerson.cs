namespace MVVM.Models
{
    /// <summary>
    /// Интерфейс пользователя.
    /// </summary>
    public interface IPerson : IContact
    {
        /// <summary>
        /// Адрес электронной почты. 
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        string FamilyName { get; }

        /// <summary>
        /// Имя.
        /// </summary>
        string GivenName { get; }

        /// <summary>
        /// Членство в группах.
        /// </summary>
        string GroupResourceName { get; }

        /// <summary>
        /// Организация. 
        /// </summary>
        string Organization { get; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        string PhoneNumber { get; }
    }
}