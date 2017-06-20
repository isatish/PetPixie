using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using KinveyXamarin;
using Merial.PetPixie.Core.Common.Exceptions;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using MvvmValidation;
using Splat;

namespace Merial.PetPixie.Core.ViewModels
{
    public class CompleteAccountViewModel : ValidationFormViewModelBase
    {
        #region Fields

        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly IBitmapService _bitmapService;
        private readonly IUploadService _uploadService;
        private readonly IKinveyService _kinveyService;
        private readonly IFacebookService _facebookService;
        private readonly ITwitterService _twitterService;

        private IMvxPictureChooserTask _mvxPictureChooserTask;

        private string _email = "";
        private byte[] _pictureBytes;
        private bool _isPictureSelected = false;
        private FileMetaData _fileMetaData;
        private string _username = "";
        private string _fullname = "";

        #endregion

        #region Constructors

        public CompleteAccountViewModel(
            IUserService userService, 
            IProfileService profileService, 
            IBitmapService bitmapService, 
            IUploadService uploadService, 
            IKinveyService kinveyService,
            IFacebookService facebookService,
            ITwitterService twitterService)
        {
            _userService = userService;
            _profileService = profileService;
            _bitmapService = bitmapService;
            _uploadService = uploadService;
            _kinveyService = kinveyService;
            _facebookService = facebookService;
            _twitterService = twitterService;

            _facebookService.DataResponse += this.FillProfileInfos;
            _twitterService.DataResponse += this.FillProfileInfos;
        }

        #endregion

        #region Events

        public event EventHandler LoggedIn;

        #endregion

        #region Properties

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _email, value);
            }
        }

        public bool CanEditEmail { get; set; }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _username, value);
            }
        }

        public string Fullname
        {
            get
            {
                return _fullname;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _fullname, value);
            }
        }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool ConfirmEULA { get; set; }
		public bool EmSubscription { get; set; }

        public FileMetaData FileMetaData
        {
            get { return _fileMetaData; }
            set
            {
                _fileMetaData = value;
                RaisePropertyChanged(() => FileMetaData);
            }
        }

        public byte[] PictureBytes
        {
            get { return _pictureBytes; }
            set { _pictureBytes = value; RaisePropertyChanged(() => PictureBytes); }
        }

        public bool IsPictureSelected {
            get { return _isPictureSelected; }
            set {
                _isPictureSelected = value;
                RaisePropertyChanged(() => IsPictureSelected);
            }
        }
        
        #endregion

        #region Commands

        public IMvxCommand CreateAccountCommand => new SafeMvxCommandAsync(CreateAccountCommandExecute);

        private async Task CreateAccountCommandExecute()
        {
            // If connection with Twitter or Facebook
            if (this.CanEditEmail)
                await this.CompleteSocialAccount();
            else
                await this.CreateAccount();
        }

        public IMvxCommand TakePictureFromCameraCommand => new SafeMvxCommand(TakePictureFromCameraCommandExecute);

        private void TakePictureFromCameraCommandExecute()
        {
            _mvxPictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            _mvxPictureChooserTask.TakePicture(800, 100, PictureAvailable, AssumeCancelled);
        }

        public IMvxCommand TakePictureFromGalleryCommand => new SafeMvxCommand(TakePictureFromGalleryCommandExecute);

        private void TakePictureFromGalleryCommandExecute()
        {
            _mvxPictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();
            _mvxPictureChooserTask.ChoosePictureFromLibrary(800, 100, PictureAvailable, AssumeCancelled);
        }
		public IMvxCommand Signup => new SafeMvxCommand(() => Close(this));

        #endregion

        #region Public Methods

        public override void AddValidationFields(ValidationHelper helper)
        {
            helper.AddRequiredRule((() => Email), "Required");
			helper.AddRule(() => Email, () => RuleResult.Assert(InputValidationHelper.ValidateEmail(Email), "Invalid email"));
            helper.AddRequiredRule((() => Username), "Required");
            helper.AddRequiredRule((() => Fullname), "Required");
            helper.AddRule(() => ConfirmEULA, () => RuleResult.Assert(ConfirmEULA, "You must be over the age of 15 to use this app"));
            helper.AddRule(() => Username, () => RuleResult.Assert(Username.Length >= 4, "Minimun 4 charaters"));

            // Cas des comptes par facebook ou twitter, le DisplayUsername n'a pas cette règle
            if (!this.CanEditEmail)
            {
                helper.AddRule(() => Username, () => RuleResult.Assert(InputValidationHelper.ValidateUserName(Username), "Invalid username"));
                helper.AddRule(() => Password, () => RuleResult.Assert(Password.Length >= 8, "Minimum 8 characters"));
                helper.AddRule(() => ConfirmPassword, () => RuleResult.Assert(Password == ConfirmPassword, "Not matching"));
                helper.AddRequiredRule((() => Password), "Required");
                helper.AddRequiredRule((() => ConfirmPassword), "Required");
            }
        }

        public void Init(string email,string username,string password,string confirmP)
        {
            this.CanEditEmail = string.IsNullOrEmpty(email); // Edition autorisé si connecté par Facebook ou Twitter

			Email = (email!=null)?email.ToLower():email;
			Username = username;
			Fullname = username;
			Password = password;
			ConfirmPassword = confirmP;
            
            if (this.CanEditEmail)
            {
                this.IsFetchingData = true;

                switch (this._userService.UserType)
                {
                    case UserType.Facebook:
                        this._facebookService.GetFacebookData();
                        break;
                    case UserType.Twitter:
                        this._twitterService.GetTwitterData();
                        break;
                }
            }
        }

        public void FillProfileInfos(object sender, SocialUserModel profileInfos)
        {
            this.Email = profileInfos.Email;
            this.Fullname = profileInfos.FullName;
			if (this._userService.UserType != UserType.Facebook)
            this.Username = profileInfos.UserName;
			this.FileMetaData = new FileMetaData() { downloadURL = profileInfos.ImageUrl };
            this.IsFetchingData = false;
        }
        
        #endregion

        #region Private Methods

        private void PictureAvailable(Stream stream)
        {
            Mvx.Resolve<IImageService>().CropPicture(stream, PictureAvailableAfterCrop, AssumeCancelled,true);
        }

        private void PictureAvailableAfterCrop(FileMetaData fileMetaData)
        {
            FileMetaData = fileMetaData;
        }

        private void AssumeCancelled()
        {
            // do nothing
        }

        private async Task CreateAccount()
        {
         //   if (!Validate()) return;

            IsFetchingData = true;
            
            try
            {
                var user = await _userService.SignupAsync(Username, Password, Email);
                await _userService.LoginAutomatedAsync(Constants.Connection.KinveyUsername + Username, Password);
                await this.CompleteAccount(user);
            }
            catch (UsernameAlreadyExistsException e)
            {
                PopupService.DisplayMessage(e.Message,"Error");
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("UsernameAlreadyExists"))
                    PopupService.DisplayMessage("This username already exists. Please choose another one.", "Error");
                else
                {
                    Logger.Error(e);
                    PopupService.DisplayMessage("An unexpected error occured. Please try again","Error");
                }
            }
            finally
            {
                IsFetchingData = false;
            }
        }

        private async Task CompleteSocialAccount()
        {
       //     if (!Validate()) return;

            IsFetchingData = true;

            try
            {
                await this.CompleteAccount();
            }
            catch (UsernameAlreadyExistsException e)
            {
                PopupService.DisplayMessage(e.Message, "Error");
            }
            catch (Exception e)
            {
                if (e.Message.StartsWith("UsernameAlreadyExists"))
                    PopupService.DisplayMessage("This username already exists. Please choose another one.", "Error");
                else
                {
                    Logger.Error(e);
                    PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                }
            }
            finally
            {
                IsFetchingData = false;
            }
        }

        private async Task CompleteAccount(User user = null)
        {
            // We add the photo and info in +
            var newProfileId = this._userService.GetProfileId();
            var newProfile = await this._profileService.GetProfileById(newProfileId);

            //Upload Picture
            var fileMetaData = await UploadPicture();

            if (fileMetaData != null && !string.IsNullOrWhiteSpace(fileMetaData.id))
            {
                newProfile.ProfilePicture = new KImage
                {
                    Id = fileMetaData.id,
                    Type = "KinveyFile",
                    MimeType = "image/jpeg"
                };
            }
            FileMetaData = null;
            //End- Upload Picture

            newProfile.FullName = this.Fullname;
            newProfile.Pets = new KPetWithReminders[] { };
            newProfile.Counts = new KProfileCounts();
            newProfile.Email = Email;
			newProfile.DisplayUsername = this.Username;
            newProfile.profileCompleteVersion = Constants.Profile.AccountCompletedProfileLevel;
            newProfile.NotificationSettings = new KNotificationSettings
            {
                NewFollower = true,
                Mention = true,
                HealthAlert = true,
                Comment = true,
                LikeMedia = true,
                FriendJoined = true
            };
			newProfile.EmailSubscription = this.EmSubscription;

            await this._profileService.EditProfile(newProfile);

            Settings.Email = Email;
            Settings.Password = Password;
            Settings.CurrentUser = user ?? this._kinveyService.GetClient().User();
            var currentProfileId = _userService.GetProfileId();
            var profile = await _profileService.GetProfileById(currentProfileId);
            Settings.CurrentUserProfile = profile;

            this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

            var presentationBundle =
                new MvxBundle(new Dictionary<string, string>
                {
                        {Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack}
                });
           //ds ShowViewModel<MainViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
             ShowViewModel<FeedViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
        }

        private async Task<FileMetaData> UploadPicture()
        {
            if (FileMetaData == null)
                return null;

            try
            {
                using (var bitmap = _bitmapService.GetBitmap(FileMetaData.downloadURL))
                {
                    if (bitmap == null)
                        return null;

                    using (var stream = new MemoryStream())
                    {
                        await bitmap.Save(CompressedBitmapFormat.Png, 0, stream);
                        return await _uploadService.UploadImage(stream.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }
        
        #endregion
    }
}