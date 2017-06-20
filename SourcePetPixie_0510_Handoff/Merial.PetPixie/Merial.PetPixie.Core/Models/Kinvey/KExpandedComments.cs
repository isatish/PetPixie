using System.Collections.Generic;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KExpandedComments
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public IList<KComment> Data { get; set; }
    }
}