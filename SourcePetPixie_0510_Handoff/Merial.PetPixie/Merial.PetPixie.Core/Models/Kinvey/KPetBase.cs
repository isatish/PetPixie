using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KPetBase: KEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("expanded_pictures")]
        public KExpandedImages ExpandedImages { get; set; }
        
        [JsonProperty("vet_ids")]
        public string[] VetIds { get; set; }

		[JsonProperty("picture")]
		public KFile Picture { get; set; }

		[JsonProperty("pictures")]
		public KPictures Pictures { get; set; }

        [JsonProperty("breeds_ids")]
        public string[] Breeds_ids { get; set; }
        
        [JsonProperty("breed_ids")]
        public string[] Breed_ids { get; set; }

        [JsonProperty("owner_profile_id")]
        public string OwnerProfileId { get; set; }

        [JsonProperty("image")]
        public KImage KImage { get; set; }

 
    }
}