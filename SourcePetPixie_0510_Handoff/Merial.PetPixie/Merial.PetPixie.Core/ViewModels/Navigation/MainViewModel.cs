using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Models.PushModels;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
//using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using Plugin.Media;
using Plugin.Permissions;
//using Plugin.Permissions.Abstractions;
using Splat;


namespace Merial.PetPixie.Core.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		#region Fields

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

		public MainViewModel(
			IMvxPictureChooserTask pictureChooserTask,
			IBitmapService bitmapService,
			IByteService byteService,
			IVideoService videoService,
			ICacheService cacheService,
			IMediaService mediaService,
			INotificationService notificationService,
			IReminderService reminderService)
		{
			_pictureChooserTask = pictureChooserTask;
			_bitmapService = bitmapService;
			_byteService = byteService;
			_cacheService = cacheService;
			_mediaService = mediaService;
			_videoService = videoService;
			_notificationService = notificationService;
			_reminderService = reminderService;
			PictureChoiceType = new ObservableCollection<string>() {
				Constants.Pictures.TakeAPicture,
				Constants.Video.RecordVideo,
				"Select from gallery"
			};
			MediaChoiceType = new ObservableCollection<string>() {
				Constants.Pictures.SelectAPicture,
				Constants.Video.SelectAVideo
			};

			Mvx.Resolve<IBreedService>();
			PictureChoiceTypeSelected = PictureChoiceType.FirstOrDefault();

		}

		#endregion

		#region Properties

		public FeedViewModel Feed { get; private set; }

		public string PictureChoiceTypeSelected
		{
			get { return _pictureChoiceTypeSelected; }
			set
			{
				_pictureChoiceTypeSelected = value;
				RaisePropertyChanged(() => PictureChoiceTypeSelected);
			}
		}

		public ObservableCollection<string> PictureChoiceType
		{
			get { return _pictureChoiceType; }
			set
			{
				_pictureChoiceType = value;
				RaisePropertyChanged(() => PictureChoiceType);
			}
		}

		public string MediaChoiceTypeSelected
		{
			get { return _mediaChoiceTypeSelected; }
			set
			{
				_mediaChoiceTypeSelected = value;
				RaisePropertyChanged(() => MediaChoiceTypeSelected);
			}
		}

		public ObservableCollection<string> MediaChoiceType
		{
			get { return _mediaChoiceType; }
			set
			{
				_mediaChoiceType = value;
				RaisePropertyChanged(() => MediaChoiceType);
			}
		}

		public bool ShowTour
		{
			get { return _showTour; }
			set
			{
				_showTour = value;
				RaisePropertyChanged(() => ShowTour);
			}
		}

		public byte[] PictureBytes
		{
			get { return _pictureBytes; }
			set
			{
				_pictureBytes = value;
				RaisePropertyChanged(() => PictureBytes);
			}
		}

		#endregion

		#region Public Methods

		public void ShowMenu()
		{
			ShowViewModel<MenuViewModel>();
			ShowViewModel<NotificationsFrameViewModel>();
			ShowViewModel<FeedViewModel>();

			this.LoadFirstPageAsync();
		}

		public void LoadData()
		{
			this.LoadDataAsync();
		}

		public override void Started()
		{
			base.Started();


		}

		public override void Paused()
		{
			base.Paused();
		}

		#endregion

		#region Methods

		public void Init(bool showTour)
		{
			ShowTour = showTour;
		}

		private void OnPictureChosen(Stream pictureStream)
		{
			Mvx.Resolve<IImageService>().CropPicture(pictureStream, PictureAvailableAfterCrop, () => { }, true);

		}

		private async void PictureAvailableAfterCrop(FileMetaData fileMetaData)
		{
			var bitmap = _bitmapService.GetBitmap(fileMetaData.downloadURL);
			ShowPictureFunCommand.Execute(bitmap);
		}

		#region Commands

		public IMvxCommand ShowCameraCommand
		{
			get
			{
				return new SafeMvxCommand(() =>
				{
					_pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
					_pictureChooserTask.TakePicture(800, 100, OnPictureChosen, () => { });
				});
			}
		}

		public IMvxCommand RecordVideo
		{
			get
			{
				return new SafeMvxCommandAsync(async () =>
						{
							if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
							{
								PopupService.DisplayMessage("No camera available", "Error!");
								return;
							}
							var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
							{
								Directory = Constants.Video.FolderName,
								Quality = Plugin.Media.Abstractions.VideoQuality.Medium,
								DesiredLength = TimeSpan.FromSeconds(30),
								SaveToAlbum = true
							});
							
							if (file != null)
							{
								IsFetchingData = true;
								var x = await _videoService.GetPath(file.Path);
								IsFetchingData = false;
								_videoService.SetPath(x);
								//PopupService.DisplayMessage(x, "");

								var byteS = _byteService.GetByte(_videoService.GetPath());
								//var byteS = _streamService.GetByte(x);
								_byteService.SetByte(byteS);
								//PopupService.DisplayMessage(file.Path, "");

								var bitmap = _bitmapService.GetBitmap(file.Path);

								_bitmapService.SetBitmap(bitmap);
								ShowViewModel<ShareViewModel>(new { isVideo = true });
								file.Dispose();
							}
							else {
								return;
							}
						});
			}
		}


		public IMvxCommand VideoGallery
		{
			get
			{
				return new SafeMvxCommandAsync(async () =>
						{
							var file = await CrossMedia.Current.PickVideoAsync();

							if (file != null)
							{
								IsFetchingData = true;
								var x = await _videoService.GetPath(file.Path);
								IsFetchingData = false;
								_videoService.SetPath(x);
								PopupService.DisplayMessage(x, "");
								
								var byteS = _byteService.GetByte(_videoService.GetPath());
								_byteService.SetByte(byteS);
								PopupService.DisplayMessage(file.Path, "");
								 
								var bitmap = _bitmapService.GetBitmap(file.Path);
								
								_bitmapService.SetBitmap(bitmap);
								ShowViewModel<ShareViewModel>(new { isVideo = true});
								file.Dispose();
							}
							else {
								return;
							}

						});
			}
		}



		public IMvxCommand ShowPictureFunCommand
		{
			get
			{
				return new SafeMvxCommand<IBitmap>(bitmap =>
				{
					_bitmapService.SetBitmap(bitmap);
					ShowViewModel<PictureFunViewModel>();
					//ShowViewModel<ShareViewModel>();
				});
			}
		}

		public IMvxCommand ShowGaleryCommand
		{
			get
			{
				return new SafeMvxCommand(() =>
				{
					_pictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
					_pictureChooserTask.ChoosePictureFromLibrary(800, 100, OnPictureChosen, () => { });
				});
			}
		}
		public IMvxCommand CloseTourComman => new SafeMvxCommand(() => ShowTour = false);

		#endregion

		public async void LoadFirstPageAsync(bool onlyFromCachIfExists = false)
		{
			var pushNotificationModel = await this._cacheService.Get<PushNotificationModel>(Config.CachePersitenceConfig.SurveyId);

			if (pushNotificationModel != null)
				RedirectToCorrespondingNotificationScreen(pushNotificationModel);
		}

		private async void RedirectToCorrespondingNotificationScreen(PushNotificationModel notification)
		{
			try
			{
				await this._cacheService.Delete(Config.CachePersitenceConfig.SurveyId);
				this._notificationService.SetNewAsReadFromId(notification.NotificationId);

				switch (notification.NotificationType)
				{
					case NotificationType.MentionsOfYou:
					case NotificationType.NewHealthAlert:
					case NotificationType.Likes:
					case NotificationType.Comments:
						await ShowPostForNotification(notification);
						break;
					case NotificationType.FollowsYou:
					case NotificationType.NewFriendsJoinPetAndPixie:
						this.ShowUserForNotification(notification);
						break;
					case NotificationType.Reminder:
						this.ShowReminderForNotification(notification);
						break;
				}
			}
			catch (Exception e)
			{
				this.Logger.Info($"An error occured while openning the notification with id = {notification?.Id ?? "NO ID"}");
				this.Logger.Error(e);
				this.PopupService.DisplayMessage("Impossible to open the detail of this notification. Please try again", "Error");
			}
		}

		private void ShowUserForNotification(PushNotificationModel notification)
		{
			ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
			{
				ProfileId = notification.ProfileId
			});
		}

		private async void ShowReminderForNotification(PushNotificationModel notification)
		{
			var reminder = await this._reminderService.GetById(notification.ReminderId);

			if (reminder == null) return;

			ShowViewModel<PetReminderListViewModel, PetReminderListParameter>(new PetReminderListParameter
			{
				PetReminderModel = reminder.PetReminderModel
			});
		}

		private async Task ShowPostForNotification(PushNotificationModel notification)
		{
			var media = await this._mediaService.GetMediaById(notification.MediaId);
			var kfeed = new KFeed
			{
				KMedia = media,
				Type = media.Type,
				UserId = media.UserId,
				PetIds = media.PetIds?.ToArray()
			};

			this.ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = FeedModel.CreateFrom(kfeed) });
		}
		#endregion

	}

}