using System;
using System.Windows.Data;

namespace WelldonePOS.ValueConverters.Converters
{
    public class boolStatus_to_checkstate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool param = bool.Parse(parameter.ToString());

            if (value == null)
                return false;
            else
                return !((bool)value ^ param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool param = bool.Parse(parameter.ToString());

            return !((bool)value ^ param);
        }
    }
}
