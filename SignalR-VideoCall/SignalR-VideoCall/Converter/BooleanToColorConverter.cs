using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SignalRVideoCall.Converter
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var colors = System.Convert.ToString(parameter).Split('|');

            if ((bool)value)
            {
                return ColorConverters.FromHex(colors[0]);
            }
            return ColorConverters.FromHex(colors[1]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
