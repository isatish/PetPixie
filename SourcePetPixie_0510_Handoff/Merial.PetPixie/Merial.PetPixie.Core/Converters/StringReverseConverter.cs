using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.UI;
//using Cirrious.CrossCore.Converters;
//using Cirrious.CrossCore.UI;
using MvvmCross.Plugins.Color;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Converters
{
   

 

    public class StringReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = value ?? string.Empty;
            //char[] charArray = value.ToCharArray();//.ToCharArray();
            // Array.Reverse(charArray);
            return "123456";// new string("12345");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    value = value ?? string.Empty;
        //    char[] charArray = value.ToCharArray();
        //    Array.Reverse(charArray);
        //    return new string(charArray);
        //}
    }

  
}
