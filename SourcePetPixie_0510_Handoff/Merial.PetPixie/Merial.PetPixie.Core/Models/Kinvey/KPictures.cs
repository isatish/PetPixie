using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
	public class KPictures
    {
		[JsonProperty("small_id")]
		public string SmallId { get; set; }

		[JsonProperty("small")]
		public string Small { get; set; }

		[JsonProperty("medium_id")]
		public string MediumId { get; set; }

		[JsonProperty("medium")]
		public string Mediam {get; set; }
	}
}