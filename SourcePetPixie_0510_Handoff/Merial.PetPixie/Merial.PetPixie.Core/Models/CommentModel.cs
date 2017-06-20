using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Models
{
    public class CommentModel : EntityBase
    {
        private string _id;
        private string _imageSrc;
        private string _userName;
        private string _textComment;
        private DateTime _createTime;
        private string _fromProfileId;
        private bool _isSelected;

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string ImageSrc
        {
            get { return _imageSrc; }
            set
            {
                if (value == _imageSrc) return;
                _imageSrc = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string TextComment
        {
            get { return _textComment; }
            set
            {
                if (value == _textComment) return;
                _textComment = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreatedTime
        {
            get { return _createTime; }
            set
            {
                if (value == _createTime) return;
                _createTime = value;
                OnPropertyChanged();
            }
        }
        
        public string FromProfileId
        {
            get { return _fromProfileId; }
            set
            {
                if (value == _fromProfileId) return;
                _fromProfileId = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return true /*_isSelected//*/; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsCreatedByConnectedUser => FromProfileId == Settings.CurrentUserProfile.Id;

        public static CommentModel CreateFrom(KComment cmt)
        {
            return new CommentModel
            {
                Id = cmt.Id,
                TextComment = cmt.Text,
                UserName = cmt.From.DisplayUsername,
                CreatedTime = cmt.CreatedTime,
                _fromProfileId = cmt.From.Id,
                ImageSrc = cmt.From?.ExpandedProfilePictures?.KMedium?.DownloadURL,
                KComment =cmt,
            };
        }

        public KComment KComment { get; private set; }
    }
}