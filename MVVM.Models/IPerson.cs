namespace MVVM.Models
{
    public interface IPerson
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
        /// Организация. 
        /// </summary>
        string Organization { get; }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        string PhoneNumber { get; }
    }
}