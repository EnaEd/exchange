using Exchange.Mobile.Core.Enums;
using Exchange.Mobile.Core.Extension;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Exchange.Mobile.Core.Converters
{
    public class StringToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string category) || string.IsNullOrWhiteSpace(category))
            {
                return string.Empty;
            }
            var iconCode = ((CategoryEnum)Enum.Parse(typeof(CategoryEnum), category)).GetAttribute<EnumDescriptorAttribute>().Descriptor;
            return iconCode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
