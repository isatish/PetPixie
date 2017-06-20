using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KImages
    {
        [JsonProperty("small_id")]
        public string SmallId { get; set; }

        [JsonProperty("large_id")]
        public string LargeId { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("medium_id")]
        public string MediumId { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}