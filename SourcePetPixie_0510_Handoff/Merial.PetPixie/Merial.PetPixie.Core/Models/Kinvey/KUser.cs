using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KUser
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("_acl")]
        public KAcl Acl { get; set; }

        [JsonProperty("_kmd")]
        public KKmd Kmd { get; set; }
    }
}