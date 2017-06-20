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
using System;
using Merial.PetPixie.Core.Models.Enums;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FeedItemViewModel : ViewModelBase<FeedItemViewModel.FeedItemParameters>, INotifyCollectionChanged
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

                private bool _isLiked = true;
        private bool _isNotLiked = true;
        private bool _hasComments = false;
        private bool _noComments = true;

          public ObservableCollection<Person> _hashTags;
          private List<string> _feedTags;// = new List<string>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private string _hashtag0 = "";
        private string _hashtag1 = "";
        private string _hashtag2 = "";
        private string _hashtag3 = "";
        private string _hashtag4 = "";

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

            if (_feed.VideoSrcFeed + "" != "")
            {
                var isVideo = true;
            }

            #region Traitement du (...) en cas de longue description dans un feed.

            var hasTagsRegex = new Regex(@"(?<=#)\w+");
            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            TextHashTags = string.Join(" ", hashTags);
            int descriptionLength = _feed.Description.Length;
            int end;
            if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            {
                FeedShortDescription = _feed.Description.Substring(0, end) + "...";
            }
            else
            {
                FeedShortDescription = _feed.Description;
            }

            #endregion

            if (_feed.VideoSrcFeed + "" != "")
            {
                var isVideo = true;
            }

            IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
            Update();


            LoadHashTags();
           // HashTags = HashTags;
        }

        #endregion

        #region Properties

        //      public List<string> WrapTagNames
        //{

        //    get
        //    {
        //        var itemList = new List<string>();

        //        itemList.Add("FunFacts");
        //        itemList.Add("Breed");
        //        return itemList;//new List() { "FunFacts", "Breed" };



        //    }


        //}


         public ObservableCollection<Person> HashTags
        {
            get
            {
                return _hashTags;
            }


            set 
            {
                _hashTags = value;


                RaisePropertyChanged(() => HashTags); 
            } 
        }


                
        public string HashTag0
        {
            get
            {
                return _hashtag0;
            }


            set 
            {
                _hashtag0 = value;


                RaisePropertyChanged(() => HashTag0); 
            } 
        }

                
        public string HashTag1
        {
            get
            {
                return _hashtag1;
            }


            set 
            {
                _hashtag1 = value;


                RaisePropertyChanged(() => HashTag1); 
            } 
        }


                 
        public string HashTag2
        {
            get
            {
                return _hashtag2;
            }

            set {
                _hashtag2 = value;


                RaisePropertyChanged(() => HashTag2);
            }
  
        }


        public string HashTag3
        {
            get
            {
                return _hashtag3;
            }


            set
            {
                _hashtag3 = value;


                RaisePropertyChanged(() => HashTag3);
            }

        }

                public string HashTag4
        {
            get
            {
                return _hashtag4;
            }


            set
            {
                _hashtag4 = value;


                RaisePropertyChanged(() => HashTag4);
            }
  
        }







        public List<string> FeedTags
        {

            get
            {
                return _feedTags;
                //var itemList = new List<string>();

                //itemList.Add("FunFacts");
                //itemList.Add("Breed");
                //return itemList;//new List() { "FunFacts", "Breed" };
            }
            set
            {
                _feedTags = value;
                RaisePropertyChanged(() => FeedTags);
            }

        }





        //public List<string> WrapTagNames
        //{

        //    get
        //    {
        //       var itemList = new List<string>();

        //        itemList.Add("FunFacts");
        //        itemList.Add("Breed");
        //        return  itemList;//new List() { "FunFacts", "Breed" };
        //    }

        //}






      



        private bool LoadHashTags()
        {

             var hasTagsRegex = new Regex(@"(?<=#)\w+");
            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            TextHashTags = string.Join(" ", hashTags);

            if (hashTags.Count() >= 1) { _hashtag0 = hashTags.ElementAt(0).ToString(); }
            if (hashTags.Count() >= 2) { _hashtag1 = hashTags.ElementAt(1).ToString(); }
            if (hashTags.Count() >= 3) { _hashtag2 = hashTags.ElementAt(2).ToString(); }
            if (hashTags.Count() >= 4) { _hashtag3 = hashTags.ElementAt(3).ToString(); }
             if (hashTags.Count() >= 5) { _hashtag4 = hashTags.ElementAt(4).ToString(); }





             var tempPersons = new ObservableCollection<Person>();

                        for (int i = 0; i < 3; i++)
            {
                var person = new Person { Name = "person " + i, Age = 10 + i };
                tempPersons.Add(person);
            }

            HashTags = tempPersons;


//           var tempHashTags = new ObservableCollection<KHashTag>();
//         //   LoadData();

//             var hashTag1 = new KHashTag { Text = "FunFacts", Id = "def", Age = 10 };
//            tempHashTags.Add(hashTag1);

//             var hashTag2 = new KHashTag { Text = "Cute", Id = "abc", Age = 11 };
//            tempHashTags.Add(hashTag2);

//            HashTags = tempHashTags;
////            RaisePropertyChanged(() => HashTags);

            return true;
        }

        //private void LoadData()
        //{
        //    for (int i = 0; i < 20; i++)
        //    {
        //        var hashTag = new KHashTag {  Text = "FunFacts", Id=i.ToString(),   Age = 10 + i };
        //        HashTags.Add(hashTag);
        //    }
        //}







        //public List<string> WrapTagNames
        //{
            
        //    get
        //    {
        //       var itemList = new List<string>();

        //        itemList.Add("FunFacts");
        //        itemList.Add("Breed");
        //       return  itemList;//new List() { "FunFacts", "Breed" };
        //    }

        //}
           
        public string FeedLikerNames
        {
            get
            {
                var value = this.Feed;
                string likeUser0 = string.Empty;
                string likeUser1 = string.Empty;
                var currentUser = Settings.CurrentUserProfile;

                if (value.UserHasLiked)
                {
                    likeUser0 = "You";
                    likeUser1 = value.Likes.FirstOrDefault(u => u.From.Id != currentUser?.Id)?.From?.DisplayUsername;
                }
                else
                {
                    if (value.NbLikes > 0)
                    {
                        likeUser0 = value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList()[0].From.DisplayUsername;
                    }

                    if (value.NbLikes > 1)

                        try
                        {
                            var list = value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList();
                            if (list.Count() > 1)
                            {
                                likeUser1 = list[1].From.DisplayUsername;
                            }
                            else
                            {
                                return $"{value.NbLikes} buddies like this";
                            }
                        }
                        catch (Exception e)
                        {
                            var exp = e;
                        }
                }

#if DEBUG
                if (String.IsNullOrWhiteSpace(likeUser0))
                {
                    likeUser0 = "No Name";
                }
                if (String.IsNullOrWhiteSpace(likeUser1))
                {
                    likeUser1 = "No Name";
                }
#endif



                switch (value.NbLikes)
                {
                    case 0:
                        return "Be the first to like this";
                    case 1:
                        if (value.UserHasLiked)
                            return $"{likeUser0} like this";
                        else
                            return $"{likeUser0} likes this";
                    case 2:
                        if (string.IsNullOrWhiteSpace(likeUser1))
                            return $"{likeUser0} and 1 buddy like this";
                        else
                            return $"{likeUser0} and {likeUser1} like this";
                    default:
                        return $"{likeUser0} and {value.NbLikes - 1} buddies like this";
                }
            }

        }





        public bool HasComments
        {
            get {
                
                _hasComments = false;
                 
                if (Comments != null)
                {
                    if (Comments.Count > 0)
                    {
                        _hasComments = true;
                    }
                }


                return _hasComments; 
            
            
            }

        }


        public bool NoComments
        {
            get
            {

                _noComments = true;

                if (Comments != null)
                {

                    if (Comments.Count > 0)
                    {
                        _noComments = false;
                    }

                }


                return _noComments;

            }
            set
            {
                _noComments = value;
                RaisePropertyChanged(() => NoComments);
            }
        }




        public virtual bool YouLiked
        {
            get
            {
 
                return Feed.UserHasLiked;

            }
        }

        public virtual bool YouNotLiked
        {
            get
            {

                return !Feed.UserHasLiked;

            }
        }









        public virtual bool IsLiked
        {
            get
            {
                bool liked = false;
                if (Feed.NbLikes > 0)
                {
                    liked = true;

                }
                return liked;

            }
        }

        public virtual bool IsNotLiked
        {
            get {

                return (Feed.NbLikes == 0);

                }
        }


        public FeedModel Feed
        {
            get { return _feed; }
            set
            {
                _feed = value;
                RaisePropertyChanged(() => Feed);
            }
        }

                
        public string ImageUrlLarge
        {
            get {

                string urlLargeImage = "no_profile_image.png";
                if (Feed.Media.KExpandedImages != null)
                {
                    if (Feed.Media.KExpandedImages.KLarge != null)
                    {
                        urlLargeImage = Feed.Media.KExpandedImages.KLarge.DownloadURL;;
                    }
                }
                return urlLargeImage; 
            }
        }

        public string ImageUrlMedium
        {
            get {

                string urlMediumImage = "no_profile_image.png";
                if (Feed.Media.KExpandedImages != null)
                {
                    if (Feed.Media.KExpandedImages.KLarge != null)
                    {
                        urlMediumImage = Feed.Media.KExpandedImages.KMedium.DownloadURL;
                    }


                }
                return urlMediumImage; 
            }
        }

        public string ImageUrlSmall
        {
            get
            {

                string urlSmallImage = "no_profile_image.png";
                if (Feed.Media.KExpandedImages != null)
                {
                    if (Feed.Media.KExpandedImages.KLarge != null)
                    {
                        urlSmallImage = Feed.Media.KExpandedImages.KSmall.DownloadURL;
                    }


                }
                return urlSmallImage;
            }
        }



        public string FeedItemTitleImageURL
        {
            get
            {
                string imageURL = Feed.ImageSrcProfile;

                if (imageURL == null)
                {
                    imageURL = "petpixiecirclelogo.png";
                }

                return imageURL;

            }
        }

                    
        public string FeedItemTitle
        {
            get { return Feed.Title; }
        }
             

   
        public override int GetHashCode()
        {
            return base.GetHashCode();
            {

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



               public string CreatedAgoDisplay
        {

             get {

                 

                var value = Feed.CreatedAgo;

                var timeAgo = DateTime.UtcNow - value;
                if (timeAgo.Days > 365)
                {
                    int years = (timeAgo.Days / 365);
                    if (timeAgo.Days % 365 != 0)
                        years += 1;
                    return $"{years} y";
                }
                if (timeAgo.Days > 30)
                {
                    int months = (timeAgo.Days / 30);
                    if (timeAgo.Days % 31 != 0)
                        months += 1;
                    return $"{months} M";
                }
                if (timeAgo.Days > 0)
                    return $"{timeAgo.Days} d";
                if (timeAgo.Hours > 0)
                    return $"{timeAgo.Hours} h";
                if (timeAgo.Minutes > 0)
                    return $"{timeAgo.Minutes} m";
                if (timeAgo.Seconds > 5)
                    return $"{timeAgo.Seconds} s";
                if (timeAgo.Seconds <= 5)
                    return "just now";
                return string.Empty;
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
                RaisePropertyChanged(() => HasComments);
                RaisePropertyChanged(() => NoComments);
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
            get { return (_feedShortDescription+ "").Trim(); }
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


        //Code Smell - connected to lack of Wrap Panel binding
        public ICommand GoTagCommand => new SafeMvxCommand(GoTagCommandExecute);
        public ICommand GoTag1Command => new SafeMvxCommand(GoTag1CommandExecute);
        public ICommand GoTag2Command => new SafeMvxCommand(GoTag2CommandExecute);
        public ICommand GoTag3Command => new SafeMvxCommand(GoTag3CommandExecute);
        public ICommand GoTag4Command => new SafeMvxCommand(GoTag4CommandExecute);





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

                
        private void GoTagCommandExecute()
        {
                HashTag0 = HashTag0.Replace("#", "");
                ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
                {
                    SearchMode = SearchMode.Tag,
                    SearchEntry = HashTag0.ToLower(),
                    TitlePage = "#" + HashTag0
                });
        }


         private void GoTag1CommandExecute()
        {
            HashTag1 = HashTag1.Replace("#", "");
            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = HashTag1.ToLower(),
                TitlePage = "#" + HashTag1
            });
        }




         private void GoTag2CommandExecute()
        {
            HashTag2 = HashTag2.Replace("#", "");
            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = HashTag2.ToLower(),
                TitlePage = "#" + HashTag2
            });
        }

         
         private void GoTag3CommandExecute()
        {
            HashTag3 = HashTag3.Replace("#", "");
            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = HashTag3.ToLower(),
                TitlePage = "#" + HashTag3
            });
        }

         private void GoTag4CommandExecute()
        {
            HashTag4 = HashTag4.Replace("#", "");
            ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            {
                SearchMode = SearchMode.Tag,
                SearchEntry = HashTag4.ToLower(),
                TitlePage = "#" + HashTag4
            });
        }


        private async void LikeChangedCommandExecute()
        {
            Feed.UserHasLiked = !Feed.UserHasLiked;  //ds
            if (Feed.UserHasLiked)
                Feed.NbLikes++;
            else
                Feed.NbLikes--;

            FeedViewModel.SelectedItem = this;
            await LikeChanged();
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
           // Feed.VideoSrcFeed = "https://storage.googleapis.com/0ba3840452fb46ab9062c340c32a7eb5/198fb442-4db3-4418-b640-c915ae34bf33/f7e9fae5-6e10-4dc6-9ec7-3fa04fc0ca6d.mp4";
			IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
            IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
            var hasTagsRegex = new Regex(@"(?<=#)\w+");
            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            TextHashTags = string.Join(" ", hashTags);

            if (hashTags.Count() >= 1) { _hashtag0 = hashTags.ElementAt(0).ToString();}
            if (hashTags.Count() >= 2) { _hashtag0 = hashTags.ElementAt(1).ToString(); }





            int descriptionLength = _feed.Description.Length;
            int end;
            if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            {
                FeedShortDescription = _feed.Description.Substring(0, end)  + "...";
            }
            else
            {
                FeedShortDescription = _feed.Description;
            }
            FeedDescriptionWithoutHashTags = _feed.Description;

            Update();

            LoadHashTags();
        }
        
        public override void Started()
        {
            base.Started();
            UpdateItemStats();
            /*
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
            */
        }

        private void UpdateItemStats()
        {
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