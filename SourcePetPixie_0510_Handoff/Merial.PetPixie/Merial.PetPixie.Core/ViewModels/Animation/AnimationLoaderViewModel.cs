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
using SimpleTimerPortable;

namespace Merial.PetPixie.Core.ViewModels
{
    public class AnimationLoaderViewModel : ViewModelBase, IMvxNotifyPropertyChanged
    {
        private Timer timer;
        int currImage = 0;

        public event NotifyCollectionChangedEventHandler CollectionChanged;


        #region Constructors

        public AnimationLoaderViewModel()
        {
            //    _feedService = feedService;
            //   _userService = userService;

            DoAnimation();
        }

        private bool _image0Visible = true;
        private bool _image1Visible = false;
        private bool _image2Visible = false;
        private bool _image3Visible = false;
        private bool _image4Visible = false;
        private bool _image5Visible = false;
        private bool _image6Visible = false;
        private bool _image7Visible = false;
        private bool _image8Visible = false;
        private bool _image9Visible = false;
        private bool _image10Visible = false;
        private bool _image11Visible = false;
        private bool _image12Visible = false;
        private bool _image13Visible = false;
        private bool _image14Visible = false;
        private bool _image15Visible = false;
        private bool _image16Visible = false;
        private bool _image17Visible = false;
        private bool _image18Visible = false;
        private bool _image19Visible = false;
        private bool _image20Visible = false;



        //public bool Image0Visible {
        //    gett
        //    {
        //        return _image0Visible;
        //    }
        //    set
        //    {
        //        _image0Visible = value;
        //     //   RaisePropertyChanged(() => Image0Visible);

        //    }
        
        
        //}
        //public bool Image1Visible { 
        //    get
        //    {{
        //            return _image1Visible;;
        //    }
        
                   
        //    set
        //    {
        //        _image1Visible = value;
        //       // RaisePropertyChanged(() => Image1Visible);

        //    }
        
        //}







        public bool Image0HideVisible
        {
            get { return true; }
        }

        private void DoAnimation()
        {
          //  if (timer == null)
           //     timer = new Timer(OnTick, null, 500, 500);
        }


        private void OnTick(object args)
        {
             currImage = currImage + 1;
            currImage = 0;
            switch (currImage)
            {
                case 0:
                    Image20Visible = false;
                    Image0Visible = true;
                    break;
                case 1:
                    Image0Visible = false;
                    Image1Visible = true;
                    break;
                           
                case 2:
                    Image1Visible = false;
                    Image2Visible = true;
                    break;

                        
                case 3:
                    Image2Visible = false;
                    Image3Visible = true;
                    break;

                                    
                case 4:
                    Image3Visible = false;
                    Image4Visible = true;
                    break;

                                   
                case 5:
                    Image4Visible = true;
                    Image5Visible = false;
                    break;

                case 6:
                    Image5Visible = false;
                    Image6Visible = true;
                    break;

                case 7:
                    Image6Visible = false;
                    Image7Visible = true;
                    break;


                case 8:
                    Image7Visible = false;
                    Image8Visible = true;
                    break;


                case 9:
                    Image8Visible = false;
                    Image9Visible = true;
                    break;

                                    
                case 10:
                    Image9Visible = false;
                    Image10Visible = true;
                    break;

                case 11:
                    Image10Visible = false;
                    Image11Visible = true;
                    break;

                case 12:
                    Image11Visible = false;
                    Image12Visible = true;
                    break;


                case 13:
                    Image12Visible = false;
                    Image13Visible = true;
                    break;


                case 14:
                    Image13Visible = false;
                    Image14Visible = true;
                    break;


                case 15:
                    Image14Visible = true;
                    Image15Visible = false;
                    break;

                case 16:
                    Image15Visible = false;
                    Image16Visible = true;
                    break;

                case 17:
                    Image16Visible = false;
                    Image17Visible = true;
                    break;


                case 18:
                    Image17Visible = false;
                    Image18Visible = true;
                    break;


                case 19:
                    Image18Visible = false;
                    Image19Visible = true;
                    break;


                case 20:
                    Image19Visible = false;
                    Image20Visible = true;
                    currImage = 0;
                    break;




            }

            //if (currImage == 0)
            //{
            //    _image0Visible = true;
            //}


            //    TimeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));

            //if (timeLeft.TotalSeconds == 0)
            //    ExecuteStopCommand();
        }

    

        public AnimationLoaderViewModel(IFeedService feedService, IUserService userService, FeedModel feed)
        {
           // _feedService = feedService;
           // _userService = userService;
           // if (_currentUserId == null)
           //     _currentUserId = _userService.GetProfileId();
           // _feed = feed;

           // if (_feed.VideoSrcFeed + "" != "")
           // {
           //     var isVideo = true;
           // }

           // #region Traitement du (...) en cas de longue description dans un feed.

           // var hasTagsRegex = new Regex(@"(?<=#)\w+");
           // var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
           // TextHashTags = string.Join(" ", hashTags);
           // int descriptionLength = _feed.Description.Length;
           // int end;
           // if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
           // {
           //     FeedShortDescription = _feed.Description.Substring(0, end);
           // }
           // else
           // {
           //     FeedShortDescription = _feed.Description;
           // }

           // #endregion

           // if (_feed.VideoSrcFeed + "" != "")
           // {
           //     var isVideo = true;
           // }

           // IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
           // Update();


           // LoadHashTags();
           //// HashTags = HashTags;
        }

        #endregion

        #region Properties

        public bool Image0Visible
        {
            get
            {
                return _image0Visible;

            }
            set
            {
                
                _image0Visible = value;
                  RaisePropertyChanged(() => Image0Visible);

            }

        }



        public bool Image1Visible
        {
            get
            {
                return _image1Visible;
            }
            set
            {
                _image1Visible = value;
                RaisePropertyChanged(() => Image1Visible);
            }
        }

        public bool Image2Visible
        {
            get
            {
                return _image2Visible;
            }
            set
            {
                _image2Visible = value;
                RaisePropertyChanged(() => Image2Visible);
            }
        }



        public bool Image3Visible
        {
            get
            {
                return _image3Visible;
            }
            set
            {
                _image3Visible = value;
                RaisePropertyChanged(() => Image3Visible);
            }
        }



        public bool Image4Visible
        {
            get
            {
                return _image4Visible;
            }
            set
            {
                _image4Visible = value;
                RaisePropertyChanged(() => Image4Visible);
            }
        }



        public bool Image5Visible
        {
            get
            {
                return _image5Visible;
            }
            set
            {
                _image5Visible = value;
                RaisePropertyChanged(() => Image5Visible);
            }
        }

        public bool Image6Visible
        {
            get
            {
                return _image6Visible;
            }
            set
            {
                _image6Visible = value;
                RaisePropertyChanged(() => Image6Visible);
            }
        }

        public bool Image7Visible
        {
            get
            {
                return _image7Visible;
            }
            set
            {
                _image7Visible = value;
                RaisePropertyChanged(() => Image7Visible);
            }
        }


        public bool Image8Visible
        {
            get
            {
                return _image8Visible;
            }
            set
            {
                _image8Visible = value;
                RaisePropertyChanged(() => Image8Visible);
            }
        }



        public bool Image9Visible
        {
            get
            {
                return _image9Visible;
            }
            set
            {
                _image9Visible = value;
                RaisePropertyChanged(() => Image9Visible);
            }
        }




        public bool Image10Visible
        {
            get
            {
                return _image10Visible;
            }
            set
            {
                _image10Visible = value;
                RaisePropertyChanged(() => Image10Visible);
            }
        }




                
        public bool Image11Visible
        {
            get
            {
                return _image11Visible;
            }
            set
            {
                _image11Visible = value;
                RaisePropertyChanged(() => Image11Visible);
            }
        }

        public bool Image12Visible
        {
            get
            {
                return _image12Visible;
            }
            set
            {
                _image12Visible = value;
                RaisePropertyChanged(() => Image12Visible);
            }
        }



        public bool Image13Visible
        {
            get
            {
                return _image13Visible;
            }
            set
            {
                _image13Visible = value;
                RaisePropertyChanged(() => Image13Visible);
            }
        }



        public bool Image14Visible
        {
            get
            {
                return _image14Visible;
            }
            set
            {
                _image14Visible = value;
                RaisePropertyChanged(() => Image14Visible);
            }
        }



        public bool Image15Visible
        {
            get
            {
                return _image15Visible;
            }
            set
            {
                _image15Visible = value;
                RaisePropertyChanged(() => Image15Visible);
            }
        }

        public bool Image16Visible
        {
            get
            {
                return _image16Visible;
            }
            set
            {
                _image16Visible = value;
                RaisePropertyChanged(() => Image16Visible);
            }
        }

        public bool Image17Visible
        {
            get
            {
                return _image17Visible;
            }
            set
            {
                _image17Visible = value;
                RaisePropertyChanged(() => Image17Visible);
            }
        }


        public bool Image18Visible
        {
            get
            {
                return _image18Visible;
            }
            set
            {
                _image18Visible = value;
                RaisePropertyChanged(() => Image18Visible);
            }
        }



        public bool Image19Visible
        {
            get
            {
                return _image19Visible;
            }
            set
            {
                _image19Visible = value;
                RaisePropertyChanged(() => Image19Visible);
            }
        }




        public bool Image20Visible
        {
            get
            {
                return _image20Visible;
            }
            set
            {
                _image20Visible = value;
                RaisePropertyChanged(() => Image20Visible);
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


        // public ObservableCollection<Person> HashTags
        //{
        //    get
        //    {
        //        return _hashTags;
        //    }


        //    set 
        //    {
        //        _hashTags = value;


        //        RaisePropertyChanged(() => HashTags); 
        //    } 
        //}



        //public string HashTag0
        //{
        //    get
        //    {
        //        return _hashtag0;
        //    }


        //    set 
        //    {
        //        _hashtag0 = value;


        //        RaisePropertyChanged(() => HashTag0); 
        //    } 
        //}


        //public string HashTag1
        //{
        //    get
        //    {
        //        return _hashtag1;
        //    }


        //    set 
        //    {
        //        _hashtag1 = value;


        //        RaisePropertyChanged(() => HashTag1); 
        //    } 
        //}



        //public string HashTag2
        //{
        //    get
        //    {
        //        return _hashtag2;
        //    }

        //    set {
        //        _hashtag2 = value;


        //        RaisePropertyChanged(() => HashTag2);
        //    }

        //}


        //public string HashTag3
        //{
        //    get
        //    {
        //        return _hashtag3;
        //    }


        //    set
        //    {
        //        _hashtag3 = value;


        //        RaisePropertyChanged(() => HashTag3);
        //    }

        //}

        //        public string HashTag4
        //{
        //    get
        //    {
        //        return _hashtag4;
        //    }


        //    set
        //    {
        //        _hashtag4 = value;


        //        RaisePropertyChanged(() => HashTag4);
        //    }

        //}







        //public List<string> FeedTags
        //{

        //    get
        //    {
        //        return _feedTags;
        //        //var itemList = new List<string>();

        //        //itemList.Add("FunFacts");
        //        //itemList.Add("Breed");
        //        //return itemList;//new List() { "FunFacts", "Breed" };
        //    }
        //    set
        //    {
        //        _feedTags = value;
        //        RaisePropertyChanged(() => FeedTags);
        //    }

        //}





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










        //        private bool LoadHashTags()
        //        {

        //             var hasTagsRegex = new Regex(@"(?<=#)\w+");
        //            var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
        //            TextHashTags = string.Join(" ", hashTags);

        //            if (hashTags.Count() >= 1) { _hashtag0 = hashTags.ElementAt(0).ToString(); }
        //            if (hashTags.Count() >= 2) { _hashtag1 = hashTags.ElementAt(1).ToString(); }
        //            if (hashTags.Count() >= 3) { _hashtag2 = hashTags.ElementAt(2).ToString(); }
        //            if (hashTags.Count() >= 4) { _hashtag3 = hashTags.ElementAt(3).ToString(); }
        //             if (hashTags.Count() >= 5) { _hashtag4 = hashTags.ElementAt(4).ToString(); }





        //             var tempPersons = new ObservableCollection<Person>();

        //                        for (int i = 0; i < 3; i++)
        //            {
        //                var person = new Person { Name = "person " + i, Age = 10 + i };
        //                tempPersons.Add(person);
        //            }

        //            HashTags = tempPersons;


        ////           var tempHashTags = new ObservableCollection<KHashTag>();
        ////         //   LoadData();

        ////             var hashTag1 = new KHashTag { Text = "FunFacts", Id = "def", Age = 10 };
        ////            tempHashTags.Add(hashTag1);

        ////             var hashTag2 = new KHashTag { Text = "Cute", Id = "abc", Age = 11 };
        ////            tempHashTags.Add(hashTag2);

        ////            HashTags = tempHashTags;
        //////            RaisePropertyChanged(() => HashTags);

        //            return true;
        //        }

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

        //        public string FeedLikerNames
        //        {
        //            get
        //            {
        //                var value = this.Feed;
        //                string likeUser0 = string.Empty;
        //                string likeUser1 = string.Empty;
        //                var currentUser = Settings.CurrentUserProfile;

        //                if (value.UserHasLiked)
        //                {
        //                    likeUser0 = "You";
        //                    likeUser1 = value.Likes.FirstOrDefault(u => u.From.Id != currentUser?.Id)?.From?.DisplayUsername;
        //                }
        //                else
        //                {
        //                    if (value.NbLikes > 0)
        //                    {
        //                        likeUser0 = value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList()[0].From.DisplayUsername;
        //                    }

        //                    if (value.NbLikes > 1)

        //                        try
        //                        {
        //                            var list = value.Likes.Where(u => u.From.Id != currentUser?.Id).ToList();
        //                            if (list.Count() > 1)
        //                            {
        //                                likeUser1 = list[1].From.DisplayUsername;
        //                            }
        //                            else
        //                            {
        //                                return $"{value.NbLikes} buddies like this";
        //                            }
        //                        }
        //                        catch (Exception e)
        //                        {
        //                            var exp = e;
        //                        }
        //                }

        //#if DEBUG
        //                if (String.IsNullOrWhiteSpace(likeUser0))
        //                {
        //                    likeUser0 = "No Name";
        //                }
        //                if (String.IsNullOrWhiteSpace(likeUser1))
        //                {
        //                    likeUser1 = "No Name";
        //                }
        //#endif



        //                switch (value.NbLikes)
        //                {
        //                    case 0:
        //                        return "Be the first to like this";
        //                    case 1:
        //                        if (value.UserHasLiked)
        //                            return $"{likeUser0} like this";
        //                        else
        //                            return $"{likeUser0} likes this";
        //                    case 2:
        //                        if (string.IsNullOrWhiteSpace(likeUser1))
        //                            return $"{likeUser0} and 1 buddy like this";
        //                        else
        //                            return $"{likeUser0} and {likeUser1} like this";
        //                    default:
        //                        return $"{likeUser0} and {value.NbLikes - 1} buddies like this";
        //                }
        //            }

        //        }





        //public bool HasComments
        //{
        //    get {

        //        _hasComments = false;

        //        if (Comments != null)
        //        {
        //            if (Comments.Count > 0)
        //            {
        //                _hasComments = true;
        //            }
        //        }


        //        return _hasComments; 


        //    }

        //}


        //public bool NoComments
        //{
        //    get
        //    {

        //        _noComments = true;

        //        if (Comments != null)
        //        {

        //            if (Comments.Count > 0)
        //            {
        //                _noComments = false;
        //            }

        //        }


        //        return _noComments;

        //    }
        //    set
        //    {
        //        _noComments = value;
        //        RaisePropertyChanged(() => NoComments);
        //    }
        //}




        //public virtual bool YouLiked
        //{
        //    get
        //    {

        //        return Feed.UserHasLiked;

        //    }
        //}

        //public virtual bool YouNotLiked
        //{
        //    get
        //    {

        //        return !Feed.UserHasLiked;

        //    }
        //}









        //      public virtual bool IsLiked
        //      {
        //          get
        //          {
        //              bool liked = false;
        //              if (Feed.NbLikes > 0)
        //              {
        //                  liked = true;

        //              }
        //              return liked;

        //          }
        //      }

        //      public virtual bool IsNotLiked
        //      {
        //          get {

        //              return (Feed.NbLikes == 0);

        //              }
        //      }


        //      public FeedModel Feed
        //      {
        //          get { return _feed; }
        //          set
        //          {
        //              _feed = value;
        //              RaisePropertyChanged(() => Feed);
        //          }
        //      }


        //      public string ImageUrlLarge
        //      {
        //          get {

        //              string urlLargeImage = "no_profile_image.png";
        //              if (Feed.Media.KExpandedImages != null)
        //              {
        //                  if (Feed.Media.KExpandedImages.KLarge != null)
        //                  {
        //                      urlLargeImage = Feed.Media.KExpandedImages.KLarge.DownloadURL;;
        //                  }
        //              }
        //              return urlLargeImage; 
        //          }
        //      }

        //      public string ImageUrlMedium
        //      {
        //          get {

        //              string urlMediumImage = "no_profile_image.png";
        //              if (Feed.Media.KExpandedImages != null)
        //              {
        //                  if (Feed.Media.KExpandedImages.KLarge != null)
        //                  {
        //                      urlMediumImage = Feed.Media.KExpandedImages.KMedium.DownloadURL;
        //                  }


        //              }
        //              return urlMediumImage; 
        //          }
        //      }

        //      public string ImageUrlSmall
        //      {
        //          get
        //          {

        //              string urlSmallImage = "no_profile_image.png";
        //              if (Feed.Media.KExpandedImages != null)
        //              {
        //                  if (Feed.Media.KExpandedImages.KLarge != null)
        //                  {
        //                      urlSmallImage = Feed.Media.KExpandedImages.KSmall.DownloadURL;
        //                  }


        //              }
        //              return urlSmallImage;
        //          }
        //      }



        //      public string FeedItemTitleImageURL
        //      {
        //          get
        //          {
        //              string imageURL = Feed.ImageSrcProfile;

        //              if (imageURL == null)
        //              {
        //                  imageURL = "petpixiecirclelogo.png";
        //              }

        //              return imageURL;

        //          }
        //      }


        //      public string FeedItemTitle
        //      {
        //          get { return Feed.Title; }
        //      }



        //      public override int GetHashCode()
        //      {
        //          return base.GetHashCode();
        //          {

        //          }
        //      }


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


        ////Code Smell - connected to lack of Wrap Panel binding
        //public ICommand GoTagCommand => new SafeMvxCommand(GoTagCommandExecute);
        //public ICommand GoTag1Command => new SafeMvxCommand(GoTag1CommandExecute);
        //public ICommand GoTag2Command => new SafeMvxCommand(GoTag2CommandExecute);
        //public ICommand GoTag3Command => new SafeMvxCommand(GoTag3CommandExecute);
        //public ICommand GoTag4Command => new SafeMvxCommand(GoTag4CommandExecute);





        //public ICommand GoLikersCommand => new SafeMvxCommand(GoLikersCommandExecute);

        //public IMvxCommand LikeChangedCommand => new SafeMvxCommand(LikeChangedCommandExecute);

        //public IMvxCommand AddWoofCommand => new SafeMvxCommand(AddWoofCommandExecute);

        //public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);

        //public ICommand DetailsCommand => new SafeMvxCommand(Details);

        //private void GoLikersCommandExecute()
        //{
        //    if (Feed.NbLikes <= 0)
        //    {
        //        Feed.UserHasLiked = true;
        //        LikeChangedCommand.Execute();
        //    }
        //    else
        //        ShowViewModel<LikeListViewModel, LikeListParameter>(new LikeListParameter()
        //        {
        //            MediaId = Feed.PostId
        //        });
        //}


        //private void GoTagCommandExecute()
        //{
        //        ShowViewModel<DiscoverSearchViewModel, DiscoverSearchParameter>(new DiscoverSearchParameter()
        //        {
        //            SearchMode =  SearchMode.Tag,
        //            SearchEntry= HashTag0,
        //            TitlePage = HashTag0// TextHashTags
        //        });
        //}


        // private void GoTag1CommandExecute()
        //{
        //        ShowViewModel<DiscoverSearchViewModel, DiscoverSearchParameter>(new DiscoverSearchParameter()
        //        {
        //            SearchMode =  SearchMode.Tag,
        //            SearchEntry= HashTag1,
        //            TitlePage = HashTag1
        //        });
        //}




        // private void GoTag2CommandExecute()
        //{
        //    ShowViewModel<DiscoverSearchViewModel, DiscoverSearchParameter>(new DiscoverSearchParameter()
        //    {
        //        SearchMode = SearchMode.Tag,
        //        SearchEntry = HashTag2,
        //        TitlePage = HashTag2 
        //    });
        //}

        //  private void GoTag3CommandExecute()
        //{
        //    ShowViewModel<DiscoverSearchViewModel, DiscoverSearchParameter>(new DiscoverSearchParameter()
        //    {
        //        SearchMode = SearchMode.Tag,
        //        SearchEntry = HashTag3,
        //        TitlePage = HashTag3 
        //    });
        //}


        //  private void GoTag4CommandExecute()
        //{
        //    ShowViewModel<DiscoverSearchViewModel, DiscoverSearchParameter>(new DiscoverSearchParameter()
        //    {
        //        SearchMode = SearchMode.Tag,
        //        SearchEntry = HashTag4,
        //        TitlePage = HashTag4 
        //    });
        //}




        //private async void LikeChangedCommandExecute()
        //{
        //    if (Feed.UserHasLiked)
        //        Feed.NbLikes--;
        //    else
        //        Feed.NbLikes++;
        //    Feed.UserHasLiked = !Feed.UserHasLiked;

        //    FeedViewModel.SelectedItem = this;
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
        //    FeedViewModel.SelectedItem = this;
        //    ShowViewModel<CommentListViewModel>(new { PostId = Feed.PostId });
        //}

        //private void ShowProfileCommandExecute()
        //{
        //    if (Feed.Type != TypeMedia.Media) return;
        //    ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
        //    {
        //        ProfileId = Feed.ProfileId
        //    });
        //}





        #endregion

        #region Methods

        //public void Update()
        //{
        //    this.Comments =
        //        new ObservableCollection<CommentItemViewModel>(
        //            Feed.Comments.Select(kComment => new CommentItemViewModel(kComment, this)));

        //    //Feed.IsUserLike = Feed.Likes.Any(d => _userService.IsUserConnected(d.From.Id));
        //    //if (Feed != null)
        //    //{
        //    //    Feed.Likes = new ObservableCollection<KLike>(Feed.Likes.OrderBy(d => d.From.Id == _currentUserId.Result));
        //    //}


        //    Feed.UserHasLiked = Feed.Likes.Any(d => d.From.Id == _currentUserId);//  _userService.UserConnected());//d.From.Id));
        //                //if (Feed != null)
        //    {
        //        Feed.Likes = new ObservableCollection<KLike>(Feed.Likes.OrderBy(d => d.From.Id == _currentUserId));
        //    }
        //}

        public override void Start()
        {
        }

        //protected override void RealInit( )
        //{
        //    throw new NotImplementedException();
        //}

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

        //public void Details()
        //{
        //    FeedViewModel.SelectedItem = this;
        //    ShowViewModel<FeedItemViewModel, FeedItemParameters>(new FeedItemParameters {Feed = Feed});
        //}

        //     protected override void RealInit(FeedItemParameters parameter)
        //     {
        //         _feed = parameter.Feed;
        //         if (_feed == null)
        //         {
        //             Close(this);
        //             return;
        //         }
        //         //Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
        //        // Feed.VideoSrcFeed = "https://storage.googleapis.com/0ba3840452fb46ab9062c340c32a7eb5/198fb442-4db3-4418-b640-c915ae34bf33/f7e9fae5-6e10-4dc6-9ec7-3fa04fc0ca6d.mp4";
        //IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
        //         IsMedicalAlert = _feed.Type == TypeMedia.HealthAlert;
        //         var hasTagsRegex = new Regex(@"(?<=#)\w+");
        //         var hashTags = hasTagsRegex.Matches(_feed.Description).Cast<Match>().Select(match => $"#{match.Value}");
        //         TextHashTags = string.Join(" ", hashTags);

        //         if (hashTags.Count() >= 1) { _hashtag0 = hashTags.ElementAt(0).ToString();}
        //         if (hashTags.Count() >= 2) { _hashtag0 = hashTags.ElementAt(1).ToString(); }





        //         int descriptionLength = _feed.Description.Length;
        //         int end;
        //         if (descriptionLength > 130 && (end = _feed.Description.IndexOf(' ', 130)) != -1)
        //         {
        //             FeedShortDescription = _feed.Description.Substring(0, end);
        //         }
        //         else
        //         {
        //             FeedShortDescription = _feed.Description;
        //         }
        //         FeedDescriptionWithoutHashTags = _feed.Description;

        //         Update();

        //         LoadHashTags();
        //     }

        //    public override void Started()
        //    {
        //        base.Started();
        //        UpdateItemStats();
        //        /*
        //        if (FeedViewModel.SelectedItem != null)
        //        {
        //Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
        ////IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
        //            Feed.NbLikes = FeedViewModel.SelectedItem.Feed.NbLikes;
        //            Feed.Likes = FeedViewModel.SelectedItem.Feed.Likes;
        //            Feed.UserHasLiked = FeedViewModel.SelectedItem.Feed.UserHasLiked;
        //            IsRemoved = FeedViewModel.SelectedItem.IsRemoved;
        //            Feed = FeedViewModel.SelectedItem.Feed;

        //            Update();
        //            FeedViewModel.SelectedItem = null;
        //        }
        //        */
        //    }

        //private void UpdateItemStats()
        //{
        //     if (FeedViewModel.SelectedItem != null)
        //    {
        //        Feed.VideoSrcFeed = "http://www.fieldandrurallife.tv/videos/Benltey%20Mulsanne.mp4";
        //        //IsVideo = string.IsNullOrEmpty(Feed.VideoSrcFeed) ? false : true;
        //        Feed.NbLikes = FeedViewModel.SelectedItem.Feed.NbLikes;
        //        Feed.Likes = FeedViewModel.SelectedItem.Feed.Likes;
        //        Feed.UserHasLiked = FeedViewModel.SelectedItem.Feed.UserHasLiked;
        //        IsRemoved = FeedViewModel.SelectedItem.IsRemoved;
        //        Feed = FeedViewModel.SelectedItem.Feed;

        //        Update();
        //        FeedViewModel.SelectedItem = null;
        //    }
        //}

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

        public class FeedItemParameters
        {
            public FeedModel Feed { get; set; }
        }
    }
}