using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KResponseReminder
    {
        [JsonProperty("errorcode", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; set; }

        [JsonProperty("errordetail", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDetail { get; set; }
        
    }
}
