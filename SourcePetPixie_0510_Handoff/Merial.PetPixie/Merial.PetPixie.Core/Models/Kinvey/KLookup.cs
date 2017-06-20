using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KLookup
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}