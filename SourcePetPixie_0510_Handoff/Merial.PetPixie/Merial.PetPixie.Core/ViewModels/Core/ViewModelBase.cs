using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Common.Logging;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
//using Plugin.Connectivity.Abstractions;
using Plugin.Media;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels.Core
{
    public class ViewModelBase : MvxViewModel
    {
        #region Static 

        public  bool CloseOnResume = false;

        #endregion

        #region Fields

        #region Services

        public IPopupService PopupService { get; private set; }

        protected INotificationService NotificationService { get; private set; }

        protected readonly IConnectivityService ConnectivityService;

        protected ILog Logger { get; }

        #endregion

        private int _isFetchingData;
        private ContentStatus _isThereContent;
        private bool _isFetchingError;
        private bool _isBusy;
        private int _numberOfNotifications;

        private bool _isMenuVisible = false;
        private ProfileModel _profile;


        #endregion

        #region Constructors

        public ViewModelBase()
        {
            PopupService = Mvx.Resolve<IPopupService>();
            ConnectivityService = Mvx.Resolve<IConnectivityService>();
            Logger = Mvx.Resolve<ILog>();
        }

        #endregion

        #region Properties



        public virtual ViewMessages Messages { get; set; }

        public bool IsFetchingData
        {
		    get { return _isFetchingData > 0; }
		    set
            {
			    if (value)
				    Interlocked.Increment(ref _isFetchingData);
			    else if (_isFetchingData > 0)
					Interlocked.Decrement(ref _isFetchingData);
				
			    RaisePropertyChanged(() => IsFetchingData);
		    }
	    }

        public bool IsFetchingError
        {
            get { return _isFetchingError; }
            set
            {
                _isFetchingError = value;
                RaisePropertyChanged(() => IsFetchingError);
            }
        }

        public ContentStatus IsThereContent
        {
            get { return _isThereContent; }
            set
            {
                _isThereContent = value;
                RaisePropertyChanged(() => IsThereContent);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public bool IsMenuVisibile
        {
            get { return _isMenuVisible; }
            set
            {
                _isMenuVisible = value;
                RaisePropertyChanged(() => IsMenuVisibile);
            }
        }

        public int NumberOfNotifications
        {
            get
            {
                return _numberOfNotifications;
            }
            set
            {
                _numberOfNotifications = value;
                this.RaisePropertyChanged();
            }
        }


        public ProfileModel Profile
        {
            get
            {

                if (_profile == null)
                {
                    var profile = Settings.CurrentUserProfile;

                    _profile = ProfileModel.CreateFrom(profile, _profile?.FileMetaData);
                }



                return _profile;
            }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
              
            }
        }

           
        public string ProfilePictureUrl
        {
            get
            {
                var imageSource = "no_profile_image.png";

                if (Profile.ImageSrc != null)
                {
                    imageSource = Profile.ImageSrc;
                }
             

              return imageSource;

            }
        }


        public KImage ProfilePicture
        {
            get

            {
                return Settings.CurrentUserProfile.ProfilePicture;
            }

        }


        public string UserFullName
        {
            get

            {
                return Settings.CurrentUserProfile.FullName;
            }

        }

                
        public string UserBio
        {
            get

            {
                return Settings.CurrentUserProfile.Biography;
            }

        }

        #endregion
        
        #region Commands





                public IMvxCommand AddAccessoryCommand => new SafeMvxCommand(AddAccessory);

        private void AddAccessory()
        {
            var currentImage = 1;// "hat_3.png";
        }



        public IMvxCommand BackCommand => new SafeMvxCommand(() => Close(this));
        
        public IMvxCommand RetryCommand => new SafeMvxCommand(RetryCommandExecute);

        public void RetryCommandExecute()
        {
            Started();
        }


        public IMvxCommand ShowFindFriendsCommand => new SafeMvxCommand(() => ShowViewModel<FindFriendsTabsViewModel>());
        public IMvxCommand HomeCommand => new SafeMvxCommand(() => ShowViewModel<FeedViewModel>());
       // public IMvxCommand ShowFeedCommand => new SafeMvxCommand(() => ShowViewModel<FeedViewModel>());
        public IMvxCommand ShowSearchCommand => new SafeMvxCommand(() => ShowViewModel<SearchViewModel>());
        public IMvxCommand ShowSettingsCommand => new SafeMvxCommand(() => ShowViewModel<SettingsViewModel>());
        public IMvxCommand ShowMyVetsCommand => new SafeMvxCommand(() => ShowViewModel<MyVetsViewModel>());
        public IMvxCommand ShowMyPackCommand => new SafeMvxCommand(() => ShowViewModel<MyPackViewModel, ProfileDetailParameter>(null));

        public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);

        public IMvxCommand ShowPictureFunCommand => new SafeMvxCommand(ShowPictureFunCommandExecute);

        public IMvxCommand ShowNotificationsCommand => new SafeMvxCommand(() => ShowViewModel<NotificationsFrameViewModel>());


        private void ShowProfileCommandExecute()
        {
            //  ShowViewModel<Option2ViewModel>();//new { PostId = "123" });
        }

        private void ShowPictureFunCommandExecute()
        {
            ShowViewModel<PictureFunViewModel>();
        }


        public IMvxCommand AcceptCropCommand => new SafeMvxCommand(() => ShowViewModel<PictureFunViewModel>());





        public IMvxCommand ShowHamburgerMenuCommand => new SafeMvxCommand(DoShowHamburgerMenuCommand);
        private void DoShowHamburgerMenuCommand()
        {
             MessagingCenter.Send(this, "ShowNavigation");
           // var abc = "you are here";
           IsMenuVisibile = !IsMenuVisibile;
        }




        public IMvxCommand ShowAboutCommand => new SafeMvxCommand(DoShowAboutCommand);
        private void DoShowAboutCommand()
        {
             MessagingCenter.Send(this, "ShowNavigation");
           // var abc = "you are here";
           IsMenuVisibile = !IsMenuVisibile;
        }


        public IMvxCommand ShowFeedCommand => new SafeMvxCommand(DoShowFeedCommand);
        private void DoShowFeedCommand()
        {
           //dsloading Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed");
            ShowViewModel<FeedViewModel>();
        }

          
        public IMvxCommand ShowDiscoverCommand => new SafeMvxCommand(DoShowDiscoverCommand);
        private void DoShowDiscoverCommand()
        {
           //   Mvx.Resolve<IUserDialogs>().ShowLoading("Discovering images");
            ShowViewModel<DiscoverViewModel, DiscoverParameter>(null);
            //ShowViewModel<DiscoverViewModel, DiscoverParameter>(new DiscoverParameter()
            //{
            //    SearchMode = SearchMode.Tag,
            //    SearchEntry = "",// tag.Text,
            //    TitlePage = "#" + "dog"// tag.Text
            //});
        }



        public IMvxCommand ShowRemindersCommand => new SafeMvxCommand(DoShowRemindersCommand);
        private void DoShowRemindersCommand()
        {
            ShowViewModel<PetReminderListViewModel, PetReminderListParameter>(new PetReminderListParameter
            {
                PetReminderModel = new PetReminderModel()// reminder.PetReminderModel
            });
        }



        public IMvxCommand TakeFunVideoCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    TakeVideo();
                });
            }
        }


        public async void TakeVideo()
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                PopupService.DisplayMessage("No Camera", $"Camera is unavailable");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                //AllowCropping = true,
                //Directory = "Sample",
                //Name = "test.jpg"
            });

            if (file == null)
                return;

            {
                App.FunPictureBytes = StreamHelper.ReadFully(file.GetStream());
                App.FunPictureUpdatedBytes = StreamHelper.ReadFully(file.GetStream());
                Settings.CurrentFunImageSource = file.Path;

                FileMetaData fm = new FileMetaData() { downloadURL = file.Path };

                ShowViewModel<ShareViewModel>(new { isVideo = true });
            }

        }

 

        public IMvxCommand SelectFunPictureCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    SelectPhoto();
                });
            }
        }

        public async void SelectPhoto()
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //PopupService.DisplayMessage("No Gallery", $"Photo Gallery is unavailable");
                return;
            }


            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions { });

            if (file != null)
            {
                App.FunPictureBytes = StreamHelper.ReadFully(file.GetStream());
                Settings.CurrentFunImageSource = file.Path;

                FileMetaData fm = new FileMetaData() { downloadURL = file.Path };

                 ShowViewModel<PictureFunViewModel>();
            }
        }


  

        public IMvxCommand TakeFunPictureCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    TakePhoto();
                });
            }
        }


        public async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                PopupService.DisplayMessage("No Camera", $"Camera is unavailable");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                AllowCropping = true,
                Directory = "PetPixie",
                Name = "PetPicture.jpg"
            });


            if (file != null)
            {
                App.FunPictureBytes = StreamHelper.ReadFully(file.GetStream());
                Settings.CurrentFunImageSource = file.Path;

                FileMetaData fm = new FileMetaData() { downloadURL = file.Path };

                ShowViewModel<PictureFunViewModel>();
            }
        }


        public IMvxCommand SelectFunVideoCommand
        {
            get
            {
                return new SafeMvxCommand(() =>
                {
                    SelectVideo(); ;
                });
            }
        }

        public async void SelectVideo()
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                PopupService.DisplayMessage("No Camera", $"Camera is unavailable");
                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();
            //{
            //    //PhotoSize= Plugin.Media.Abstractions.PhotoSize.Medium,
            //    //Directory = "Sample",
            //    //Name = "test.jpg"
            //});

            if (file != null)
            {
                App.FunPictureBytes = StreamHelper.ReadFully(file.GetStream());
                App.FunPictureUpdatedBytes = StreamHelper.ReadFully(file.GetStream());

                Settings.CurrentFunImageSource = file.Path;

                FileMetaData fm = new FileMetaData() { downloadURL = file.Path };

               
                ShowViewModel<ShareViewModel>(new { isVideo = true });
            }
        }
    


    #endregion

 

    #region Methods

    protected virtual Task<bool> LoadDataAsync()
        {
            if (PopupService == null) PopupService = Mvx.Resolve<IPopupService>();
            if (NotificationService == null)
            {
                NotificationService = Mvx.Resolve<INotificationService>();
                NotificationService.NewNotificationArrived += (sender, args) => this.LoadNotifications();
            }

            this.LoadNotifications();

            return Task.FromResult(true);
	    }

	    protected virtual void OnLoadDataError(Exception exception)
        {
		    Debug.WriteLine("LoadDataAsync Error:" + exception);
	    }

	    protected bool ShowViewModel<TViewModel, TInit>(TInit parameter) where TViewModel : ViewModelBase<TInit>
        {
            string text = "";
            try
            {
                 text = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(parameter);
            }
            catch (Exception exc)
            {
                var error = exc.Message;
            }
            return base.ShowViewModel<TViewModel>(new Dictionary<string, string> { { "parameter", text } });
        }

        protected bool ShowValidationViewModel<TValidationViewModel, TInit>(TInit parameter) where TValidationViewModel : ValidationFormViewModelBase<TInit>
        {
            var text = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(parameter);
            return base.ShowViewModel<TValidationViewModel>(new Dictionary<string, string> { { "parameter", text } });
        }
        
        protected bool ShowViewModel<TViewModel, TInit>(TInit parameter, MvxBundle mvxBundle) where TViewModel : ViewModelBase<TInit>
        {
			var text = Mvx.Resolve<IMvxJsonConverter>().SerializeObject(parameter);
			return base.ShowViewModel<TViewModel>(new Dictionary<string, string> { { "parameter", text } }, mvxBundle);
		}

        #region OnStart OnPause

        public virtual async void Started()
        {
            IsFetchingError = false;
         //ds   ConnectivityService.AddConnectivityChangedListener(CurrentOnConnectivityChanged);
            
            if (ConnectivityService.IsConnected() == false)
            {

         //ds       ShowViewModel<NoConnectivityViewModel>();
            }
            IsFetchingData = true;

            try
            {
                await LoadDataAsync();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                OnLoadDataError(e);
                IsFetchingError = true;

            }
            finally
            {
                IsFetchingData = false;
            }
		}

		public virtual void Paused()
        {
          //ds  ConnectivityService.RemoveConnectivityChangedListener( CurrentOnConnectivityChanged);
        }

        public virtual void Destroyed()
        {
        }

        #endregion

        //ds
        //private void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs)
        //{
        //    if (connectivityChangedEventArgs.IsConnected == false)
        //        ShowViewModel<NoConnectivityViewModel>();
        //}
        
        public async Task EnsureIsConnected(Func<Task> action)
        {
            if (ConnectivityService.IsConnected())
                await action();
            else
                PopupService.DisplayMessageAsToast(Messages.MessageRequiredConnectivity);
        }

        protected async void EnsureIsBusy(Func<Task> action, bool flagHasFetchingData = true)
        {
            if (IsBusy) return;

            try
            {
                if (flagHasFetchingData)
                    IsFetchingData = true;
                IsBusy = true;
                await action();
            }
            finally
            {
                if (flagHasFetchingData)
                    IsFetchingData = false;
                IsBusy = false;
            }
        }

        private void LoadNotifications()
        {
            this.NumberOfNotifications = this.NotificationService.NbrNewNotifications;
        }

        #endregion
    }

    public abstract class ViewModelBase<TInit> : ViewModelBase
    {
        public void Init(string parameter)
        {
            var deserialized = Mvx.Resolve<IMvxJsonConverter>().DeserializeObject<TInit>(parameter);
            RealInit(deserialized);
        }

        protected abstract void RealInit(TInit parameter);
    }

    public class ViewMessages
    {
        public static ViewMessages CreateFrom(string noDataMessage)
        {
            return new ViewMessages
            {
                NoDataMessage = noDataMessage
            };
        }

        public  string MessageRequiredConnectivity = "This functionnality required connectivity";
        public  string NoConnectionErrorMessage = "An error occured , please check your network connection.";
        public  string FetchingErrorMessage = "An error occured, when fetching data.";
        public  string NoDataMessage = "No data";
    }
}