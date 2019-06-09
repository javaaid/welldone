using System;
using System.Windows.Data;

namespace WelldonePOS.ValueConverters.Converters
{
    public class boolStatus_to_string : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
                return "Aktif";
            else
                return "Non Aktif";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "Aktif")
                return true;
            else
                return false;
        }
    }
}
