using System;
using System.Globalization;
using Xamarin.Forms;

namespace Exchange.Mobile.UI.Converters
{
    public class ExcludeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()) ||
                string.IsNullOrWhiteSpace(parameter?.ToString()) ||
                value?.ToString() != parameter?.ToString())
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
