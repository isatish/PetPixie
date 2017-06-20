using System;
using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Converters
{
    public class ActiveTextColorValueConverter : IValueConverter
    {

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.White : Color.FromHex("#aac2bb");
        }
    }
}
