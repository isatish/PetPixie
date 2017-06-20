using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FollowingsViewModel : ViewModelBase<ProfileModel>
    {
        #region Fields

        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        private ObservableCollection<ProfileItemViewModel> _profiles;
        private ObservableCollection<ProfileItemViewModel> _allProfiles;
        private string _searchString;
        private ProfileModel _profileModel;

        #endregion

        #region Constructors

        public FollowingsViewModel(IFriendService friendService, IUserService userService)
        {
            _friendService = friendService;
            _userService = userService;
        }

        #endregion

        #region Properties

        public ObservableCollection<ProfileItemViewModel> Profiles
        {
            get { return _profiles; }
            set
            {
                _profiles = value;
                RaisePropertyChanged(() => Profiles);
            }
        }

        public ObservableCollection<ProfileItemViewModel> AllProfiles
        {
            get { return _allProfiles; }
            set
            {
                _allProfiles = value;
                RaisePropertyChanged(() => AllProfiles);
            }
        }

        public string SearchString {
            get { return _searchString; }
            set
            {
                if (AllProfiles == null || !AllProfiles.Any())
                    return;
                _searchString = value;
                Profiles = string.IsNullOrEmpty(value) 
                    ? new ObservableCollection<ProfileItemViewModel>(AllProfiles) 
                    : new ObservableCollection<ProfileItemViewModel>(AllProfiles.Where(p => p.Profile.UserName.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
                RaisePropertyChanged(() => this.SearchString);
                RaisePropertyChanged(() => Profiles);
            }
        }

        #endregion

        #region Methods

        protected override async Task<bool> LoadDataAsync()
        {
            return await LoadData();
        }

        private async Task<bool> LoadData()
        {
            var connectedProfileId = _userService.GetProfileId();
            var currentProfileId = _profileModel.ProfileId;
            var tsc_followings = _friendService.GetFollowingOf(currentProfileId);

            var tsc_followingsOfConnectedUser =
                connectedProfileId != currentProfileId 
                    ? _friendService.GetFollowingOf(connectedProfileId)
                    : tsc_followings;

            await Task.WhenAll(tsc_followingsOfConnectedUser, tsc_followings);
            var followings = tsc_followings.Result;
            var followingsOfConnectedUser = tsc_followingsOfConnectedUser.Result.ToList();

            this.Profiles = new ObservableCollection<ProfileItemViewModel>();

            List<ProfileItemViewModel> returnProfile = new List<ProfileItemViewModel>();
            foreach (var following in followings)
            {
                returnProfile.Add(new ProfileItemViewModel(new ProfileModel(connectedProfileId)
                {
                    ProfileId = following.Id,
                    ImageSrc = following.ExpandedProfilePictures?.KSmall?.DownloadURL,
                    UserName = !string.IsNullOrEmpty(following.DisplayUsername) ? following.DisplayUsername : following.FullName,
                    UserFullName = !string.IsNullOrEmpty(following.DisplayUsername) ? following.FullName : string.Empty,
                    IsFollowedByCurrentUser = followingsOfConnectedUser.Any(kp => kp.Id == following.Id),
                    IsRegisteredInPetPal = true
                }));
            }

            Profiles = new ObservableCollection<ProfileItemViewModel>(returnProfile);
            AllProfiles = new ObservableCollection<ProfileItemViewModel>(returnProfile);

            return true;
        }

        #endregion

        protected override async void RealInit(ProfileModel parameter)
        {
            _profileModel = parameter;
            await LoadDataAsync();
        }
    }
}