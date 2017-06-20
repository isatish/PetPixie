using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KProfileCounts
    {
        [JsonProperty("follows")]
        public int Follows { get; set; }

        [JsonProperty("followed_by")]
        public int FollowedBy{ get; set; }

        [JsonProperty("media")]
        public int Media { get; set; }
    }
}