using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KRating
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}