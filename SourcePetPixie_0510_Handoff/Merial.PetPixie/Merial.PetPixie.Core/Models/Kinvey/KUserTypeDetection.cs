using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KUserTypeDetection
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }
    }
}