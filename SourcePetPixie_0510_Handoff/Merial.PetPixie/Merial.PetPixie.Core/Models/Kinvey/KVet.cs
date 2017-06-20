using Newtonsoft.Json;
using System.Collections.Generic;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KVet
    {
        [JsonProperty("_acl")]
        public KAcl Acl { get; set; }

        [JsonProperty("_kmd")]
        public KKmd Kmd { get; set; }

        [JsonProperty("rating")]
        public KRating Rating { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("_geoloc")]
        public IList<double> Geoloc { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("user_rating")]
        public int UserRating { get; set; }
    }
}