using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Reminder;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class ProfileDetailBaseViewModel: ViewModelBase<ProfileDetailParameter>
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        private readonly IFriendService _friendService;
        private readonly IPetService _petService;
        private readonly IFeedService _feedService;
        private readonly IBreedService _breedService;

        private ProfileModel _profile;
        private PetModel _petSelected;
        private int _postCount;
        private string _shownProfileId = "";
        private bool _isUserBlocked;

        #endregion

        #region Constructors

        public ProfileDetailBaseViewModel(IUserService userService,
            IFriendService friendService,
            IProfileService profileService,
            IPetService petService,
            IFeedService feedService,
            IBreedService breedService)
        {
            _userService = userService;
            _friendService = friendService;
            _profileService = profileService;
            _feedService = feedService;
            _breedService = breedService;
        }

        #endregion

        #region Properties

        public ProfileModel Profile
        {
            get
            {

                //if (_profile == null)
                //{
                //    var profile = Settings.CurrentUserProfile;

                //    _profile = ProfileModel.CreateFrom(profile, _profile?.FileMetaData);
                //}



                return _profile;
            }
            set
            {
                _profile = value;
                RaisePropertyChanged(() => Profile);
                RaisePropertyChanged(() => PostCount);
            }
        }


        public string ProfilePictureUrl
        {
            get
            {
                var imageSource = "no_profile_image.png";
                if (Profile != null)
                {
                    if (Profile.ImageSrc != null)
                    {
                        imageSource = Profile.ImageSrc;
                    }
                }

                return imageSource;

            }
        }


       // public KImage ProfilePicture
         public string ProfilePicture
        {
            get

            {
                string profilePicture = "";

                if (Profile != null)
                {



                    if (Profile.ImageSrc != null)
                    {
                        profilePicture = Profile.ImageSrc;
                    }
                }
                return profilePicture;

                //return Settings.CurrentUserProfile.ProfilePicture;
            }

        }


        public string UserFullName
        {
            get
            {
                string fullName = "";

                if (Profile != null)
                {
                    fullName = Profile.UserFullName;
                }
                return fullName;
            }

        }

                
        public string UserBiography
        {
            get

            {
                return Settings.CurrentUserProfile.Biography;
            }

        }

        public string UserEmail
        {
            get

            {
                return Settings.CurrentUserProfile.Email;
            }

        }


         public string TotalPosts
        {
            get

            {

                
                return Settings.CurrentUserProfile.Counts.Media.ToString();
            }

        }


        public string TotalFollowers
        {
            get

            {
                return Settings.CurrentUserProfile.Counts.Follows.ToString();
            }
        }


        public string TotalFollowedBy
        {
            get

            {
                return Settings.CurrentUserProfile.Counts.FollowedBy.ToString();
            }

        }


        public ObservableCollection<PetModel> UserPets
        {
            get 
            {
                ObservableCollection<PetModel> userPets = new ObservableCollection<PetModel>();
                if (Profile != null)
                {
                    userPets = Profile.Pets;
                }
                //  return Profile.Pets; 
                return userPets;
            }

        }



        public PetModel PetSelected
        {
            get { return _petSelected; }
            set
            {
                if (_petSelected == value && value != null)
                    _petSelected = null;
                else
                    _petSelected = value;

                SetPostCount();
                RaisePropertyChanged(() => PetSelected);
            }
        }

        public bool IsUserBlocked
        {
            get { return _isUserBlocked; }
            set
            {
                _isUserBlocked = value;
                RaisePropertyChanged(() => IsUserBlocked);
            }
        }

        public int PostCount
        {
            get
            {
                return _postCount;
            }

            set
            {
                _postCount = value;
                RaisePropertyChanged(() => PostCount);
            }
        }

        #endregion

        #region Commands

        public IMvxCommand ShowNotificationsFrameCommand => new SafeMvxCommand(ShowNotificationsFrame);

        public IMvxCommand ShowFollowersCommand => new SafeMvxCommand(ShowFollowers);
        public IMvxCommand ShowFollowingsCommand => new SafeMvxCommand(ShowFollowings);
        public IMvxCommand EditProfileCommand => new SafeMvxCommand(OnEditProfile);
        public IMvxCommand SelectReminderTypeCommand => new SafeMvxCommand(OnSelectReminderType);


        public IMvxCommand ShowFeedCommand => new SafeMvxCommand(OnShowFeed);
        public IMvxCommand ShowDiscoverCommand => new SafeMvxCommand(OnShowDiscover);
        public IMvxCommand PetSelectedCommand => new SafeMvxCommand<PetModel>(OnPetSelected);

        public IMvxCommand FollowCommand => new SafeMvxCommandAsync(FollowProfile);
        public IMvxCommand UnfollowCommand => new SafeMvxCommandAsync(UnfollowProfile);

        public IMvxCommand ShowReminderListCommand => new SafeMvxCommand<PetModel>(OnOpenReminder);
        public IMvxCommand ReportUserCommand => new SafeMvxCommandAsync(OnReportUser);
        public IMvxCommand BlockUserCommand => new SafeMvxCommandAsync(OnBlockUser);
        public IMvxCommand UnblockUserCommand => new SafeMvxCommand(OnUnblockUser);
      





        #region Command actions

        protected void OnUnblockUser()
        {
            _profileService.UnblockUser(_shownProfileId);
            IsUserBlocked = false;
            Profile.IsUserBlocked = IsUserBlocked;
        }

        protected async Task OnReportUser()
        {
            if (await PopupService
                .DisplayYesNoMessage("Report User ?", $"Are you sure you want to report {Profile.UserFullName}",
                    "Yes", "No"))
            {
                _profileService.ReportUser(_shownProfileId);
            }
        }

        protected async Task OnBlockUser()
        {
            if (await PopupService
                .DisplayYesNoMessage("Block Account ?", $"Are you sure you want to block {Profile.UserFullName}",
                    "Yes", "No"))
            {
                _profileService.BlockUser(_shownProfileId);
                IsUserBlocked = true;
                Profile.IsUserBlocked = IsUserBlocked;
            }
        }

        protected void OnPetSelected(PetModel pet)
        {
            PetSelected = pet;

            foreach (var petModel in Profile.Pets)
                petModel.IsSelected = petModel.Id == PetSelected?.Id;
        }

        protected void OnShowDiscover()
        {
            ShowViewModel<ProfileFeedViewModel, ProfileFeedViewModel.ProfileFeedParameters>(new ProfileFeedViewModel.ProfileFeedParameters
            {
                Page = ProfileFeedViewModel.DefaultPageType.Discover,
                PetId = null,
                ProfileId = Profile.ProfileId
            });
        }

        protected void OnShowFeed()
        {
            ShowViewModel<ProfileFeedViewModel, ProfileFeedViewModel.ProfileFeedParameters>(new ProfileFeedViewModel.ProfileFeedParameters
            {
                Page = ProfileFeedViewModel.DefaultPageType.Feed,
                PetId = null,
                ProfileId = Profile.ProfileId
            });
        }

        protected void OnEditProfile()
        {
            ShowViewModel<ProfileEditorViewModel>();
        }


        protected void OnSelectReminderType()
        {
             ShowViewModel<PetReminderSelectTypeViewModel>();
        }


        protected void ShowNotificationsFrame()
        {
                       
            if (Profile == null) return;

            ShowViewModel<NotificationsFrameViewModel>(Profile);
        }

        protected void ShowFollowers()
        {

            if (Profile == null) return;


            ShowViewModel<FollowersViewModel, ProfileModel>(Profile);
        }

        protected void ShowFollowings()
        {
            if (Profile == null) return;
            ShowViewModel<FollowingsViewModel, ProfileModel>(Profile);
        }

        protected async Task FollowProfile()
        {
            if (this.Profile.IsFollowedByCurrentUser) return;
            this.Profile.IsLoading = true;

            var currentProfileId = this._userService.GetProfileId();
            try
            {
                var relation = await this._friendService.FollowProfile(currentProfileId, this.Profile.ProfileId);

                if (relation != null)
                {
                    this.Profile.IsFollowedByCurrentUser = true;
                    this.Profile.Followers++;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Debug.WriteLine(e.Message);
                PopupService.DisplayMessage("An error occured while following this user. Please try again.", "Error");
            }

            this.Profile.IsLoading = false;
        }

        protected async Task UnfollowProfile()
        {
            if (!this.Profile.IsFollowedByCurrentUser) return;
            this.Profile.IsLoading = true;

            var currentProfileId = this._userService.GetProfileId();
            try
            {
                var result = await this._friendService.UnfollowProfile(currentProfileId, this.Profile.ProfileId);

                if (result)
                {
                    this.Profile.IsFollowedByCurrentUser = false;
                    this.Profile.Followers--;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Debug.WriteLine(e.Message);
                PopupService.DisplayMessage("An error occured while unfollowing this user. Please try again.", "Error");
            }

            this.Profile.IsLoading = false;
        }

        protected void OnOpenReminder(PetModel pet)
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
        }

        #endregion

        #endregion

        #region Methods

        protected override Task<bool> LoadDataAsync()
        {
            this.LoadProfile();
            return base.LoadDataAsync();
        }

        protected override void RealInit(ProfileDetailParameter parameter)
        {
            this.IsFetchingData = true; // / Place here to not show the load if we return to this screen
            this._shownProfileId = parameter?.ProfileId ?? Settings.CurrentUserProfile.Id;
            LoadDataAsync();
        }

        private async void SetPostCount()
        {
            if (_petSelected != null)
                PostCount = await _profileService.GetPostCountByPet(_petSelected.Id, this.Profile.ProfileId);
            else
                PostCount = this.Profile.Posts;
        }

        protected async void LoadProfile()
        {
            var tsc_profile = _profileService.GetProfileById(this._shownProfileId);
            var tsc_alreadyFollowedFriends = _friendService.GetFollowersOf(this._shownProfileId);
            var tsc_isUserBlocked = _profileService.IsUserBlocked(_shownProfileId);

            await Task.WhenAll(tsc_profile, tsc_alreadyFollowedFriends, tsc_isUserBlocked);


            Profile = ProfileModel.CreateFrom(tsc_profile.Result);
            Profile.Pets = Profile.Pets;
            this.SetPostCount();

            Profile.IsUserBlocked = IsUserBlocked = tsc_isUserBlocked.Result;
            Profile.IsFollowedByCurrentUser = tsc_alreadyFollowedFriends.Result.Any(p => p.Id == Settings.CurrentUserProfile.Id);
            Profile.IsRegisteredInPetPal = true;
            IsFetchingData = false;


        }

        #endregion
    }

    public class ProfileDetailParameter
    {
        public string ProfileId { get; set; }
    }
}
