using System.Collections.Generic;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KExpandedLikes
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public IList<KLike> Data { get; set; }
    }
}