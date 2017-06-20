using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FollowersViewModel : ViewModelBase<ProfileModel>
    {
        #region Fields

        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        private ObservableCollection<ProfileItemViewModel> _profiles;
        private ObservableCollection<ProfileItemViewModel> _allProfiles;
        private string _searchString;
        private ProfileModel _followedProfileModel;
        private bool _loadingData;

        #endregion

        #region Constructors

        public FollowersViewModel(IFriendService friendService, IUserService userService)
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

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (AllProfiles == null || !AllProfiles.Any())
                    return;
                _searchString = value;
                Profiles = string.IsNullOrEmpty(value)
                    ? new ObservableCollection<ProfileItemViewModel>(AllProfiles)
                    : new ObservableCollection<ProfileItemViewModel>(AllProfiles.Where(p => p.Profile.UserName.IndexOf(value,StringComparison.OrdinalIgnoreCase) >=0).ToList());
                RaisePropertyChanged(() => this.SearchString);
                RaisePropertyChanged(() => Profiles);
            }
        }

        public bool LoadingData
        {
            get { return _loadingData; }
            set
            {
                _loadingData = value;
                RaisePropertyChanged(() => LoadingData);
            }
        }
        
        #endregion

        #region Methods

        protected override async void RealInit(ProfileModel parameter)
        {
            this._followedProfileModel = parameter;
            await LoadDataAsync();
        }

        protected override async Task<bool> LoadDataAsync()
        {
            return await LoadData();
        }

        private async Task<bool> LoadData()
        {
            var connectedProfileId = _userService.GetProfileId();
            var currentProfileId = _followedProfileModel.ProfileId;

            var tsc_followers = _friendService.GetFollowersOf(currentProfileId);
            var tsc_alreadyFollowedFriends = _friendService.GetFollowingOf(connectedProfileId);

            await Task.WhenAll(tsc_followers, tsc_alreadyFollowedFriends);
            var followers = tsc_followers.Result;
            var alreadyFollowedFriends = tsc_alreadyFollowedFriends.Result.ToList();

            this.Profiles = new ObservableCollection<ProfileItemViewModel>();
            List<ProfileItemViewModel> returnProfile = new List<ProfileItemViewModel>();
            foreach (var follower in followers)
            {
                returnProfile.Add(new ProfileItemViewModel(new ProfileModel(connectedProfileId)
                {
                    ProfileId = follower.Id,
                    ImageSrc = follower.ExpandedProfilePictures?.KSmall?.DownloadURL,
                    UserName = !string.IsNullOrEmpty(follower.DisplayUsername) ? follower.DisplayUsername : follower.FullName,
                    UserFullName = !string.IsNullOrEmpty(follower.DisplayUsername) ? follower.FullName : string.Empty,
                    IsFollowedByCurrentUser = alreadyFollowedFriends.Any(profile => profile.Id == follower.Id),
                    IsRegisteredInPetPal = true
                }));
            }

            Profiles = new ObservableCollection<ProfileItemViewModel>(returnProfile);
            AllProfiles = new ObservableCollection<ProfileItemViewModel>(returnProfile);

            return true;
        }

        #endregion
    }
}
