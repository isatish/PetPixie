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
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
    public class FindFriendsTabsViewModel : ViewModelBase
    {
        #region Fields

        private readonly IFriendService _friendService;
        private readonly IDeviceHelper _deviceHelper;
        private readonly IUserService _userService;

        private bool _showContacts = true;
        private bool _showFacebook = false;

        private ObservableCollection<ProfileItemViewModel> _profilesUsing;
        private ObservableCollection<ProfileItemViewModel> _profilesNotRegistered;
        private ObservableCollection<ProfileItemViewModel> _profilesFollowed;
        private bool _isLoading;
        private ProfileModel _selectedProfile;

        private FindFriendsFromContactViewModel _contactsViewModel;
        private FindFriendsFromFacebookViewModel _facebookViewModel;

      //  private ObservableCollection<FindFriendsFromFacebookViewModel> _allTestFacebookProfiles;





        //Facebook specific
       // private readonly IFriendService _friendService;
      //  private readonly IUserService _userService;

        private bool _isConnectedWithFacebook;
       // private bool _isLoading;
        private string currentProfileId;

        private Task<IEnumerable<KProfile>> tsc_alreadyFollowedFriends;
        private IEnumerable<KProfile> alreadyFollowedFriends;
        private ObservableCollection<ProfileItemViewModel> _allProfiles;

        private ObservableCollection<FindFriendsFromFacebookViewModel> _allTestFacebookProfiles;



        //private bool _showContacts = true;
        //private bool _showFacebook = false;

        #endregion

        #region Constructors

        public FindFriendsTabsViewModel(IFriendService friendService, IDeviceHelper deviceHelper, IUserService userService)
        {
            _friendService = friendService;
            _deviceHelper = deviceHelper;
            _userService = userService;
            ShowContacts = true;
            ShowFacebook = false;
        }

        #endregion

        #region Public Properties

        public ObservableCollection<FindFriendsFromFacebookViewModel> AllTestFacebookProfiles
        {
            get
            {
                return _allTestFacebookProfiles;
            }

            set
            {
                _allTestFacebookProfiles = value;
                this.RaisePropertyChanged();
          //ds      this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }




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


        public string TestConv
        {
            get { return "ABCDEFGHI"; }

        }
        public bool ContactsSelected
        {
            get { return true; }
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

                if (IsLoading == false)
                {
                    MessagingCenter.Send(this, "StopLoading");
                }
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

        #region Commands

        public IMvxCommand ShowFriendsFromFacebookCommand => new SafeMvxCommand(DoShowFriendsFromFacebookCommand);
        private void DoShowFriendsFromFacebookCommand()
        {
            ShowContacts = false;
            ShowFacebook = true;

            IEnumerable<string> facebookfriendIds = null;//ds - come back to this.
            LoadFacebookData(facebookfriendIds);


        }

        public IMvxCommand ShowFriendsFromContactsCommand => new SafeMvxCommand(DoShowFriendsFromContactsCommand);
        private void DoShowFriendsFromContactsCommand()
        {
            ShowContacts = true;
            ShowFacebook = false;
            // ShowViewModel<FindFriendsFromContactViewModel>();


            //  ShowViewModel<FindFriendsFromFacebookViewModel>();
            LoadContactData();

        }




        public IMvxCommand ToggleSearchTypeCommand => new SafeMvxCommand(DoToggleSearchTypeCommand);


        private void DoToggleSearchTypeCommand()
        {
            ShowContacts = !ShowContacts;
            ShowFacebook = !ShowFacebook;
            if (ShowFacebook)
            {
                ShowViewModel<FindFriendsFromFacebookViewModel>();
            }
            else
            {
                ShowViewModel<FindFriendsFromContactViewModel>();
            }
        }

        #endregion








        #region CONTACTS methods

        public async void LoadContactData()
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




        #region FACEBOOK Commands

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
            if (facebookFriendIds == null)
            {
                IsLoading = false;
                return;
            }
            
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
                var profileModel = this.CreateFacebookProfile( profil, currentProfileId, alreadyFollowedFriends);
                var profileViewModel = new ProfileItemViewModel(profileModel);
                this.AllProfiles.Add(profileViewModel);
            }

            this.RaisePropertyChanged(() => this.AllProfiles);
            this.RaisePropertyChanged(() => this.ListIsEmpty);

            this.LoadMoreFriends?.BeginInvoke(this, EventArgs.Empty, null, null);
        }

        private ProfileModel CreateFacebookProfile(KProfile friendProfile, string currentProfileId, IEnumerable<KProfile> alreadyFollowedFriends)
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

        #region FACEBOOK events

        #region Events

        public event EventHandler LoadMoreFriends;

        public event EventHandler Refresh;

        #endregion


        #endregion


        #region FACEBOOK Properties
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

        #region Methods

        //public override void Start()
        //{
        //    base.Start();
        //    this.SelectedProfile =  new ProfileModel(Settings.CurrentUserProfile.Id);


        //  //  ContactsViewModel.LoadData();
        //}

     

        private void ProfilModelOnInvitContact(object sender, EventArgs eventArgs)
        {
            var profile = ((ProfileItemViewModel)sender).Profile;
            if (profile == null) return;

            this.SelectedProfile = profile;
            InvitEventHandler.Invoke(profile, EventArgs.Empty);
        }

        private void ProfileContactModelOnPropertyChanged(object sender, EventArgs EventArgs)
        {
            this.UpdateListes((ProfileItemViewModel)sender);
        }

        private void UpdateContactListes(ProfileItemViewModel profileViewModel)
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

        private ProfileModel CreateContactProfile(List<KProfile> availableFriendslist, string currentProfileId, List<KProfile> alreadyFollowedFriends, ContactModel contact)
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
    }
}
