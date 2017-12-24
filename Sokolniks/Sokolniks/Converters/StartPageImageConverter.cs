using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sokolniks.Converters
{
    public class StartPageImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isStartPage = value as bool?;
            if (isStartPage == true)
            {
                return new Uri("../Images/logo_white.png", UriKind.Relative);
            }
            else if (isStartPage == false)
            {
                return new Uri("../Images/logo.png", UriKind.Relative);
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
