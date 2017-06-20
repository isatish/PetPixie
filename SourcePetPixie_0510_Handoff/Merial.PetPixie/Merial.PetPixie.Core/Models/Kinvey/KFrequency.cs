using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KFrequency
    {
        [JsonProperty("intervalid")]
        public string IntervalId { get; set; }

     
        [JsonProperty("interval")]
        public string Interval { get; set; }

        //[JsonProperty("value")]
        //public int Value { get; set; }

        //[JsonProperty("type")]
        //public int Type { get; set; }
    }
}
