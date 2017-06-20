using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Platform.Converters;

namespace Merial.PetPixie.Core.Converters
{
    public class DateTimeToStringValueConverter : MvxValueConverter<DateTime?, string>
    {
        protected override string Convert(DateTime? value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.HasValue ? value.Value.ToString("dd MMM yyyy HH:mm:ss") : "None";
        }
    }
}
