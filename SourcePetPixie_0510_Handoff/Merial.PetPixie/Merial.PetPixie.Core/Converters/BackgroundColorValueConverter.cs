using MvvmCross.Platform.UI;
using MvvmCross.Plugins.Color;

namespace Merial.PetPixie.Core.Converters
{
    public class BackgroundColorValueConverter : MvxColorValueConverter
    {
        private static readonly MvxColor TrueBgColor = new MvxColor(0xFF, 0x00, 0x00);
        private static readonly MvxColor FalseBgColor = new MvxColor(0xCC, 0xCC, 0xCC);

        protected override MvxColor Convert(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? TrueBgColor : FalseBgColor;
        }
    }
}
