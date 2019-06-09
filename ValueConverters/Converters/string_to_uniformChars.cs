using System;
using System.Windows.Data;

namespace WelldonePOS.ValueConverters.Converters
{
    public class string_to_uniformChars : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value as string;
            char[] returnText = new char[text.Length];

            if (string.IsNullOrEmpty(text))
                return string.Empty;
            else
                for (int i = 0; i < text.Length; i++)
                    returnText[i] = '*';

            return new string(returnText);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
