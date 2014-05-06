using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Notifications
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool opt = (bool)value;

            if (opt) return System.Windows.Visibility.Visible;
            else return System.Windows.Visibility.Hidden;
        }
        

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
