using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KExpandedImages
    {
        [JsonProperty("small")]
        public KSmall KSmall { get; set; }

        [JsonProperty("large")]
        public KLarge KLarge { get; set; }

        [JsonProperty("medium")]
        public KMedium KMedium { get; set; }
    }
}