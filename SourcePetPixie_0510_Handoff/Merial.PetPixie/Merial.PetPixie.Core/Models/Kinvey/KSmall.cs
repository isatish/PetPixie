using System;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KSmall
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_downloadURL")]
        public string DownloadURL { get; set; }

        [JsonProperty("_expiresAt")]
        public DateTime ExpiresAt { get; set; }
    }
}