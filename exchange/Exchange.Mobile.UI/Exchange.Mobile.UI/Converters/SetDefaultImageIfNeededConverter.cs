using System;
using System.Globalization;
using Xamarin.Forms;

namespace Exchange.Mobile.UI.Converters
{
    public class SetDefaultImageIfNeededConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is null))
            {
                return value as ImageSource;
            }
            return ImageSource.FromFile("uploadImage.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
