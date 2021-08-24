using System;
using System.Globalization;
using Xamarin.Forms;

namespace SignalRVideoCall.Converter
{
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter as string;
            if (param == "Inverse")
            {
                return !(bool)value;
            }

            return (bool)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
