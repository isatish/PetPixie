using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
	public class KFile
    {
		[JsonProperty("_type")]
		public string Type { get; set; }

		[JsonProperty("_id")]
		public string Id { get; set; }
	}
}