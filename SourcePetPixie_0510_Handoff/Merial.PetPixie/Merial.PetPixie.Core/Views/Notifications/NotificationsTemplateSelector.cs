using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.Views
{
    public class NotificationsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ReminderTemplate { get; set; }

        public DataTemplate FollowYouTemplate { get; set; }

        public DataTemplate LikePhotoTemplate { get; set; }

        public DataTemplate NotificationDefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            NotificationModel vm = (NotificationModel)item;

            switch (vm.NotificationSource.NotificationType)
            {
                case Models.Enums.NotificationType.Reminder:
                    return ReminderTemplate;

                case Models.Enums.NotificationType.FollowsYou:
                    return FollowYouTemplate;


                case Models.Enums.NotificationType.Likes:
                    return LikePhotoTemplate;



                default:
                    return NotificationDefaultTemplate;

            }

        }
    }
}
