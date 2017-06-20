using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public partial class KHashTag
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_acl")]
        public KAcl Acl { get; set; }

        [JsonProperty("_kmd")]
        public KKmd Kmd { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
        public int Age { get; internal set; }
    }
}