namespace MVVM.UI.ViewModels
{
    using System.Windows;

    using MVVM.Models;

    /// <summary>
    /// Класс валидации ошибок для <see cref="IContact"/>.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверка полученных результатов. 
        /// </summary>
        /// <param name="result"> Результат операции. </param>
        /// <returns> <see langword="true"/>, если проверка пройдена. </returns>
        public static bool Validate(this IContact result)
        {
            if (!(result is Contact contact))
                return false;

            var error = contact.Error;
            return ValidateError(error);
        }

        /// <summary>
        /// Проверка ошибки результата.
        /// </summary>
        /// <param name="error"> Сообщение об ошибке. </param>
        /// <returns> True, если ошибка пуста, иначе вывод сообщения на экран. </returns>
        private static bool ValidateError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return true;

            MessageBox.Show(error);
            return false;
        }
    }
}