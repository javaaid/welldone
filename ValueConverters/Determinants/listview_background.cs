using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WelldonePOS.ValueConverters.Determinants
{
    public class listview_background : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ListViewItem item = value as ListViewItem;
            ListView container = ItemsControl.ItemsControlFromItemContainer(item) as ListView;

            int index = container.ItemContainerGenerator.IndexFromContainer(item);

            if (index % 2 == 0)
                return new SolidColorBrush(Color.FromArgb(175, 220, 220, 220));
            else
                return new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
