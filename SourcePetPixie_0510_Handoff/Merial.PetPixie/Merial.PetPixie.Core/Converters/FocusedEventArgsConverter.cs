using System;
using System.Globalization;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Converters
{
    public class FocusedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as FocusEventArgs;
            if (eventArgs == null)
                throw new ArgumentException("Expected FocusedEventArgs as value", "value");

            return eventArgs.IsFocused;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
