using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Exchange.Mobile.UI.Converters
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return null;
            }
            return ImageSource.FromStream(
            () => new MemoryStream(System.Convert.FromBase64String(value.ToString())));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
