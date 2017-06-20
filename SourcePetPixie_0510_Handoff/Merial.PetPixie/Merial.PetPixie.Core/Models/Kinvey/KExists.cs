using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KExists
    {
        [JsonProperty("exists")]
        public bool Exists { get; set; }
    }
}