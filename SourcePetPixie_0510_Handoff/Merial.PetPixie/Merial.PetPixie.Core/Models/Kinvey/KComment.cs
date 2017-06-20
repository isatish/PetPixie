using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KComment: KResult
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}