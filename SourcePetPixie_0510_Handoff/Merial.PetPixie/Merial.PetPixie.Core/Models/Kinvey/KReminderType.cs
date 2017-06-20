using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public  class KReminderType
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("products")]
        public KProduct[] Products { get; set; }
    }
}
