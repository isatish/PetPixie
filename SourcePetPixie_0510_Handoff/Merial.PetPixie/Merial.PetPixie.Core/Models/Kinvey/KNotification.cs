using System;
using Merial.PetPixie.Core.Models.Enums;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.Kinvey
{
    public class KNotification : KEntity
    {
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("reminder_id")]
        public string ReminderId { get; set; }

        [JsonProperty("read")]
        public bool Read { get; set; }
        
        [JsonProperty("from_user_id")]
        public string FromUserId { get; set; }

        [JsonProperty("from_profile_id")]
        public string FromProfileId { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("profile")]
        public KProfile Profile { get; set; }

        [JsonProperty("media")]
        public KMedia KMedia { get; set; }

        [JsonIgnore]
        public NotificationType NotificationType
        {
            get
            {
                switch (this.Type)
                {
                    case "health_alert":
                        return NotificationType.NewHealthAlert;
                    case "user_liked_your_media":
                        return NotificationType.Likes;
                    case "user_following_you":
                        return NotificationType.FollowsYou;
                    case "mentioned_in_media_caption":
                    case "mentioned_in_media_comment":
                        return NotificationType.MentionsOfYou;
                    case "user_commented_on_your_media":
                        return NotificationType.Comments;
                    case "reminder":
                        return NotificationType.Reminder;
                    default: // TODO : Récupérer le bon string pour ce type
                        return NotificationType.NewFriendsJoinPetAndPixie;
                        //throw new NotImplementedException($"Le type de notification {this.Type} n'est pas supporté");
                }
            }
        }
    }
}