using System;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KKmd
    {
        [JsonProperty("ect")]
        public DateTime Ect { get; set; }

        [JsonProperty("lmt")]
        public DateTime Lmt { get; set; }
    }
}