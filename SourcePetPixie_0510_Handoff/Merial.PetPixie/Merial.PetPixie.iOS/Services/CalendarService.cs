using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.App;
//using Android.Content;
//using Android.Database;
//using Android.OS;
//using Android.Provider;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Java.Util;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Extensions;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.iOS;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Platform;
//using Uri = Android.Net.Uri;
using Merial.PetPixie.iOS.Model.Extensions;
namespace Merial.PetPixie.iOS.Services
{
    public class CalendarService :ICalendarService
    {
        public string AddReminder(ReminderModel reminder)
        {
            //if (reminder.IdCalendarAndroid > 0)
            //    RemoveReminder(reminder);
            //string calId = FindCalendarId();
            //ContentValues eventValues = new ContentValues();

            //eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId,
            //    calId);
            //eventValues.Put(CalendarContract.Events.InterfaceConsts.Title,
            //    reminder.GenerateTitleEvent());
            //eventValues.Put(CalendarContract.Events.InterfaceConsts.Description,
            //    reminder.GenerateContentEvent());
            //eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart,
            //    DateTimeHelper.GetDateTimeMS(reminder.FirstAlert.AddMonths(-1)));
            //eventValues.Put(CalendarContract.Events.InterfaceConsts.Duration,
            //    "PT15M");
            //if(reminder.VetReminderModel!= null)
            //    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation,
            //       reminder.VetReminderModel.Id ==null ? reminder.VetReminderModel.OtherValue : reminder.VetReminderModel.Adresse);


            //if (reminder.FrequencyModel == null)
            //{
            //    reminder.SetDefaultFrequency();
            //}

            //if (reminder.FrequencyModel != null && reminder.FrequencyModel.Type != ReminderFrequencyType.Never)
            //{
            //    eventValues.Put(CalendarContract.Events.InterfaceConsts.Rrule,
            //    reminder.FrequencyModel.GetAndroidCalendarFrequency());
            //}

            //eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,
            //    "UTC");
            //eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,
            //    "UTC");
            //var uri = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.ContentResolver.Insert(CalendarContract.Events.ContentUri,
            //    eventValues).ToString();
            //reminder.IdCalendarAndroid = long.Parse(uri.Split('/').Last());
            return "";

        }

        public int RemoveReminder(ReminderModel reminder)
        {
            //try
            //{
            //    if (reminder.IdCalendarAndroid <= 0)
            //        return -1;
            //    Uri eventUri = ContentUris.WithAppendedId(CalendarContract.Events.ContentUri, reminder.IdCalendarAndroid);
            //    reminder.IdCalendarAndroid = 0;
            //    return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.ContentResolver.Delete(eventUri, null, null);
            //}
            //catch (Exception e)
            //{
                return -1;
            //}

        }

        private string FindCalendarId()
        {
			//var calendarsUri = CalendarContract.Calendars.ContentUri;
			//string[] calendarsProjection = {
			//    CalendarContract.Calendars.InterfaceConsts.Id,
			//    CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
			//    CalendarContract.Calendars.InterfaceConsts.AccountName
			//};
			//ICursor cursor = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);

			//cursor.MoveToNext();
			//cursor.MoveToNext();
			//return cursor.GetString(cursor.GetColumnIndexOrThrow(CalendarContract.Calendars.InterfaceConsts.Id));
			return "";
        }

      
    }
}