using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KFrom
    {
        [JsonProperty("display_username")]
        public string DisplayUsername { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("expanded_profile_pictures")]
        public KExpandedProfilePictures ExpandedProfilePictures { get; set; }
    }
}