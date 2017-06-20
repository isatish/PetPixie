using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class NotificationsFrameViewModel : ViewModelBase
    {
        #region Fields

        private readonly INotificationService _notificationService;
        private readonly IMediaService _mediaService;
        private readonly IReminderService _reminderService;

        private ObservableCollection<NotificationModel> _notifications;
        private bool _isLoading;
        private IEnumerable<KMedia> _medias;

        #endregion

        #region Constructors

        public NotificationsFrameViewModel(
            INotificationService notificationService, 
            IMediaService mediaService, 
            IReminderService reminderService)
        {
            _notificationService = notificationService;
            _mediaService = mediaService;
            _reminderService = reminderService;
            _notificationService.NewNotificationArrived += (sender, args) => this.ReloadData();
             LoadDataAsync();
        }
        
        #endregion

        #region Public properties

        public ObservableCollection<NotificationModel> Notifications
        {
            get { return _notifications; }
            set
            {
                if (value == _notifications) return;
                _notifications = value;
                UpdateInfoView();
                this.RaisePropertyChanged();
            }
        }

        private void UpdateInfoView()
        {
            RaisePropertyChanged(() => CountLikeNotification);
            RaisePropertyChanged(() => CountCommentNotification);
            RaisePropertyChanged(() => CountReminderNotification);
            RaisePropertyChanged(() => CountHealthAlertNotification);
            RaisePropertyChanged(() => CountFollowAndFriendNotification);
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;

                this.RaisePropertyChanged();
            }
        }

        public int CountLikeNotification
        {
            get
            {
                var count = Notifications?.Count(n => n.Type == NotificationType.Likes);
                if (count != null)
                    return (int) count;
                return 0;
            }
        }


        public int CountCommentNotification
        {
            get
            {
                var count = Notifications?.Count(n => n.Type == NotificationType.Comments || n.Type == NotificationType.MentionsOfYou);
                if (count != null)
                    return (int)count;
                return 0;
            }
        }

        public int CountReminderNotification
        {
            get
            {
                var count = Notifications?.Count(n => n.Type == NotificationType.Reminder);
                if (count != null)
                    return (int)count;
                return 0;
            }
        }

        public int CountHealthAlertNotification
        {
            get
            {
                var count = Notifications?.Count(n => n.Type == NotificationType.NewHealthAlert);
                if (count != null)
                    return (int)count;
                return 0;
            }
        }

        public int CountFollowAndFriendNotification
        {
            get
            {
                var count = Notifications?.Count(n => n.Type == NotificationType.FollowsYou || n.Type == NotificationType.NewFriendsJoinPetAndPixie);
                if (count != null)
                    return (int)count;
                return 0;
            }
        }
        #endregion

        #region Commands

        public IMvxCommand ItemSelectedCommand => new SafeMvxCommandAsync<NotificationModel>(ItemSelectedCommandExecute);

        private async Task ItemSelectedCommandExecute(NotificationModel notificationModel)
        {
            this.IsFetchingData = true;
            try
            { 
              await this._notificationService.SetNewAsRead(notificationModel.NotificationSource);

                switch (notificationModel.Type)
                {
                    case NotificationType.MentionsOfYou:
                    case NotificationType.NewHealthAlert:
                    case NotificationType.Likes:
                    case NotificationType.Comments:
                        await this.ShowPostForNotification(notificationModel);
                        break;
                    case NotificationType.FollowsYou:
                    case NotificationType.NewFriendsJoinPetAndPixie:
                        this.ShowUserForNotification(notificationModel);
                        break;
                    case NotificationType.Reminder:
                        this.ShowReminderForNotification(notificationModel);
                        break;
                }
            }
            catch (Exception e)
            {
                this.Logger.Info($"An error occured while openning the notification with id = {notificationModel?.Id ?? "NO ID"}");
                this.Logger.Error(e);
                this.PopupService.DisplayMessage("Impossible to open the detail of this notification. Please try again", "Error");
            }

            this.IsFetchingData = false;
        }

        public IMvxCommand RemoveNotification => new SafeMvxCommand<NotificationModel>(RemoveNotificationExecute);

        private void RemoveNotificationExecute(NotificationModel notification)
        {
            Notifications.Remove(notification);
            UpdateInfoView();
            this._notificationService.SetNewAsRead(notification.NotificationSource);
        }

        #endregion

        private async void ReloadData()
        {
            await this.LoadDataAsync();
        }

        protected override async Task<bool> LoadDataAsync()
        {
            if (IsLoading) return true; // Prevent loading again after GetNews() is called

            this.IsLoading = true;

            var allNotifications = await this._notificationService.GetNews();

            this.Notifications = new ObservableCollection<NotificationModel>(allNotifications.Select(notif => new NotificationModel(notif)));
           
            await this.PreloadMedia();

            this.IsLoading = false;

             return true;
        }

        private async Task PreloadMedia()
        {
            if(_medias == null) _medias = Enumerable.Empty<KMedia>();

            var loadedMediaId = _medias.Select(media => media.Id);

            var allNotloadMediaId =
                Notifications
                    .Where(notification => notification.MediaId != null)
                    .Select(notification => notification.MediaId)
                    .Except(loadedMediaId)
                    .Distinct();

            this._medias = this._medias.Concat(await _mediaService.GetMediaListById(allNotloadMediaId));
        }

        private void ShowUserForNotification(NotificationModel notificationModel)
        {
            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            {
                ProfileId = notificationModel.ProfileId
            });
        }

        private async void ShowReminderForNotification(NotificationModel notificationModel)
        {
            var reminder = await this._reminderService.GetById(notificationModel.ReminderId);

            if (reminder == null) return;

            if (reminder.TypeModel.SubType == ReminderSubType.Product && !reminder.ProductModel.IsOtherType)
            {
                ShowValidationViewModel<PetReminderProductDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                {
                    Reminder = reminder,
                    PetReminderModel = reminder.PetReminderModel,
                    EntityMode = EntityMode.Edit
                });
            }
            else
            {
                ShowValidationViewModel<PetReminderOtherDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                {
                    Reminder = reminder,
                    PetReminderModel = reminder.PetReminderModel,
                    EntityMode = EntityMode.Edit
                });
            }
        }

        private async Task ShowPostForNotification(NotificationModel notificationModel)
        {
            var media = this._medias.SingleOrDefault(m => m.Id == notificationModel.MediaId) ?? await this._mediaService.GetMediaById(notificationModel.MediaId);
            var kfeed = new KFeed
            {
                KMedia = media,
                Type = media.Type,
                UserId = media.UserId,
                PetIds = media.PetIds?.ToArray()
            };

            this.ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = FeedModel.CreateFrom(kfeed) });
        }
    }
}
