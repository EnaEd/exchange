using System;
using System.Globalization;
using Xamarin.Forms;

namespace Exchange.Mobile.UI.Converters
{
    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue ? !boolValue : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
