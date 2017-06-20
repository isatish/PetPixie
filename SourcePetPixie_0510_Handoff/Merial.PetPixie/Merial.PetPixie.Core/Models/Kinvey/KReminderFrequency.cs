using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KReminderFrequency
    {
        [JsonProperty("errorcode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errordetail")]
        public string ErrorDetail { get; set; }

        [JsonProperty("frequency")]
        public KFrequency[] KFrequencies { get; set; }

    }
}
