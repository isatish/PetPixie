using System.ComponentModel;
using System.Runtime.CompilerServices;
using Merial.PetPixie.Core.Annotations;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Models
{
    public class NotificationSettingModel : ViewModelBase
    {
        private bool _isChecked;

        public NotificationSettingModel(bool isChecked, NotificationType type)
        {
            IsChecked = isChecked;
            Type = type;
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value == _isChecked) return;
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public NotificationType Type { get; set; }

        public string Name
        {
            get
            {
                switch (this.Type)
                {
                    case NotificationType.Likes:
                        return "Likes";
                    case NotificationType.Comments:
                        return "Comments";
                    case NotificationType.MentionsOfYou:
                        return "Mentions of you";
                    case NotificationType.FollowsYou:
                        return "Follows you";
                    case NotificationType.NewFriendsJoinPetAndPixie:
                        return "New friends join Pet";
                    case NotificationType.NewHealthAlert:
                        return "New health alert";
                    //case NotificationType.Reminder:
                    //    return "reminder";
                    default:
                        return string.Empty;
                }
            }
        }

        public string Detail
        {
            get
            {
                switch (this.Type)
                {
                    case NotificationType.Likes:
                        return "Pet+ user likes your photo";
                    case NotificationType.Comments:
                        return "Pet+ user commented on your photo";
                    case NotificationType.MentionsOfYou:
                        return "Pet+ user mentioned you in a photo";
                    case NotificationType.FollowsYou:
                        return "Pet+ user started following you";
                    case NotificationType.NewFriendsJoinPetAndPixie:
                        return "One of your friends join Pet+";
                    case NotificationType.NewHealthAlert:
                        return "Health alerts customized for your pet and your location";
                    //case NotificationType.Reminder:
                    //    return "reminder";
                    default:
                        return string.Empty;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IMvxCommand SwitchCheckedCommand => new SafeMvxCommand(this.DoSwitchCheckedCommand);

        private void DoSwitchCheckedCommand()
        {
            this.IsChecked = !this.IsChecked;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}