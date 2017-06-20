using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public abstract class KEntity
    {

        [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("_acl", NullValueHandling = NullValueHandling.Ignore)]
        public KAcl Acl { get; set; }

        [JsonProperty("_kmd", NullValueHandling = NullValueHandling.Ignore)]
        public KKmd Kdm { get; set; }
    }
}