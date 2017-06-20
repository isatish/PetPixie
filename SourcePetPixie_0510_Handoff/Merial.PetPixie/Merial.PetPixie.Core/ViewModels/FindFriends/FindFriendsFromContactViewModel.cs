using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FindFriendsFromContactViewModel : ViewModelBase
    {
        #region Fields

        private readonly IFriendService _friendService;
        private readonly IDeviceHelper _deviceHelper;
        private readonly IUserService _userService;

        private ObservableCollection<ProfileItemViewModel> _profilesUsing;
        private ObservableCollection<ProfileItemViewModel> _profilesNotRegistered;
        private ObservableCollection<ProfileItemViewModel> _profilesFollowed;
        private bool _isLoading;
        private ProfileModel _selectedProfile;


        private bool _showContacts = true;
        private bool _showFacebook = false;



        #endregion

        #region Constructors

        public FindFriendsFromContactViewModel(IFriendService friendService, IDeviceHelper deviceHelper, IUserService userService)
        {
            _friendService = friendService;
            _deviceHelper = deviceHelper;
            _userService = userService;
        }

        #endregion

        #region Public Properties



        public bool ShowContacts
        {
            get
            {
                return _showContacts;;
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
                return _showFacebook;;
            }
            set
            {
                _showFacebook = value;
                RaisePropertyChanged(() => this.ShowFacebook);
            }
        }


        public ObservableCollection<ProfileItemViewModel> ProfilesUsing
        {
            get { return _profilesUsing; }
            set
            {
                _profilesUsing = value;
                RaisePropertyChanged(() => ProfilesUsing);
            }
        }

        public ObservableCollection<ProfileItemViewModel> ProfilesNotRegistered
        {
            get { return _profilesNotRegistered; }
            set
            {
                _profilesNotRegistered = value;
                RaisePropertyChanged(() => ProfilesNotRegistered);
            }
        }

        public ObservableCollection<ProfileItemViewModel> ProfilesFollowed
        {
            get { return _profilesFollowed; }
            set
            {
                _profilesFollowed = value;
                RaisePropertyChanged(() => ProfilesFollowed);
            }
        }

        public ObservableCollection<ProfileItemViewModel> AllProfilesShown
        {
            get
            {
                var list = new List<ProfileItemViewModel>();
                list.Add(new ProfileItemViewModel("Friends already using Pet+Pixie"));
                //To add empty item
                if (this.ProfilesUsing == null || !this.ProfilesUsing.Any())
                {
                    //list.Add(new ProfileItemViewModel());
                }
                else
                {
                    list.AddRange(this.ProfilesUsing);

                }

                list.Add(new ProfileItemViewModel("Friends already followed on Pet+Pixie"));
                //To add empty item
                if (this.ProfilesFollowed == null || !this.ProfilesFollowed.Any())
                {
                    //list.Add(new ProfileItemViewModel());
                }
                else
                {
                    list.AddRange(this.ProfilesFollowed);
                }
                list.Add(new ProfileItemViewModel("Invite friends on Pet+Pixie"));
                //To add empty item
                if (this.ProfilesNotRegistered == null || !this.ProfilesNotRegistered.Any())
                {
                    //list.Add(new ProfileItemViewModel());
                }
                else
                {
                    list.AddRange(this.ProfilesNotRegistered);
                }
                return new ObservableCollection<ProfileItemViewModel>(list);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public ProfileModel SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                if (value == _selectedProfile) return;
                _selectedProfile = value;
                RaisePropertyChanged(() => this.SelectedProfile);
            }
        }

        public EventHandler InvitEventHandler;

        #endregion

        #region Methods

        public override void Start()
        {
            base.Start();
            LoadData();
        }

        public async void LoadData()
        {
            IsLoading = true;

            var currentProfileId = _userService.GetProfileId();
            var tsc_alreadyFollowedFriends = _friendService.GetFollowingOf(currentProfileId);

            var contacts = this._deviceHelper.GetContacts();

            var allEmails = contacts.Where(c => c.Emails != null && c.Emails.Count > 0).SelectMany(c => c.Emails).Distinct().ToArray();
            var allPhones = contacts.Where(c => c.Phones != null && c.Phones.Count > 0).SelectMany(c => c.Phones).Distinct().ToArray();

            var tsc_availableFriendslist = _friendService.FindFriendsFromMailsOrPhoneNumber(allEmails, allPhones);

            this.ProfilesUsing = new ObservableCollection<ProfileItemViewModel>();
            this.ProfilesFollowed = new ObservableCollection<ProfileItemViewModel>();
            this.ProfilesNotRegistered = new ObservableCollection<ProfileItemViewModel>();

            await Task.WhenAll(tsc_alreadyFollowedFriends, tsc_availableFriendslist);
            var availableFriendslist = tsc_availableFriendslist.Result;
            var alreadyFollowedFriends = tsc_alreadyFollowedFriends.Result.ToList();

            foreach (var profil in contacts)
            {
                var profileModel = this.CreateProfile(availableFriendslist, currentProfileId, alreadyFollowedFriends, profil);

                if (!profileModel.IsRegisteredInPetPal)
                {
                    var profileViewModel = new ProfileItemViewModel(profileModel);
                    profileViewModel.InvitCommandEventHandler += ProfilModelOnInvit;
                    this.ProfilesNotRegistered.Add(profileViewModel);
                }
                else if (profileModel.IsFollowedByCurrentUser)
                {
                    var profileViewModel = new ProfileItemViewModel(profileModel);
                    profileViewModel.FollowingChangedEventHandler += ProfileModelOnPropertyChanged;
                    this.ProfilesFollowed.Add(profileViewModel);
                }
                else
                {
                    var profileViewModel = new ProfileItemViewModel(profileModel);
                    profileViewModel.FollowingChangedEventHandler += ProfileModelOnPropertyChanged;
                    this.ProfilesUsing.Add(profileViewModel);
                }
            }

            this.RaisePropertyChanged(() => this.AllProfilesShown);
            IsLoading = false;
        }

        private void ProfilModelOnInvit(object sender, EventArgs eventArgs)
        {
            var profile = ((ProfileItemViewModel)sender).Profile;
            if (profile == null) return;

            this.SelectedProfile = profile;
            InvitEventHandler.Invoke(profile, EventArgs.Empty);
        }

        private void ProfileModelOnPropertyChanged(object sender, EventArgs EventArgs)
        {
            this.UpdateListes((ProfileItemViewModel)sender);
        }

        private void UpdateListes(ProfileItemViewModel profileViewModel)
        {
            if (profileViewModel.Profile.IsFollowedByCurrentUser)
            {
                this.ProfilesFollowed.Add(profileViewModel);
                this.ProfilesUsing.Remove(profileViewModel);
            }
            else
            {
                this.ProfilesUsing.Add(profileViewModel);
                this.ProfilesFollowed.Remove(profileViewModel);
            }

            this.RaisePropertyChanged(() => this.AllProfilesShown);
        }

        private ProfileModel CreateProfile(List<KProfile> availableFriendslist, string currentProfileId, List<KProfile> alreadyFollowedFriends, ContactModel contact)
        {
            var petProfile = availableFriendslist.SingleOrDefault(friend => contact.Emails.Contains(friend.Email) || contact.Phones.Contains(friend.PhoneNumber));

            var profileModel = new ProfileModel(currentProfileId);

            if (petProfile != null)
            {
                profileModel.ProfileId = petProfile.Id;
                profileModel.IsRegisteredInPetPal = true;
                profileModel.ImageSrc = petProfile.ExpandedProfilePictures?.KSmall?.DownloadURL ?? contact.PhotoUri;
                profileModel.UserName = !string.IsNullOrEmpty(petProfile.DisplayUsername) ? petProfile.DisplayUsername : petProfile.FullName;
                profileModel.UserFullName = !string.IsNullOrEmpty(petProfile.DisplayUsername) ? petProfile.FullName : string.Empty;
                profileModel.IsFollowedByCurrentUser = alreadyFollowedFriends.Any(profile => profile.Id == petProfile.Id);
            }
            else
            {
                profileModel.ProfileId = string.Empty;
                profileModel.IsRegisteredInPetPal = false;
                profileModel.ImageSrc = contact.PhotoUri;
                profileModel.UserName = !string.IsNullOrEmpty(contact.DisplayNameAlternative) ? contact.DisplayNameAlternative : contact.DisplayName;
                profileModel.UserFullName = !string.IsNullOrEmpty(contact.DisplayNameAlternative) ? contact.DisplayName : string.Empty;
                profileModel.IsFollowedByCurrentUser = false;
                profileModel.PhoneNumber = contact.Phones?.FirstOrDefault();
                profileModel.Email = contact.Emails.FirstOrDefault();
            }

            return profileModel;
        }

        #endregion




        #region Commands

                
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


    }
}
