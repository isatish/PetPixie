using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Merial.PetPixie.iOS.Core.ValueConverters {
    public class BoolToNotBoolValueConverter : IMvxValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(value as bool?);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}