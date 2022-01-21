namespace MVVM.Services
{
    using System;

    using MVVM.Models;

    /// <summary>
    /// Обработчик исключений.
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Обработать исключение.
        /// </summary>
        /// <param name="exception"> Исключение. </param>
        /// <returns> Контакт с сообщением об ошибке. </returns>
        public static IContact HandleException(Exception exception)
        {
            return new Contact(exception.ToString());
        }
    }
}