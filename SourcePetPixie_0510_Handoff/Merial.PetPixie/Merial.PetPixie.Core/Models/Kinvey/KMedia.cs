using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KMedia : KEntity
    {
        [JsonProperty("sharepoint_id")]
        public string SharepointId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("image")]
        public KImage KImage { get; set; }

		[JsonProperty("video")]
		public KVideo KVideo { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("Species")]
        public string Species { get; set; }

        [JsonProperty("Breed")]
        public string Breed { get; set; }

        [JsonProperty("Age")]
        public string Age { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Publication_date")]
        public string PublicationDate { get; set; }

        [JsonProperty("Last_published_date")]
        public string LastPublishedDate { get; set; }

        [JsonProperty("Expiration_date")]
        public string ExpirationDate { get; set; }

        [JsonProperty("images")]
        public KImages KImages { get; set; }

        [JsonProperty("sort_index")]
        public long SortIndex { get; set; }
        
        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("likes")]
        public IList<KLike> KLikes { get; set; }

        [JsonProperty("expanded_likes")]
        public KExpandedLikes KExpandedLikes { get; set; }
        
        [JsonProperty("videos")]
        public IList<object> KVideos { get; set; }

        [JsonProperty("comments")]
        public IList<KComment> Comments { get; set; }

        [JsonProperty("expanded_comments")]
        public KExpandedComments KExpandedComments { get; set; }

        [JsonProperty("expanded_pets")]
        public IList<object> ExpandedPets { get; set; }
        
        [JsonProperty("pets_ids")]
        public IList<string> PetIds { get; set; }
        
        [JsonProperty("user_has_liked")]
        public bool UserHasLiked { get; set; }

        [JsonProperty("expanded_images")]
        public KExpandedImages KExpandedImages { get; set; }

        [JsonProperty("_geoloc")]
        public IList<double> Geoloc { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("profile")]
        public KProfile KProfile { get; set; }
    }
}