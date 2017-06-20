using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KExpandedProfilePictures
    {
        [JsonProperty("small")]
        public KSmall KSmall { get; set; }

        [JsonProperty("exlarge")]
        public KSmall KLarge { get; set; }

        [JsonProperty("medium")]
        public KMedium KMedium { get; set; }
    }
}
