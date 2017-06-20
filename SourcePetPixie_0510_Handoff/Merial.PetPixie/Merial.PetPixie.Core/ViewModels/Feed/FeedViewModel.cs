using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Plugins.PictureChooser;
using MvvmCross.Platform;
using System.IO;
using KinveyXamarin;
using Acr.UserDialogs;
using System;
using Plugin.Media;
using Splat;
using Xamarin.Forms;
//using Merial.PetPixie.Core.ViewModels.Merial.PetPixie.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FeedViewModel : ViewModelBase
    {
        #region Fieds

        private readonly IFeedService _feedService;
        private readonly IUserService _userService;
        private readonly IHashTagService _HashService;
        private ObservableCollection<FeedItemViewModel> _items;

        private bool _isRefreshing;
        private bool _isLoading;
                //   private bool _isMenuVisible = false;
       

        private bool _isLiked = true;
        private bool _isNotLiked = false;



        private readonly IBitmapService _bitmapService;
        private readonly IByteService _byteService;
        private readonly IVideoService _videoService;
        private readonly ICacheService _cacheService;
        private readonly IMediaService _mediaService;
        private readonly INotificationService _notificationService;
        private readonly IReminderService _reminderService;




        private IMvxPictureChooserTask _pictureChooserTask;
        private ObservableCollection<string> _pictureChoiceType;
        private ObservableCollection<string> _mediaChoiceType;
        private string _pictureChoiceTypeSelected;
        private string _mediaChoiceTypeSelected;
        private byte[] _pictureBytes;
        private byte[] _vBytes;
        private bool _showTour;

        #endregion


        #region Constructors

        public FeedViewModel(IFeedService feedService, IUserService userService, IBitmapService bitmapService)
        {
            _feedService = feedService;
            _userService = userService;
            _bitmapService = bitmapService;
            _items = new ObservableCollection<FeedItemViewModel>();
           
        }

        #endregion


        #region Properties

        public ObservableCollection<FeedItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public bool ShowTour
                {
                        get
                        {
                                return Settings.ShowGetStarted;
                        }

                        set
                        {
                                Settings.ShowGetStarted = value;
                                RaisePropertyChanged(() => ShowTour);
                        }

                }

        public static FeedItemViewModel SelectedItem
        {
            get; set;

        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == true)
                {
                    _isLoading = value;
                    RaisePropertyChanged(() => IsLoading);

                    try
                    {
                        InvokeOnMainThread(() =>
                        {
                          //dsloading  Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed images");
                        });
                    }
                    catch (Exception exc)
                    {
                        string except = exc.Message;
                    }

                }
                else
                {
                    // Mvx.Resolve<IUserDialogs>().HideLoading();
                    MessagingCenter.Send(this, "StopLoading");
                }



            }
        }



        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        #endregion

        #region Commands



        public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);

        public IMvxCommand ShowPictureFunCommand => new SafeMvxCommand(ShowPictureFunCommandExecute);

        public IMvxCommand ShowNotificationsCommand => new SafeMvxCommand(ShowNotificationsCommandExecute);


        private void ShowProfileCommandExecute()
        {
          //  ShowViewModel<Option2ViewModel>();//new { PostId = "123" });
        }

        private void ShowPictureFunCommandExecute()
        {
             ShowViewModel<PictureFunViewModel>();
        }
                
        private void ShowNotificationsCommandExecute()
        {
            ShowViewModel<NotificationsFrameViewModel>();
        }




        public IMvxCommand ShowFindFriendsViewCommand
		{
			get
			{
				return new SafeMvxCommand(() => {
					ShowViewModel<FindFriendsTabsViewModel>();
				});
			}
		}

                
        public IMvxCommand ShowMainCommand => new SafeMvxCommand(ShowMainCommandExecute);
        private void ShowMainCommandExecute()
        {
          //  ShowViewModel<Option2ViewModel>();//new { PostId = "123" });

            // We demand to clear the Navigation stack as we are changing the section
            var presentationBundle = new MvxBundle(new Dictionary<string, string> { { "NavigationMode", "ClearStack" } });

            // Show the ViewModel in the Detail NavigationPage
            ShowViewModel(typeof(RootMainViewModel), presentationBundle: presentationBundle);
        }



		public ICommand LoadCommand => new MvxCommand(LoadData);

        public ICommand LoadMoreCommand => new MvxCommand(LoadMoreData);

        public ICommand ReloadCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    IsRefreshing = true;

                    ReloadData();

                    IsRefreshing = false;
                });
            }
        }

        public virtual ICommand ItemSelected
        {
            get
            {
                return new SafeMvxCommand<FeedItemViewModel>(item => { /*SelectedItem = item; */});
            }
        }

        #endregion

       





        #region Public Methods

        public override void Start()
        {
            base.Start();


            LoadData();
        }

        public override void Started()
        {
            base.Started();
           

            if (SelectedItem != null)
            {
                var item = Items.FirstOrDefault(f => f.Feed.PostId == SelectedItem.Feed.PostId);

                if (SelectedItem.IsRemoved && item != null)
                {
                    Items.Remove(item);
                }
                else if (item != null)
                {
                    Items[Items.IndexOf(item)].Feed.NbLikes = SelectedItem.Feed.NbLikes;
                    Items[Items.IndexOf(item)].Feed.Likes = SelectedItem.Feed.Likes;
                    Items[Items.IndexOf(item)].Feed.UserHasLiked = SelectedItem.Feed.UserHasLiked;
                    Items[Items.IndexOf(item)].Feed.CommentsCount = SelectedItem.Feed.CommentsCount;
                    Items[Items.IndexOf(item)].Feed = SelectedItem.Feed;
                    Items[Items.IndexOf(item)].Update();


                }
                SelectedItem = null;

            }
        }

        #endregion


        #region Methods



                           
    


        public async void LoadData()
        {
           //    Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed");
            if (IsLoading == true)
                return;


               
            var list = await _feedService.GetAllAsync();
            IsLoading = true;
            if (list != null && list.Any())
            {
                
                Items = new ObservableCollection<FeedItemViewModel>(
                    list.Select(feed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(feed))).ToList());

            }

            IsLoading = false;

        }

        public virtual async void LoadMoreData()
        {
            if (IsLoading == true)
                return;

            IsLoading = true;
           
			int skip = Items.Count;
            var list = await _feedService.GetAllAsync(skip);

            for (int i = 0; i < list.Count - 1; i++)
            {
                Items.Add(new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(list[i])));
            }
			IsLoading = false;
		}

        public void ReloadData()
        {
            LoadData();
        }

        #endregion

        #region Commands

                
        public IMvxCommand EditProfileCommand => new SafeMvxCommand(OnEditProfile);
    
         protected void OnEditProfile()
        {
            
            ShowViewModel<ProfileEditorViewModel>();
        }
        #endregion





        #region NOT USED

        //public IMvxCommand ShowGaleryCommand
        //{
        //    get
        //    {
        //              return new SafeMvxCommand(() =>
        //        {
        //            _pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
        //            _pictureChooserTask.ChoosePictureFromLibrary(800, 100, OnPictureChosen, () => { });
        //        });
        //    }
        //}

             
        //private void OnPictureChosen(Stream pictureStream)
        //{
        //    Mvx.Resolve<IImageService>().CropPicture(pictureStream, PictureAvailableAfterCrop, () => { }, true);

        //}



        //private async void CropPicture(FileMetaData fileMetaData)
        //{
        //  //  var bitmap = _bitmapService.GetBitmap(fileMetaData.downloadURL);
        //    ShowViewModel<CropperViewModel>();
        //}



        //private async void PictureAvailableAfterCrop(FileMetaData fileMetaData)
        //{
        //    var bitmap = _bitmapService.GetBitmap(fileMetaData.downloadURL);
        //    ShowPictureFunCommand.Execute(bitmap);
        //}

        #endregion






    }
}