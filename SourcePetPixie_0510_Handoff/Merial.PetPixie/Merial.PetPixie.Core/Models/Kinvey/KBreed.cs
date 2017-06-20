using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KBreed
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_acl")]
        public KAcl Acl { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }
    }
}