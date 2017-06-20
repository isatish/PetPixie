using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Merial.PetPixie.Core.Converters
{
    public class TimeAgoValueConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            var timeAgo = DateTime.UtcNow - value;
            if (timeAgo.Days > 365)
            {
                int years = (timeAgo.Days / 365);
                if (timeAgo.Days % 365 != 0)
                    years += 1;
                return $"{years} y";
            }
            if (timeAgo.Days > 30)
            {
                int months = (timeAgo.Days / 30);
                if (timeAgo.Days % 31 != 0)
                    months += 1;
                return $"{months} M";
            }
            if (timeAgo.Days > 0)
                return $"{timeAgo.Days} d";
            if (timeAgo.Hours > 0)
                return $"{timeAgo.Hours} h";
            if (timeAgo.Minutes > 0)
                return $"{timeAgo.Minutes} m";
            if (timeAgo.Seconds > 5)
                return $"{timeAgo.Seconds} s";
            if (timeAgo.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}
