using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KProduct 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("importantsafetyinformation")]
        public string Information { get; set; }

        
    }
}
