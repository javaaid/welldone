using System;
using System.Windows.Data;

namespace WelldonePOS.ValueConverters.Converters
{
    public class string_to_upperstring : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value as string;

            return string.IsNullOrEmpty(text) ? string.Empty : text.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value as string;

            return string.IsNullOrEmpty(text) ? string.Empty : text.ToUpper();
        }
    }
}
