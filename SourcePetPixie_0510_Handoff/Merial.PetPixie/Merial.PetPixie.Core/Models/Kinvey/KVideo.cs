using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
	public class KVideo
	{
        [JsonProperty("_type")]
		public string Type { get; set; }

		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("mimeType")]
		public string MimeType { get; set; }

		[JsonProperty("_downloadURL")]
		public string DownloadURL { get; set; }
	}
}
