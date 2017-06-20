using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;
using Splat;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{



    public enum ShareState
    {
        NotShared,
        UploadingInitially,
        InitialUploadDone,
        ShouldShareAfterInitialUpload,
        SharingComplete,
        SharingCancelled
    }

    public enum LocalSaveState
    {
        NotSaved,
        Saving,
        Saved
    }

    public class ShareViewModel : ValidationFormViewModelBase
    {
        #region Fields

        private readonly IBitmapService _bitmapService;
		private readonly IByteService _byteService;
        private readonly IPetService _petService;
        private readonly IMediaService _mediaService;
        private readonly IUploadService _uploadService;

        private ObservableCollection<SelectablePetModel> _pets;
        private bool _shareOnFacebook;
        private bool _shareOnTwitter;
        private bool _silently;
        private string _description = string.Empty;
        private byte[] _image;
        private byte[] _pictureBytes;
		private byte[] _video;
		private bool _isVideo;

        private ShareState shareState = ShareState.NotShared;
        private FileMetaData succesfullFileUpload;
        private bool _isUploading;
        private bool _sharingOnFacebook;
        private bool _sharingOnTwitters;

        #endregion

        #region Constructors

        //public ShareViewModel()
        //{
            
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="petService"></param>
        /// <param name="mediaService"></param>
        /// <param name="bitmapService"></param>
        /// <param name="uploadService"></param>
        /// <param name="initialShareState"></param>
        public ShareViewModel(IPetService petService,
            IMediaService mediaService, 
            IBitmapService bitmapService,
		    IByteService byteService,
            IUploadService uploadService )
        {
            _petService = petService;
            _mediaService = mediaService;
			_byteService = byteService;
            _bitmapService = bitmapService;
            _uploadService = uploadService;

            this.Image = App.FunPictureUpdatedBytes;
            this.Init(_isVideo);
        }   




        public async Task<byte[]> Download(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] fileArray = await client.GetByteArrayAsync(url);
                return fileArray;
            }

        }





        #endregion

        #region Events

        public event EventHandler RequestFacebookPublishPermissions;
        public event EventHandler RequestTwitterPublishPermissions;
               
        public event EventHandler ShareOnFacebookEventHandler;
        public event EventHandler ShareOnTwitterEventHandler;

        public event EventHandler ShareRequested;

        #endregion

        #region Properties 

        public ShareState ShareState
        {
            get { return shareState; }
        }

            
        public string FunImageSource
        {
            get { return Settings.CurrentFunImageSource; }
        }

        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }

		public bool IsVideo
		{
			get { return _isVideo; }
		}

        public ObservableCollection<SelectablePetModel> Pets
        {
            get { return _pets; }
            set
            {
                _pets = value;
                RaisePropertyChanged(() => Pets);
            }
        }

        public bool ShareOnFacebook
        {
            get { return _shareOnFacebook; }
            set
            {
                if (_shareOnFacebook == value) return;
                _shareOnFacebook = value;
                RaisePropertyChanged(() => ShareOnFacebook);

                if (_shareOnFacebook)
                    this.RequestFacebookPublishPermissions?.BeginInvoke(this, EventArgs.Empty, null, null);
            }
        }

        public bool SharingOnFacebook
        {
            get
            {
                return _sharingOnFacebook;
            }
            set
            {
                _sharingOnFacebook = value;
                this.IsUploading = SharingOnFacebook || SharingOnTwitters;
            }
        }

        public bool ShareOnTwitter
        {
            get { return _shareOnTwitter; }
            set
            {
                if (_shareOnTwitter == value) return;
                _shareOnTwitter = value;
                RaisePropertyChanged(() => ShareOnTwitter);

                if (_shareOnTwitter)
                    this.RequestTwitterPublishPermissions?.BeginInvoke(this, EventArgs.Empty, null, null);
            }
        }

        public bool SharingOnTwitters
        {
            get { return _sharingOnTwitters; }
            set
            {
                _sharingOnTwitters = value;
                this.IsUploading = SharingOnFacebook || SharingOnTwitters;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public KMedia PostedMedia { get; set; }

        public bool IsUploading
        {
            get
            {
                return _isUploading;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this._isUploading, value);
            }
        }

        public LocalSaveState SaveState { get; set; }

        #endregion

        #region Commands

        public IMvxCommand ShareImageCommand => new SafeMvxCommand(OnShareImageCommand);

        private void OnShareImageCommand()
        {
            if (!Validate())
            {
                return;
            }

            this.Share(false, true);
            
            if (this.SaveState == LocalSaveState.NotSaved)
                this.ShareRequested?.BeginInvoke(this, EventArgs.Empty, null, null);
        }

        #endregion

        #region LoadData

        protected override async Task<bool> LoadDataAsync()
        {
            if (Image == null)
            {
                var bitmap = _bitmapService.GetBitmap();
                using (var stream = new MemoryStream())
                {
                    await bitmap.Save(CompressedBitmapFormat.Png, 0, stream);
                    Image = stream.ToArray();
                }
            }

            // Begin upload image silently, don't show error messages
            if (this.shareState == ShareState.NotShared)
                this.Share(true);

            return await LoadData();
        }

        private async Task<bool> LoadData()
        {
            if (Pets == null)
                Pets = new ObservableCollection<SelectablePetModel>((await _petService.GetAllAsync(Settings.CurrentUserProfile))
                    .Select(kp => new SelectablePetModel(kp)));
            return true;
        }

        #endregion

        #region Share Methods

        private void Share(bool silently, bool buttonTapped = false)
        {
            Mvx.Resolve<IUserDialogs>().ShowLoading("Sharing your image..");
            this._silently = silently;
            this.IsUploading = !silently;

            switch (this.shareState)
            {
                case ShareState.NotShared:
                    if (this.succesfullFileUpload == null)
                    {
                        this.shareState = ShareState.UploadingInitially;
                        if (_isVideo)
                        {
                            //this.UploadImage(_byteService.GetByte(), buttonTapped);
                              this.UploadImage(App.FunPictureUpdatedBytes, buttonTapped);
                        }
                        else

                        {
                            Mvx.Resolve<IUserDialogs>().ShowLoading("Uploading your image..");

                            //ds this.UploadImage(this.Image, buttonTapped);

                            this.UploadImage(App.FunPictureUpdatedBytes, buttonTapped);
                            Mvx.Resolve<IUserDialogs>().HideLoading();
                        }

                    }
                    else 
                    {
                        this.shareState = ShareState.NotShared; // State was NotShared but image is uploaded
                        this.Share(silently);
                    }
                    break;
                case ShareState.InitialUploadDone:
                    this.ShareMedia(this.succesfullFileUpload);
                    break;
                case ShareState.UploadingInitially:
                    this.shareState = ShareState.ShouldShareAfterInitialUpload;
                    break;
                case ShareState.SharingComplete:
                    break;
                case ShareState.SharingCancelled:
                    // Wants to try again
                    this.shareState = ShareState.NotShared;
                    this.Share(false, buttonTapped);
                    break;
            }
        }

        private async void UploadImage(byte[] fileBytes, bool buttonTapped)
        {
            try
            {
				var fileMetadata=(IsVideo)?await _uploadService.UploadVideo(fileBytes):await _uploadService.UploadImage(fileBytes) ;
                this.succesfullFileUpload = fileMetadata;
                
                // If 'share' button typed while uploading this file
                if (this.shareState == ShareState.ShouldShareAfterInitialUpload || buttonTapped)
                {
                    this.shareState = ShareState.InitialUploadDone;
                    this.Share(false);
                }
                else
                {
                    this.shareState = ShareState.InitialUploadDone;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                this.shareState = ShareState.NotShared;
                this.IsUploading = false;

                if (!this._silently)
                    PopupService.DisplayMessage("An error occured while uploading your image. Please try again","Error");
            }
        }

        private async void ShareMedia(FileMetaData fileMetaData)
        {
            try
            {
				KMedia postedMedia;
				if (_isVideo)
				{
					postedMedia = await _mediaService.ShareVideo(
					Description,
					Pets.Where(p => p.IsSelected).Select(p => p.Id),
					fileMetaData);
				}
				else
				{
					postedMedia = await _mediaService.SharePicture(
					Description,
					Pets.Where(p => p.IsSelected).Select(p => p.Id),
					fileMetaData);
				}
				
                this.PostedMedia = await _mediaService.GetMediaById(postedMedia.Id);

                if (this.shareState == ShareState.SharingCancelled) return;

                this.shareState = ShareState.SharingComplete;

                _bitmapService.Reset();

                SuccessFullPostedToPetPlus();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                this.shareState = ShareState.InitialUploadDone;
                this.IsUploading = false;
                Mvx.Resolve<IUserDialogs>().HideLoading();
                PopupService.DisplayMessage("An error occured. Please try again.","Error");
            }

        }
    
        private void SuccessFullPostedToPetPlus()
        {
            this.ShareOnSocialAccountsIfAllowed();

            if (!SharingOnFacebook && !SharingOnTwitters)
            {
                this.IsUploading = false;
                this.CloseShare();
            }
        }

        private void ShareOnSocialAccountsIfAllowed()
        {
            if (this.ShareOnFacebook) this.ShareOnFacebookExecute();
            if (this.ShareOnTwitter) this.ShareOnTwitterExecute();
        }

        private void ShareOnFacebookExecute()
        {
            this.SharingOnFacebook = true;
            this.ShareOnFacebookEventHandler?.BeginInvoke(this, EventArgs.Empty, null, null);
        }

        private void ShareOnTwitterExecute()
        {
            this.SharingOnTwitters = true;
            this.ShareOnTwitterEventHandler?.BeginInvoke(this, EventArgs.Empty, null, null);
        }

        #endregion

        public void CloseShare()
        {
            var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.ReloadScreen } });
            ShowViewModel<FeedViewModel>(presentationBundle: presentationBundle);
        }

        public override void AddValidationFields(ValidationHelper helper)
        {
            helper.AddRule(() => Description, () => RuleResult.Assert(Description.Length <= 140, "Maximum 140 charaters"));
        }

        public class SelectablePetModel : EntityBase
        {
            private bool _isSelected;

            public SelectablePetModel(KPetWithReminders kPet)
            {
                Id = kPet.Id;
                Name = kPet.Name;
                IsSelected = false;
            }

            public string Id { get; private set; }

            public string Name { get; private set; }

            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
		public void Init(bool isVideo) {
			_isVideo = isVideo;
            LoadData();
		}
    }
}