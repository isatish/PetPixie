using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
	public class KInappropriateReport: KEntity
    {
		[JsonProperty("inappropriate_profile_id")]
		public string ProfileId { get; set; }

		[JsonProperty("inappropriate_media_id")]
		public string MediaId { get; set; }

		[JsonProperty("inappropriate_comment_id")]
		public string CommentId { get; set; }
	}
}