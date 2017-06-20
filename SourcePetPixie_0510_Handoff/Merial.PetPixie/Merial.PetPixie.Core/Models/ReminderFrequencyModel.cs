using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class ReminderFrequencyModel : EntityBase
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public int  Value { get; set; }

        public string IntervalValid { get; set; }

        //public string RecurrenceNameDisplay
        //{
        //    get
        //    {
        //        switch (Name)
        //        {
        //            case "Daily":
        //                return "Day";
        //            case "Weekly":
        //                return "Week";
        //            case "Monthly":
        //                return "Month";
        //            case "Yearly":
        //                return "Year";
        //            case "Never":
        //                return "Never";
        //            default:
        //                return null;
        //        }
        //    }
        //}

        public ReminderFrequencyType Type { get; set; }

        //public static ReminderFrequencyModel CreateFrom(ReminderFrequencyModel recurrenceType, int frequency)
        //{
        //    return new ReminderFrequencyModel
        //    {
        //        Id = recurrenceType.Id,
        //        Name = recurrenceType.Name,
        //        Value = frequency,
        //        IntervalId = recurrenceType.IntervalId
        //    };
        //}

        public static ReminderFrequencyModel CreateFrom(KFrequency frequency)
        {
            var frequencyModel = new ReminderFrequencyModel
            {

                Name = frequency.Interval,
                IntervalValid = frequency.IntervalId,

            };
            switch (frequency.IntervalId)
            {
                //case "1":
                case "6":
                    frequencyModel.Value = 1;
                    frequencyModel.Type = ReminderFrequencyType.Daily;
                break;
                //case "2":
                case "7":
                    frequencyModel.Value = 1;
                    frequencyModel.Type = ReminderFrequencyType.Weekly;
                    break;
                //case "3":
                case "9":
                    frequencyModel.Value = 1;
                    frequencyModel.Type = ReminderFrequencyType.Monthly;
                    break;
                //case "4":
                case "10":
                    frequencyModel.Value = 1;
                    frequencyModel.Type = ReminderFrequencyType.Yearly;
                    break;
                case "5":
                    frequencyModel.Value = 0;
                    frequencyModel.Type = ReminderFrequencyType.Never;
                    break;
                case "8":
                    frequencyModel.Value =2;
                    frequencyModel.Type = ReminderFrequencyType.Weekly;
                    break;
                default:
                    return null;


            }
            return frequencyModel;


        }

        public static ReminderFrequencyModel CreateFrom(int reminderOccurenceIntervalId)
        {
            return new ReminderFrequencyModel
            {

                IntervalValid = reminderOccurenceIntervalId.ToString(),

            };
        }


        //public static ReminderFrequencyModel CreateFrom(KFrequency frequency)
        //{
        //    var reminderFrequencyModel= new ReminderFrequencyModel
        //    {

        //        Name = frequency.Interval,

        //        IntervalId = int.Parse(frequency.IntervalId)
        //    };

        //    if (string.IsNullOrEmpty(reminderFrequencyModel.NameDisplay))
        //    {
        //        return null;
        //    }
        //    return reminderFrequencyModel;
        //}

        //public string NameDisplay => $"Every {Value} {RecurrenceNameDisplay}";
    }
}
