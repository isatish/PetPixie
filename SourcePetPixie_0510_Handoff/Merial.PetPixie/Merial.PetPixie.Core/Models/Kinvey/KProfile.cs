using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KProfile : KEntity
    {
        [JsonProperty("display_username")]
        public string DisplayUsername { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("searchable_display_username")]
        public string SearchableDisplayUsername { get; set; }

        [JsonProperty("searchable_full_name")]
        public string SearchableFullName { get; set; }

        [JsonProperty("notification_settings")]
        public KNotificationSettings NotificationSettings { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
        
        [JsonProperty("leadid")]
        public string LeadId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("vet_ids")]
        public string[] VetIds { get; set; }

        [JsonProperty("pet_ids")]
        public string[] PetIds { get; set; }

        [JsonProperty("counts")]
        public KProfileCounts Counts { get; set; }

        [JsonProperty("pets")]
        public KPetWithReminders[] Pets { get; set; }

        [JsonProperty("vets")]
        public KVet[] Vets { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }
        
        [JsonProperty("expanded_profile_pictures")]
        public KExpandedProfilePictures ExpandedProfilePictures { get; set; }

        [JsonProperty("profile_picture")]
        public KImage ProfilePicture { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("profile_complete_version")]
        public int profileCompleteVersion { get; set; }

        [JsonProperty("email_subscription")]
        public bool EmailSubscription { get; set; }

        
    }
}