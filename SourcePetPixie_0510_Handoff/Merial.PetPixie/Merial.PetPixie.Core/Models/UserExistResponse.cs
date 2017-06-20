using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models {
    public class UserExistResponse
    {
        [JsonProperty("usernameExists")]
        public bool UsernameExists { get; set; }
    }
}