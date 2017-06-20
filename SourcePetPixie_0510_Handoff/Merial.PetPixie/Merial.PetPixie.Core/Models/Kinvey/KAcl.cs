using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KAcl
    {
        [JsonProperty("creator")]
        public string Creator { get; set; }
    }
}