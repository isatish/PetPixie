using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
	public class KVetRating : KEntity
    {
		[JsonProperty("profile_id")]
		public string ProfileId { get; set; }

		[JsonProperty("user_id")]
		public string UserId { get; set; }

		[JsonProperty("vet_id")]
		public string VetId { get; set; }

		[JsonProperty("score")]
		public int Rate { get; set; }
	}
}