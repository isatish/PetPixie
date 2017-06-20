using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KNotificationSettings
    {
        [JsonProperty("like_media")]
        public bool LikeMedia { get; set; }

        [JsonProperty("comment")]
        public bool Comment { get; set; }

        [JsonProperty("mention")]
        public bool Mention { get; set; }

        [JsonProperty("new_follower")]
        public bool NewFollower { get; set; }

        [JsonProperty("friend_joined")]
        public bool FriendJoined { get; set; }

        [JsonProperty("health_alert")]
        public bool HealthAlert { get; set; }
    }
}
