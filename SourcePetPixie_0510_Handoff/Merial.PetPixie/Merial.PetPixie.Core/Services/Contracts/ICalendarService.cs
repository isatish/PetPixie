using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface ICalendarService
    {
        string AddReminder(ReminderModel reminder);

        int RemoveReminder(ReminderModel reminder);
    }
}
