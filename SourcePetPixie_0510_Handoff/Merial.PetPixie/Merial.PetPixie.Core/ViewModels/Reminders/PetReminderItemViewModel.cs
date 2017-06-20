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
using Merial.PetPixie.Core.ViewModels.Reminder;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;

namespace Merial.PetPixie.Core.ViewModels
{
    public class PetReminderItemViewModel : ViewModelBase<PetReminderItemViewModel.PetReminderItemParameters>, INotifyCollectionChanged
    {
        #region Fields

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly IReminderService _reminderService;

        private readonly string _currentUserId;
        private  KReminder _selectedItem;

        private KPetWithReminders _petWithReminders;
        private ObservableCollection<CommentItemViewModel> _comments;
        private bool _isRemoved = false;
        private PetReminderModel _petReminderModel;
          public ObservableCollection<Person> _hashTags;
          private List<string> _feedTags;// = new List<string>();
        private List<ReminderTypeModel> _reminderTypes;
        public event NotifyCollectionChangedEventHandler CollectionChanged;



        #endregion

        #region Constructors

        public PetReminderItemViewModel( IUserService userService)
        {
           // _feedService = feedService;
            _userService = userService;

        }

        public PetReminderItemViewModel(IUserService userService, KPetWithReminders petWithReminders, IReminderService reminderService)
        {
            //  _feedService = feedService;
          
            _userService = userService;
            if (_currentUserId == null)
                _currentUserId = _userService.GetProfileId();
            _petWithReminders = petWithReminders;
            _reminderService = reminderService;


            LoadPetReminderTypes();

            #region Traitement du (...) en cas de longue description dans un feed.

            //var hasTagsRegex = new Regex(@"(?<=#)\w+");
            //var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            //TextHashTags = string.Join(" ", hashTags);
            //int descriptionLength = _feed.Description.Length;
            //int end;
            //if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            //{
            //    FeedShortDescription = _feed.Description.Substring(0, end);
            //}
            //else
            //{
            //    FeedShortDescription = _feed.Description;
            //}

            #endregion

            //if (_petWithReminders.VideoSrcFeed + "" != "")
            //{
            //    var isVideo = true;
            //}

            //IsMedicalAlert = _petWithReminders.Type == TypeMedia.HealthAlert;
            Update();


         //ds   LoadHashTags();
           // HashTags = HashTags;
        }

        #endregion

        #region Properties


        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                if (this._petReminderModel == value) return;
                this.SetProperty(ref this._petReminderModel, value);
            }
        }



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






       







      








          


        public KPetWithReminders PetWithReminders
        {
            get { return _petWithReminders; }
            set
            {
               
                
                _petWithReminders = value;
                RaisePropertyChanged(() => PetWithReminders);
            }
        }


        public bool HasReminders
        {
            get
            {
                bool hasReminders = false;
                if (PetWithReminders.Reminders != null)
                {
                    hasReminders = PetWithReminders.Reminders.Any();
                }
                return hasReminders;
            }

        }

                
        //public string ImageUrlLarge
        //{
        //    get {

        //        string urlLargeImage = "no_profile_image.png";
        //        if (Feed.Media.KExpandedImages != null)
        //        {
        //            if (Feed.Media.KExpandedImages.KLarge != null)
        //            {
        //                urlLargeImage = Feed.Media.KExpandedImages.KLarge.DownloadURL;;
        //            }
        //        }
        //        return urlLargeImage; 
        //    }
        //}

        public string ImageUrlMedium
        {
            get
            {
                
                string urlMediumImage = "no_profile_image.png";
 
                if (PetWithReminders.ExpandedImages != null)
                {
                    if (PetWithReminders.ExpandedImages.KMedium != null)
                    {
                        urlMediumImage = PetWithReminders.ExpandedImages.KMedium.DownloadURL;
                    }


                }
                return urlMediumImage;
            }
        }

        //public string ImageUrlSmall
        //{
        //    get
        //    {

        //        string urlSmallImage = "no_profile_image.png";
        //        if (Feed.Media.KExpandedImages != null)
        //        {
        //            if (Feed.Media.KExpandedImages.KLarge != null)
        //            {
        //                urlSmallImage = Feed.Media.KExpandedImages.KSmall.DownloadURL;
        //            }


        //        }
        //        return urlSmallImage;
        //    }
        //}



        //public string FeedItemTitleImageURL
        //{
        //    get
        //    {
        //        string imageURL = Feed.ImageSrcProfile;

        //        if (imageURL == null)
        //        {
        //            imageURL = "petpixiecirclelogo.png";
        //        }

        //        return imageURL;

        //    }
        //}

                    
        //public string FeedItemTitle
        //{
        //    get { return Feed.Title; }
        //}
             

   
        public override int GetHashCode()
        {
            return base.GetHashCode();
            {

            }
        }


  //      public bool IsRemoved
  //      {
  //          get { return _isRemoved; }
  //          set
  //          {
  //              _isRemoved = value;
  //              RaisePropertyChanged(() => IsRemoved);
  //          }
  //      }



  //             public string CreatedAgoDisplay
  //      {

  //           get {

                 

  //              var value = Feed.CreatedAgo;

  //              var timeAgo = DateTime.UtcNow - value;
  //              if (timeAgo.Days > 365)
  //              {
  //                  int years = (timeAgo.Days / 365);
  //                  if (timeAgo.Days % 365 != 0)
  //                      years += 1;
  //                  return $"{years} y";
  //              }
  //              if (timeAgo.Days > 30)
  //              {
  //                  int months = (timeAgo.Days / 30);
  //                  if (timeAgo.Days % 31 != 0)
  //                      months += 1;
  //                  return $"{months} M";
  //              }
  //              if (timeAgo.Days > 0)
  //                  return $"{timeAgo.Days} d";
  //              if (timeAgo.Hours > 0)
  //                  return $"{timeAgo.Hours} h";
  //              if (timeAgo.Minutes > 0)
  //                  return $"{timeAgo.Minutes} m";
  //              if (timeAgo.Seconds > 5)
  //                  return $"{timeAgo.Seconds} s";
  //              if (timeAgo.Seconds <= 5)
  //                  return "just now";
  //              return string.Empty;
  //          }

  //      }


		// public bool IsVideo
		//{
		//	get { return _isVideo; }
		//	set
		//	{
		//		_isVideo = value;
		//		RaisePropertyChanged(() => IsVideo);
		//	}
		//}

  //      public ObservableCollection<CommentItemViewModel> Comments
  //      {
  //          get { return _comments; }
  //          set
  //          {
  //              _comments = value;
  //              RaisePropertyChanged();
  //              RaisePropertyChanged(() => HasComments);
  //              RaisePropertyChanged(() => NoComments);
  //          }
  //      }

  //      public bool IsMedicalAlert
  //      {
  //          get { return _isMedicalAlert; }
  //          set
  //          {
  //              _isMedicalAlert = value;
  //              RaisePropertyChanged(() => IsMedicalAlert);
  //          }
  //      }

  //      public string TextHashTags
  //      {
  //          get { return _textHashTags; }
  //          set
  //          {
  //              _textHashTags = value;
  //              RaisePropertyChanged(() => TextHashTags);
  //          }
  //      }

  //      public string FeedShortDescription
  //      {
  //          get { return (_feedShortDescription+ "").Trim(); }
  //          set
  //          {
  //              _feedShortDescription = value;
  //              RaisePropertyChanged(() => FeedShortDescription);
  //          }
  //      }

  //      public string FeedDescriptionWithoutHashTags
  //      {
  //          get { return _feedDescriptionWithoutHashTags; }
  //          set
  //          {
  //              _feedDescriptionWithoutHashTags = value;
  //              RaisePropertyChanged(() => FeedDescriptionWithoutHashTags);
  //          }
  //      }

        #endregion

        #region Commands



        public IMvxCommand OpenReminderCommand => new SafeMvxCommand<KReminder>(OpenReminderCommandExecute);

        private void OpenReminderCommandExecute(KReminder kReminder)
        {

            if (kReminder != null)
            {


                var reminderPet = Settings.CurrentUserProfile.Pets.Where(pet => pet.Id == kReminder.PetId).FirstOrDefault();
                var petImageUrl = reminderPet.ExpandedImages.KMedium.DownloadURL;
                ReminderModel reminderModel = ReminderModel.CreateFrom(kReminder, petImageUrl, reminderPet.Name);

            //    this.PetReminderModel = reminderModel;


                if (reminderModel.TypeModel.SubType == ReminderSubType.Product && !reminderModel.ProductModel.IsOtherType)
                {
                     reminderModel.ProductModel = ProductModel.CreateFrom(reminderModel.ProductModel.Id, reminderModel.Name);

                    ShowValidationViewModel<PetReminderProductDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                    {
                        Reminder = reminderModel,
                        PetReminderModel = reminderModel.PetReminderModel,
                        EntityMode = EntityMode.Edit
                    });

                }
                else
                {
                    ShowValidationViewModel<PetReminderOtherDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                    {
                        Reminder = reminderModel,
                        PetReminderModel = reminderModel.PetReminderModel,
                        EntityMode = EntityMode.Edit
                    });
                }
            }
        }



        public KReminder SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                ItemSelectChange();
            }
        }


                
        private void ItemSelectChange()
        {
            OpenReminderCommandExecute(SelectedItem);
        }

 
        public ICommand DetailsCommand => new SafeMvxCommand(Details);

 



                
        public IMvxCommand AddReminderCommand => new SafeMvxCommandAsync(async () => await EnsureIsConnected(AddReminderCommandExecute));

        private async Task AddReminderCommandExecute()
        {
            EnsureIsBusy(AddReminderCoreAction, false);
        }



        private async Task AddReminderCoreAction()
        {
            ShowViewModel<PetReminderSelectTypeViewModel, PetReminderSelectTypeParameter>(
                new PetReminderSelectTypeParameter
                {
                    ReminderTypes = _reminderTypes,
                    PetReminderModel = new PetReminderModel()
                    {
                        PetId = PetWithReminders.Id,
                        PetName = PetWithReminders.Name,
                        PetPictureUrl = PetWithReminders.ExpandedImages.KMedium.DownloadURL
                    }
                });
        }




      



        //private async void LikeChangedCommandExecute()
        //{
        //    if (Feed.UserHasLiked)
        //        Feed.NbLikes--;
        //    else
        //        Feed.NbLikes++;
        //    Feed.UserHasLiked = !Feed.UserHasLiked;

        //    PetReminderItemViewModel.SelectedItem = this;
        //    await LikeChanged();

          

        //    RaisePropertyChanged(() => Feed);
        // //   RaisePropertyChanged(() => Feed.UserHasLiked);
        //    RaisePropertyChanged(() => YouLiked);
        //    RaisePropertyChanged(() => YouNotLiked);
        //    //  RaisePropertyChanged(() => Feed.NbLikes);
        ////    Update();
        //}

        //private void AddWoofCommandExecute()
        //{
        //    KPetWithReminders.SelectedItem = this;
        //    ShowViewModel<CommentListViewModel>(new { PostId = Feed.PostId });
        //}

        //private void ShowProfileCommandExecute()
        //{
        //    if (PetReminderModel.Type != TypeMedia.Media) return;
        //    ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
        //    {
        //        ProfileId = Feed.ProfileId
        //    });
        //}





        #endregion

        #region Methods

                

        public void Update()
        {
           // this.Comments =
           //     new ObservableCollection<CommentItemViewModel>(
           //         Feed.Comments.Select(kComment => new CommentItemViewModel(kComment, this)));

            //Feed.IsUserLike = Feed.Likes.Any(d => _userService.IsUserConnected(d.From.Id));
            //if (Feed != null)
            //{
            //    Feed.Likes = new ObservableCollection<KLike>(Feed.Likes.OrderBy(d => d.From.Id == _currentUserId.Result));
            //}


            //Feed.UserHasLiked = Feed.Likes.Any(d => d.From.Id == _currentUserId);//  _userService.UserConnected());//d.From.Id));
            //            //if (Feed != null)
            //{
            //    Feed.Likes = new ObservableCollection<KLike>(Feed.Likes.OrderBy(d => d.From.Id == _currentUserId));
            //}
        }

        public override void Start()
        {
        }

        //public virtual async Task LikeChanged()
        //{
        //    if (Feed.UserHasLiked)
        //    {
        //        KResult result = await _feedService.Like(Feed.PostId);
        //        Feed.Likes.Add(new KLike()
        //        {
        //            CreatedTime = result.CreatedTime,
        //            From = result.From,
        //            Id = result.Id
        //        });
        //    }
        //    else
        //    {
        //        await _feedService.UnLike(Feed.PostId);
        //        Feed.Likes.Remove(Feed.Likes.FirstOrDefault(l => l.From.Id == _currentUserId));
        //    }
        //    UpdateItemStats();
        //}

        public void Details()
        {
        //    PetReminderItemViewModel.SelectedItem = this;
        //    ShowViewModel<PetReminderItemViewModel, PetReminderItemParameters>(new PetReminderItemParameters { PetWithReminders = PetWithReminders});
        }

        protected override void RealInit(PetReminderItemParameters parameter)
        {
            _petWithReminders = parameter.PetWithReminders;
             this.PetReminderModel = parameter.PetReminderModel;
            if (_petWithReminders == null)
            {
                Close(this);
                return;
            }
            //Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
           // Feed.VideoSrcFeed = "https://storage.googleapis.com/0ba3840452fb46ab9062c340c32a7eb5/198fb442-4db3-4418-b640-c915ae34bf33/f7e9fae5-6e10-4dc6-9ec7-3fa04fc0ca6d.mp4";
			//IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
            //IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
            var hasTagsRegex = new Regex(@"(?<=#)\w+");
            //var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
            //TextHashTags = string.Join(" ", hashTags);

            //if (hashTags.Count() >= 1) { _hashtag0 = hashTags.ElementAt(0).ToString();}
            //if (hashTags.Count() >= 2) { _hashtag0 = hashTags.ElementAt(1).ToString(); }





            //int descriptionLength = _feed.Description.Length;
            //int end;
            //if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
            //{
            //    FeedShortDescription = _feed.Description.Substring(0, end);
            //}
            //else
            //{
            //    FeedShortDescription = _feed.Description;
            //}
            //FeedDescriptionWithoutHashTags = _feed.Description;

            //Update();

            //LoadHashTags();
        }


                
        private async Task<bool> LoadPetReminderTypes()
        {
            _reminderTypes = await _reminderService.GetAllReminderType();
            return true;
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
           //ds  if (PetReminderListViewModel.SelectedItem != null)
           //ds {
                //dsFeed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
                //IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
                //ds Feed.NbLikes = FeedViewModel.SelectedItem.Feed.NbLikes;
                //ds Feed.Likes = FeedViewModel.SelectedItem.Feed.Likes;
                //ds Feed.UserHasLiked = FeedViewModel.SelectedItem.Feed.UserHasLiked;
                //ds IsRemoved = FeedViewModel.SelectedItem.IsRemoved;


              //ds  PetReminderModel = PetReminderItemViewModel.SelectedItem.Feed;

          //ds      Update();
              //ds  PetReminderItemViewModel.SelectedItem = null;
        //ds    }
        }

		//public void DeleteFeed()
		//{
		//	 EnsureIsConnected(async () =>
		//	 {
		//		 if (await PopupService
		//	   .DisplayYesNoMessage("Delete feed?", $"Are you sure you want to delete  your feed",
		//		   "Yes", "No"))
		//		 {
		//			 IsFetchingData = true;
		//			 var result = await _feedService.RemoveFeed(Feed.Media.Id);

		//			 Close(this);
		//			 FeedViewModel.SelectedItem = this;
		//			 FeedViewModel.SelectedItem.IsRemoved = result;
		//			 IsFetchingData = false;
		//		 }
		//	 });
		//}

		#endregion

		public class PetReminderItemParameters
        {
            public KPetWithReminders    PetWithReminders { get; set; }
            public PetReminderModel PetReminderModel { get; set; }
        }
    }
}