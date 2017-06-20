using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KFeed
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("media")]
        public KMedia KMedia { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("pets_ids")]
		public string[] PetIds { get; set; }
    }
}