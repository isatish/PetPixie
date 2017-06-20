using System.Collections.ObjectModel;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FeedItemViewModel : ViewModelBase<FeedItemViewModel.FeedItemParameters>
    {
        #region Fields

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly string _currentUserId;

        private FeedModel _feed;
        private ObservableCollection<CommentItemViewModel> _comments;
		private bool _isVideo=false;
        private bool _isMedicalAlert;
        private string _textHashTags;
        private string _feedShortDescription;
        private string _feedDescriptionWithoutHashTags;
        private bool _isRemoved = false;

        #endregion

        #region Constructors

        public FeedItemViewModel(IFeedService feedService, IUserService userService)
        {
            _feedService = feedService;
            _userService = userService;
        }

        public FeedItemViewModel(IFeedService feedService, IUserService userService, FeedModel feed)
        {
            _feedService = feedService;
            _userService = userService;
            if (_currentUserId == null)
                _currentUserId = _userService.GetProfileId();
            _feed = feed;

            #region Traitement du (...) en cas de longue description dans un feed.

            var hasTagsRegex = new Regex(@"(?<=#)\w+");
            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            TextHashTags = string.Join(" ", hashTags);
            int descriptionLength = _feed.Description.Length;
            int end;
            if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            {
                FeedShortDescription = _feed.Description.Substring(0, end);
            }
            else
            {
                FeedShortDescription = _feed.Description;
            }

            #endregion

            IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
            Update();
        }

        #endregion

        #region Properties

        public FeedModel Feed
        {
            get { return _feed; }
            set
            {
                _feed = value;
                RaisePropertyChanged(() => Feed);
            }
        }

        public bool IsRemoved
        {
            get { return _isRemoved; }
            set
            {
                _isRemoved = value;
                RaisePropertyChanged(() => IsRemoved);
            }
        }

		 public bool IsVideo
		{
			get { return _isVideo; }
			set
			{
				_isVideo = value;
				RaisePropertyChanged(() => IsVideo);
			}
		}

        public ObservableCollection<CommentItemViewModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMedicalAlert
        {
            get { return _isMedicalAlert; }
            set
            {
                _isMedicalAlert = value;
                RaisePropertyChanged(() => IsMedicalAlert);
            }
        }

        public string TextHashTags
        {
            get { return _textHashTags; }
            set
            {
                _textHashTags = value;
                RaisePropertyChanged(() => TextHashTags);
            }
        }

        public string FeedShortDescription
        {
            get { return _feedShortDescription; }
            set
            {
                _feedShortDescription = value;
                RaisePropertyChanged(() => FeedShortDescription);
            }
        }

        public string FeedDescriptionWithoutHashTags
        {
            get { return _feedDescriptionWithoutHashTags; }
            set
            {
                _feedDescriptionWithoutHashTags = value;
                RaisePropertyChanged(() => FeedDescriptionWithoutHashTags);
            }
        }

        #endregion

        #region Commands

        public ICommand GoLikersCommand => new SafeMvxCommand(GoLikersCommandExecute);

        public IMvxCommand LikeChangedCommand => new SafeMvxCommand(LikeChangedCommandExecute);

        public IMvxCommand AddWoofCommand => new SafeMvxCommand(AddWoofCommandExecute);

        public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);

        public ICommand DetailsCommand => new SafeMvxCommand(Details);

        private void GoLikersCommandExecute()
        {
            if (Feed.NbLikes <= 0)
            {
                Feed.UserHasLiked = true;
                LikeChangedCommand.Execute();
            }
            else
                ShowViewModel<LikeListViewModel, LikeListParameter>(new LikeListParameter()
                {
                    MediaId = Feed.PostId
                });
        }

        private void LikeChangedCommandExecute()
        {
            if (Feed.UserHasLiked)
                Feed.NbLikes++;
            else
                Feed.NbLikes--;

            FeedViewModel.SelectedItem = this;
            LikeChanged();
            RaisePropertyChanged(() => Feed);
        }

        private void AddWoofCommandExecute()
        {
            FeedViewModel.SelectedItem = this;
            ShowViewModel<CommentListViewModel>(new { PostId = Feed.PostId });
        }

        private void ShowProfileCommandExecute()
        {
            if (Feed.Type != TypeMedia.Media) return;
            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            {
                ProfileId = Feed.ProfileId
            });
        }

        #endregion

        #region Methods

        public void Update()
        {
            this.Comments =
                new ObservableCollection<CommentItemViewModel>(
                    Feed.Comments.Select(kComment => new CommentItemViewModel(kComment, this)));

            //Feed.IsUserLike = Feed.Likes.Any(d => _userService.IsUserConnected(d.From.Id));
            //if (Feed != null)
            //{
            //    Feed.Likes = new ObservableCollection<KLike>(Feed.Likes.OrderBy(d => d.From.Id == _currentUserId.Result));
            //}
        }

        public override void Start()
        {
        }

        public virtual async Task LikeChanged()
        {
            if (Feed.UserHasLiked)
            {
                KResult result = await _feedService.Like(Feed.PostId);
                Feed.Likes.Add(new KLike()
                {
                    CreatedTime = result.CreatedTime,
                    From = result.From,
                    Id = result.Id
                });
            }
            else
            {
                await _feedService.UnLike(Feed.PostId);
                Feed.Likes.Remove(Feed.Likes.FirstOrDefault(l => l.From.Id == _currentUserId));
            }
        }

        public void Details()
        {
            FeedViewModel.SelectedItem = this;
            ShowViewModel<FeedItemViewModel, FeedItemParameters>(new FeedItemParameters {Feed = Feed});
        }

        protected override void RealInit(FeedItemParameters parameter)
        {
            _feed = parameter.Feed;
            if (_feed == null)
            {
                Close(this);
                return;
            }
			//Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
			IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
            IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
            var hasTagsRegex = new Regex(@"(?<=#)\w+");
            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            TextHashTags = string.Join(" ", hashTags);
            int descriptionLength = _feed.Description.Length;
            int end;
            if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            {
                FeedShortDescription = _feed.Description.Substring(0, end);
            }
            else
            {
                FeedShortDescription = _feed.Description;
            }
            FeedDescriptionWithoutHashTags = _feed.Description;

            Update();
        }
        
        public override void Started()
        {
            base.Started();
            if (FeedViewModel.SelectedItem != null)
            {
				Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
				//IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
                Feed.NbLikes = FeedViewModel.SelectedItem.Feed.NbLikes;
                Feed.Likes = FeedViewModel.SelectedItem.Feed.Likes;
                Feed.UserHasLiked = FeedViewModel.SelectedItem.Feed.UserHasLiked;
                IsRemoved = FeedViewModel.SelectedItem.IsRemoved;
                Feed = FeedViewModel.SelectedItem.Feed;
                
                Update();
                FeedViewModel.SelectedItem = null;
            }
        }

		public void DeleteFeed()
		{
			 EnsureIsConnected(async () =>
			 {
				 if (await PopupService
			   .DisplayYesNoMessage("Delete feed?", $"Are you sure you want to delete  your feed",
				   "Yes", "No"))
				 {
					 IsFetchingData = true;
					 var result = await _feedService.RemoveFeed(Feed.Media.Id);

					 Close(this);
					 FeedViewModel.SelectedItem = this;
					 FeedViewModel.SelectedItem.IsRemoved = result;
					 IsFetchingData = false;
				 }
			 });
		}

		#endregion

		public class FeedItemParameters
        {
            public FeedModel Feed { get; set; }
        }
    }
}