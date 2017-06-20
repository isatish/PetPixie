using System.Globalization;
using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;

namespace Merial.PetPixie.Core.Converters
{
    public class IsMedicalAlertBackgroundColorValueConverter : MvxColorValueConverter
    {
        protected override MvxColor Convert(object value, object parameter, CultureInfo culture)
        {
            return (bool)value ? new MvxColor(229,57,53) : MvxColors.Transparent;
        }
    }
}