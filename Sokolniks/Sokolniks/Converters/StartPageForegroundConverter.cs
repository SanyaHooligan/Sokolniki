using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Sokolniks.Converters
{
    public class StartPageForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isStartPage = value as bool?;
            if (isStartPage == true)
            {
                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            else if (isStartPage == false)
            {
                return new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
