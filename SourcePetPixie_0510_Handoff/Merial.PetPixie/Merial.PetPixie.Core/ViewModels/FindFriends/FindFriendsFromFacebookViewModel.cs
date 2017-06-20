using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FindFriendsFromFacebookViewModel : ViewModelBase
    {
        #region Fields

        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        private bool _isConnectedWithFacebook;
        private bool _isLoading;
        private string currentProfileId;

        private Task<IEnumerable<KProfile>> tsc_alreadyFollowedFriends;
        private IEnumerable<KProfile> alreadyFollowedFriends;
        private ObservableCollection<ProfileItemViewModel> _allProfiles;

        private ObservableCollection<FindFriendsFromFacebookViewModel> _allTestFacebookProfiles;



        private bool _showContacts = true;
        private bool _showFacebook = false;



        #endregion

        #region Constructors

        public FindFriendsFromFacebookViewModel(IFriendService friendService, IUserService userService)
        {
            _friendService = friendService;
            _userService = userService;
        }

        #endregion

        #region Events

        public event EventHandler LoadMoreFriends;

        public event EventHandler Refresh;

        #endregion  

        #region Properties


        public bool ShowContacts
        {
            get
            {
                return _showContacts;
            }
            set
            {
                _showContacts = value;
                RaisePropertyChanged(() => this.ShowContacts);
            }
        }

        public bool ShowFacebook
        {
            get
            {
                return _showFacebook;
            }
            set
            {
                _showFacebook = value;
                RaisePropertyChanged(() => this.ShowFacebook);
            }
        }


        public bool IsConnectedWithFacebook
        {
            get
            {
                return _isConnectedWithFacebook;
            }
            set
            {
                if (value == _isConnectedWithFacebook) return;
                _isConnectedWithFacebook = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;

                this.RaisePropertyChanged();
                this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }




        public ObservableCollection<ProfileItemViewModel> AllProfiles
        {
            get
            {
                return _allProfiles;
            }

            set
            {
                _allProfiles = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }

        public bool ListIsEmpty =>
            this.IsConnectedWithFacebook
            && !this.AllProfiles.Any()
            && !this.IsLoading;

        #endregion

        #region Public Methods

        public override void Start()
        {
            base.Start();

            this.IsLoading = true;
            this.AllProfiles = new ObservableCollection<ProfileItemViewModel>();
            currentProfileId = _userService.GetProfileId();
            tsc_alreadyFollowedFriends = _friendService.GetFollowingOf(currentProfileId);

            if (!this.IsConnectedWithFacebook)
                this.IsLoading = false;
        }

        public async void LoadFacebookData(IEnumerable<string> facebookFriendIds)
        {
            if (!facebookFriendIds.Any())
            {
                IsLoading = false;
                return;
            }

            await Task.WhenAll(this.tsc_alreadyFollowedFriends);
            this.alreadyFollowedFriends = tsc_alreadyFollowedFriends.Result.ToList();

            var availableFriendslist = await _friendService.FindFriendsByFacebookId(facebookFriendIds.ToArray());

            foreach (var profil in availableFriendslist)
            {
                var profileModel = this.CreateProfile(profil, currentProfileId, alreadyFollowedFriends);
                var profileViewModel = new ProfileItemViewModel(profileModel);
                this.AllProfiles.Add(profileViewModel);
            }

            this.RaisePropertyChanged(() => this.AllProfiles);
            this.RaisePropertyChanged(() => this.ListIsEmpty);

            this.LoadMoreFriends?.BeginInvoke(this, EventArgs.Empty, null, null);
        }

        #endregion

        #region Methods

        private ProfileModel CreateProfile(KProfile friendProfile, string currentProfileId, IEnumerable<KProfile> alreadyFollowedFriends)
        {
            var profileModel = new ProfileModel(currentProfileId);

            profileModel.ProfileId = friendProfile.Id;
            profileModel.IsRegisteredInPetPal = true;
            profileModel.ImageSrc = friendProfile.ExpandedProfilePictures?.KSmall?.DownloadURL;
            profileModel.UserName = !string.IsNullOrEmpty(friendProfile.DisplayUsername) ? friendProfile.DisplayUsername : friendProfile.FullName;
            profileModel.UserFullName = !string.IsNullOrEmpty(friendProfile.DisplayUsername) ? friendProfile.FullName : string.Empty;
            profileModel.IsFollowedByCurrentUser = alreadyFollowedFriends.Any(profile => profile.Id == friendProfile.Id);


            return profileModel;
        }

        #endregion


        #region Commands

        public IMvxCommand FriendSelectedCommand => new SafeMvxCommand<FacebookUserModel>(OnFriendSelected);


                
        public IMvxCommand ShowFriendsFromFacebookCommand => new SafeMvxCommand(DoShowFriendsFromFacebookCommand);
        private void DoShowFriendsFromFacebookCommand()
        {
            ShowContacts = false;
            ShowFacebook = true;
            ShowViewModel<FindFriendsFromFacebookViewModel>();
        }

        public IMvxCommand ShowFriendsFromContactsCommand => new SafeMvxCommand(DoShowFriendsFromContactsCommand);
        private void DoShowFriendsFromContactsCommand()
        {
            ShowContacts = true;
            ShowFacebook = false;
            ShowViewModel<FindFriendsFromContactViewModel>();
        }


 
        #endregion


        #region Actions
        private void OnFindFriends()
        {
            //ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters { SelectedVet = null });
        }
        private void OnFriendSelected(FacebookUserModel friend)
        {
        //    ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters
        //    {
        //        SelectedVet = Settings.CurrentUserProfile.Vets.FirstOrDefault(v => v.Id.Equals(vet.Id))
        //        // SelectedVet = Vets.FirstOrDefault(v => v.Id.Equals(vet.Id))
        //    });
        }
        #endregion






    }
}