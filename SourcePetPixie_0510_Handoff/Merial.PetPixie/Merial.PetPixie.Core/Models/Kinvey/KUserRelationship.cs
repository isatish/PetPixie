using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KUserRelationship : KEntity
    {
        // TODO : En faire une enum pour le Type
        public const string TYPE_FOLLOW = "follow";
        public const string TYPE_BLOCK  = "block";

        [JsonProperty("to_profile_id")]
        public string ToProfileId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("from_profile_id")]
        public string FromProfileId { get; set; }

        [JsonProperty("from_profile")]
        public KProfile FromProfile { get; set; }

        [JsonProperty("to_profile")]
        public KProfile ToProfile { get; set; }
    }
}