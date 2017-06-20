using System;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KResult
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("from")]
        public KFrom From { get; set; }
    }
}