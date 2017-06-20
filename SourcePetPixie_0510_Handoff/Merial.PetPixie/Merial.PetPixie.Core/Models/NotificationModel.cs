using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class NotificationModel : INotifyPropertyChanged
    {
        #region Fields

        private string _notificationTitle;
        private string _mediaImageUrl;
        private string _profileImageUrl;
        private NotificationType _type;
        private KProfile _profile;
        private DateTime _creationDate;
        private KNotification _notificationSource;

        private FeedModel _feedModel;

        #endregion

        #region Constructors

        public NotificationModel(KNotification notification)
        {
            this.NotificationTitle = notification?.Profile?.DisplayUsername == null
                                    ? notification.Text
                                    : Regex.Replace(notification.Text, notification.Profile.DisplayUsername, "");
            this.MediaImageUrl     = notification.KMedia?.KExpandedImages?.KSmall?.DownloadURL;
            this.ProfileImageUrl   = notification.Profile?.ExpandedProfilePictures?.KSmall?.DownloadURL;
            this.Type              = notification.NotificationType;
            this.Profile = notification.Profile;
            this.CreationDate = notification.CreatedTime;
            this._notificationSource = notification;

           
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        public string NotificationTitle
        {
            get
            {
                return _notificationTitle;
            }
            set
            {
                if (value == _notificationTitle) return;
                _notificationTitle = value;

                this.RaisePropertyChanged();
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                 this.RaisePropertyChanged();
            }
        }

        public KProfile Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                this.RaisePropertyChanged();
            }
        }

        public string MediaImageUrl
        {
            get { return _mediaImageUrl; }
            set
            {
                if (value == _mediaImageUrl) return;
                _mediaImageUrl = value;

                this.RaisePropertyChanged();
            }
        }

        public string ProfileImageUrl
        {
            get
            {
                if (_profileImageUrl + "" == "")
                {
                    _profileImageUrl = "no_profile_image.png";
                }


                return _profileImageUrl;
            }
            set
            {
                if (value == _profileImageUrl) return;
                _profileImageUrl = value;

                this.RaisePropertyChanged();
            }
        }

        public NotificationType Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;

                this.RaisePropertyChanged();
            }
        }

        public KMedia Media => this._notificationSource.KMedia;

        #region Entities Id

        public string MediaId => _notificationSource.MediaId;

        public string ProfileId => _notificationSource.FromProfileId;

        public string ReminderId => _notificationSource.ReminderId;

        public string Id => _notificationSource.Id;

        public KNotification NotificationSource => _notificationSource;

        #endregion

        #endregion

        #region Methods and Operators

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}