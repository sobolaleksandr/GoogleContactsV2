namespace MVVM.UI.Views
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Инвертор булевых значений.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class InvertBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert(value);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Invert(value);
        }

        /// <summary>
        /// Инвертировать значение.
        /// </summary>
        /// <param name="value"> Булевое значение. </param>
        /// <returns> Инветированное значение. </returns>
        private static object Invert(object value)
        {
            if (value is bool booleanToInvert)
                return !booleanToInvert;

            return Binding.DoNothing;
        }
    }
}