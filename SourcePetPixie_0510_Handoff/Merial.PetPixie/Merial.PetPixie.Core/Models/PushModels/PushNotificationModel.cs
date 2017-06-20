using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Models.PushModels
{
    public class PushNotificationModel : KEntity
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("notification_id")]
        public string NotificationId { get; set; }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("comment_id")]
        public string CommentId { get; set; }

        [JsonProperty("reminder_id")]
        public string ReminderId { get; set; }

        [JsonProperty("media_id")]
        public string MediaId { get; set; }

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