using System;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KReminder : KResponseReminder
    {

        //[JsonProperty("name")]
        //public string Name { get; set; }

        [JsonProperty("reminder_type_id")]
        public string ReminderTypeId { get; set; }

        [JsonProperty("name")]
        public string TypeName{ get; set; }

        [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("eventid", NullValueHandling = NullValueHandling.Ignore)]
        public string EventId { get; set; }

      

        [JsonProperty("reminder_title", NullValueHandling = NullValueHandling.Ignore)]
        public string ReminderTypeTitle { get; set; }

        [JsonProperty("otherproductname", NullValueHandling = NullValueHandling.Ignore)]
        public string OtherProductName { get; set; }

        [JsonProperty("pet_id")]
        public string PetId { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("sync_with_local_calendar")]
        public bool SyncCalendar { get; set; }


        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("location_vet_id")]
        public string LocationVetId { get; set; }

        [JsonProperty("location_custom_text")]
        public string LocationCustomText { get; set; }

        [JsonProperty("reminder_date")]
        public DateTime? ReminderDate { get; set; }

        [JsonProperty("reminderrecurrencevalue")]
        public int ReminderOccurenceValue => 0;

        [JsonProperty("firsttimereminder")]
        public bool FirstTimerReminder => true;
        

        [JsonProperty("reminderrecurrenceintervalid", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReminderOccurenceIntervalId { get; set; }


    }
}
