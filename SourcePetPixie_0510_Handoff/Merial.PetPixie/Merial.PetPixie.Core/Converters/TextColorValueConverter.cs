using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;

namespace Merial.PetPixie.Core.Converters
{
    public class TextColorValueConverter : MvxColorValueConverter
    {
        private static readonly MvxColor Black = new MvxColor(0x00, 0x00, 0x00);
        
        protected override MvxColor Convert(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? MvxColors.White : Black;
        }
    }
}
