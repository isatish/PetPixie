using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core.Helpers
{
    public static class ReminderHelper
    {

        public static ReminderType GetReminderTypeFromString(string reminderTypeStr)
        {
            ReminderType result;
            return Enum.TryParse<ReminderType>(reminderTypeStr, true, out result) ? result : ReminderType.Other;
        }
    }
}
