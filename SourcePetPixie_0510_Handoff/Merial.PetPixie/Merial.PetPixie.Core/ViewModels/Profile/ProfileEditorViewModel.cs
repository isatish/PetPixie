using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;
using MvvmValidation;
using Plugin.Media;

namespace Merial.PetPixie.Core.ViewModels
{
	public class ProfileEditorViewModel : ValidationFormViewModelBase
    {
        #region Fields

        private readonly IProfileService _profileService;
	    private readonly IPetService _petService;
		private IMvxPictureChooserTask _mvxPictureChooserTask;
        private PetReminderModel _petReminderModel;
        private ProfileModel _profile;
	    private IUserService _userService;
	    #endregion

        #region Constructors

        public ProfileEditorViewModel(IProfileService profileService, IPetService petService, IUserService userService) {
			_profileService = profileService;
		//	_mvxPictureChooserTask = mvxPictureChooserTask;     IMvxPictureChooserTask mvxPictureChooserTask,
            _petService = petService;
            _userService = userService;
        }

        #endregion

        #region Properties

        public ProfileModel Profile {
			get { return _profile; }
			set {
				_profile = value;
				RaisePropertyChanged(() => Profile);
			}
		}

        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                if (this._petReminderModel == value) return;
                this.SetProperty(ref this._petReminderModel, value);
            }
        }

        #endregion

        #region Events

        public EventHandler LoggedOut;

        #endregion

        #region Commands


        public IMvxCommand ValidateCommand => new SafeMvxCommandAsync(async () =>
        {
            if (!Validate())
                return;
            IsFetchingData = true;
            try
            {
                await _profileService.EditProfile(Profile.UpdateKProfile(Settings.CurrentUserProfile), true);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                IsFetchingData = false;
                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                return;
            }
            IsFetchingData = false;
            Close(this);
		});

		public IMvxCommand AddNewPetsCommand => new SafeMvxCommand(() => {
            ProfilePetViewModel.SelectedBreeds = new List<KBreed>();
            ShowValidationViewModel<ProfilePetViewModel,ProfilePetParameter>(new ProfilePetParameter()
			{
			    EntityMode = EntityMode.New
			});
		});

		public IMvxCommand DeleteAccountCommand => new SafeMvxCommandAsync(async () =>
		{
		    if (await PopupService
		        .DisplayYesNoMessage("Remove Account ?", $"Are you sure you want to remove your account",
		            "Yes", "No"))
		    {
		        IsFetchingData = true;
                await _profileService.DeleteAccountAsync();
                await _userService.DeleteMeAsync(); // Todo : côté kinvey, doit supprimer les deux users (dans le cas dun MerialKinvey user)
                _userService.Logout();
		        this.LoggedOut?.BeginInvoke(this, EventArgs.Empty, null, null);
		        IsFetchingData = false;

		        var presentationBundle =
		            new MvxBundle(new Dictionary<string, string>
		            {
		                {Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack}
		            });
		        ShowViewModel<SignupViewModel>(presentationBundle: presentationBundle);
		    }

		});

		public IMvxCommand TakePictureFromCameraCommand => new SafeMvxCommand(() => {

            TakePhoto();
            
//            _mvxPictureChooserTask=  Mvx.Resolve<IMvxPictureChooserTask>();
  //          _mvxPictureChooserTask.TakePicture(800, 100, PictureAvailable, AssumeCancelled);
		});

		public IMvxCommand TakePictureFromGalleryCommand => new SafeMvxCommand(() => {
            _mvxPictureChooserTask = Mvx.Resolve<IMvxPictureChooserTask>();

            _mvxPictureChooserTask.ChoosePictureFromLibrary(800, 100, PictureAvailable, AssumeCancelled);
        });

        public IMvxCommand OpenPetReminders => new SafeMvxCommand<PetModel>(pet =>
        {
            ShowViewModel<PetReminderListViewModel, PetReminderListParameter>(new PetReminderListParameter
            {
                PetReminderModel = new PetReminderModel
                {
                    PetId = pet.Id,
                    PetPictureUrl = pet.ImageUrl,
                    PetName = pet.Name,

                }
            });

        });

        public IMvxCommand RemovePet => new SafeMvxCommandAsync<PetModel>(async (pet) =>
        {
            if (await Mvx.Resolve<IPopupService>()
                .DisplayYesNoMessage("Remove Pet ?", $"Are you sure you want to remove {pet.Name}",
                    "Yes", "No"))
            {
                IsFetchingData = true;
                await _petService.RemovePet(pet);
                Profile.Pets =new ObservableCollection<PetModel>(Settings.CurrentUserProfile.Pets.Select(p=>PetModel.CreateFrom(p)));
                Profile = Profile;
                IsFetchingData = false;
            }

        });

        public IMvxCommand EditPet => new SafeMvxCommand<PetModel>( (pet) =>
        {
            ShowValidationViewModel<ProfilePetViewModel, ProfilePetParameter>(new ProfilePetParameter()
            {
                EntityMode = EntityMode.Edit,
                PetModel = pet,
            });

        });

        #endregion

        #region Methods


        public async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                // DisplayAlert("No Camera", ":( No camera available.", "OK");

                PopupService.DisplayMessage("No Camera", $"Camera is unavailable");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file != null)
                {


                    Profile.ImageSrc = file.Path;
                    Profile.ImageSrclarge = file.Path;

                    Profile.FileMetaData = new FileMetaData() { downloadURL = file.Path };//.ImageUrl };
                    App.FunPictureBytes = StreamHelper.ReadFully(file.GetStream());
                    Settings.CurrentFunImageSource = file.Path;
                    Settings.CurrentUserProfile.ExpandedProfilePictures.KSmall = new KSmall() { DownloadURL = file.Path };
                    Settings.CurrentUserProfile.ExpandedProfilePictures.KMedium = new KMedium() { DownloadURL = file.Path };
                    Settings.CurrentUserProfile.ExpandedProfilePictures.KLarge = new KSmall() { DownloadURL = file.Path };
                }
            }

        }


        private void PictureAvailable(Stream stream) {
            Mvx.Resolve<IImageService>().CropPicture(stream,PictureAvailableAfterCrop, AssumeCancelled);

           
                //       Mvx.Resolve<IImageService>().CropPicture(stream, PictureAvailableAfterCrop, AssumeCancelled);

		}

	    private void PictureAvailableAfterCrop(FileMetaData fileMetaData)
	    {
	        Profile.FileMetaData = fileMetaData;
        }

	    private void AssumeCancelled() {
            // do nothing
	    }
        
	    protected override void InitFromBundle(IMvxBundle parameters)
	    {
            if (Profile == null)
            {
                var profile = Settings.CurrentUserProfile;

                Profile = ProfileModel.CreateFrom(profile, Profile?.FileMetaData);
            }
            base.InitFromBundle(parameters);
	    }

	    public override void AddValidationFields(ValidationHelper helper)
        {
            helper.AddRequiredRule((() => Profile.UserFullName), "Required");
            helper.AddRequiredRule((() => Profile.UserName), "Required");
            helper.AddRequiredRule((() => Profile.Country), "Required");
           
            helper.AddRule(() => Profile.UserFullName, () => RuleResult.Assert(Profile.UserFullName.Length >= 4, "Minimun 4 charaters"));
            helper.AddRule(() => Profile.UserName, () => RuleResult.Assert(Profile.UserName.Length >= 4, "Minimun 4 charaters"));
          
        }

        #endregion
    }
}