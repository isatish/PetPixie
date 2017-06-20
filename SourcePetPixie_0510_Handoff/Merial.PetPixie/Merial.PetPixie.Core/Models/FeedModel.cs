using Merial.PetPixie.Core.Models.Kinvey;
using System;
using System.Collections.ObjectModel;

namespace Merial.PetPixie.Core.Models
{
    public class FeedModel : EntityBase
    {
        private string _title;
        private string _imageSrcProfile;
        private DateTime _createdAgo;
        private string _imageSrcFeed;
		private string _videoSrcFeed;
        private string _description;
        private bool _userHasLiked;
        private int _nbLikes;
        private TypeMedia _type;
        private ObservableCollection<KLike> _likes;
        private ObservableCollection<KComment> _comments;
        private int _commentsCount;
        private string _imageSrcFeedDiscover;

        public string PostId { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string ImageSrcProfile
        {
            get { return _imageSrcProfile; }
            set
            {
                _imageSrcProfile = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreatedAgo
        {
            get { return _createdAgo; }
            set
            {
                _createdAgo = value;
                OnPropertyChanged();
            }
        }

        public string ImageSrcFeedDiscover
        {
            get { return _imageSrcFeedDiscover; }
            set
            {
                _imageSrcFeedDiscover = value;
                OnPropertyChanged();
            }
        }

        public string ImageSrcFeed
        {
            get { return _imageSrcFeed; }
            set
            {
                _imageSrcFeed = value;
                OnPropertyChanged();
            }
        }

		public string VideoSrcFeed
		{
			get { return _videoSrcFeed; }
			set
			{
				_videoSrcFeed = value;
				OnPropertyChanged();
			}
		}

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public bool UserHasLiked
        {
            get { return _userHasLiked; }
            set
            {
                _userHasLiked = value;
                OnPropertyChanged();
            }
        }

        public int NbLikes
        {
            get { return _nbLikes; }
            set
            {
                _nbLikes = value;
                OnPropertyChanged();
            }
        }

        public TypeMedia Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KLike> Likes
        {
            get { return _likes; }
            set
            {
                _likes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KComment> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        public int CommentsCount
        {
            get { return _commentsCount; }
            set
            {
                _commentsCount = value;
                OnPropertyChanged();
            }
        }

        public KMedia Media { get; set; }

        public string ProfileId { get; set; }

        public static FeedModel CreateFrom(KFeed feed)
        {
            TypeMedia typeMedia;
            string title;
            string imageSrc = null;
			//string videoSrc = null;

            switch (feed.KMedia.Type)
            {
                case "infotainment":
                    title = Constants.Feed.ProductName;
                    typeMedia = TypeMedia.TypeInfotainment;
                    break;
                case "health_alert":
                    title = Constants.Feed.ProductName;
                    typeMedia = TypeMedia.HealthAlert;
                    break;
                case "media":
                    title = feed.KMedia.KProfile.FullName;
                    imageSrc = feed.KMedia.KProfile?.ExpandedProfilePictures?.KSmall?.DownloadURL ?? string.Empty;
                    typeMedia = TypeMedia.Media;
                    break;
                default:
                    title = Constants.Feed.ProductName;
                    typeMedia = TypeMedia.Unknown;
                    break;
            }

            return new FeedModel
            {
                PostId = feed.KMedia.Id,
                ImageSrcFeed = feed.KMedia?.KExpandedImages?.KLarge?.DownloadURL,
				VideoSrcFeed= feed.KMedia?.KVideo?.DownloadURL,
                Description = feed.KMedia.Caption,
                CreatedAgo = feed.KMedia.CreatedTime,
                Title = title,
                ImageSrcProfile = imageSrc,
                NbLikes = feed.KMedia.KExpandedLikes.Count,
                Type = typeMedia,
                Likes = new ObservableCollection<KLike>(feed.KMedia.KExpandedLikes.Data),
                Comments = new ObservableCollection<KComment>(feed.KMedia.KExpandedComments.Data),
                CommentsCount = feed.KMedia.KExpandedComments.Count,
                UserHasLiked = feed.KMedia.UserHasLiked,
                ProfileId = feed.KMedia.ProfileId,
                ImageSrcFeedDiscover = feed.KMedia?.KExpandedImages?.KSmall?.DownloadURL?? feed.KMedia?.KExpandedImages?.KMedium?.DownloadURL ?? feed.KMedia?.KExpandedImages?.KLarge?.DownloadURL,
                Media =feed.KMedia
            };
        }
    }
}