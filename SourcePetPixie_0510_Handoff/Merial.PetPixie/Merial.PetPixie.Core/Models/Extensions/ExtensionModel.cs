using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models.Extensions
{
    public static class ExtensionModel
    {
        public static KReminder GenerateKReminder(this ReminderModel reminder,string petId)
        {
            //Set Default recurrence 
           

            return new KReminder
            {
                Id = reminder.Id,
                PetId = petId,
                ReminderTypeId =  reminder.Type!=ReminderType.Other? ((int) reminder.Type).ToString():null,
                LocationVetId = reminder.VetReminderModel?.Id,
                LocationCustomText = reminder.VetReminderModel?.OtherValue,
                ReminderOccurenceIntervalId = reminder.FrequencyModel?.IntervalValid != null && reminder.FrequencyModel?.IntervalValid !="0"? int.Parse(reminder.FrequencyModel?.IntervalValid):(int?) null,
                SyncCalendar = reminder.SaveInCalendar,
                Notes = reminder.Note,
                EventId = reminder.IdCalendarAndroid!=0?reminder.IdCalendarAndroid.ToString():null,
                ReminderDate = DateTime.SpecifyKind(reminder.FirstAlert, DateTimeKind.Local),
                TypeName = reminder.TypeModel.NameDisplay,
                ProductId = reminder.ProductModel?.Id,
                OtherProductName = reminder.ProductModel?.OtherValue


            };
        }

        public static void SetDefaultFrequency(this ReminderModel reminder)
        {
            reminder.FrequencyModel = new ReminderFrequencyModel
            {
                IntervalValid ="0",
                Value =1,
                Type = ReminderFrequencyType.Monthly

            };
        }
    }
}
