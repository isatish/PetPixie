using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.iOS.Model.Extensions
{
    public static class ExtensionMethod
    {
        public static string GetiOSCalendarFrequency(this ReminderFrequencyModel frequency)
        {

            return $"FREQ={frequency.Type.ToString().ToUpper()};INTERVAL={frequency.Value.ToString()}";
        }
    }
}