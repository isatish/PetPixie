using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private string _displayUsername;
        private string _email;
        private string _picture;

        private readonly IProfileService _profileService;
        public IMvxCommand HomeCommand => new SafeMvxCommand(() => ShowViewModel<FeedViewModel>());


        public IMvxCommand DiscoveryCommand => new SafeMvxCommand(() =>   ShowViewModel<DiscoverViewModel, DiscoverParameter>(null));
                                 
                                                                 








        public IMvxCommand SettingsCommand => new SafeMvxCommand(() => ShowViewModel<SettingsViewModel>());
        public IMvxCommand MyVetsCommand => new SafeMvxCommand(() => ShowViewModel<MyVetsViewModel>(new { redirect = true }));
    //    public IMvxCommand MyPackCommand=> new SafeMvxCommand(() => ShowViewModel<MyPackViewModel, ProfileDetailParameter>(null));

        public MenuViewModel(IProfileService profileService)
        {
            _profileService = profileService;
            _profileService.OnProfileChangeEvent+=ProfileServiceOnOnProfileChangeEvent;
            InitValue();
        }

        private void ProfileServiceOnOnProfileChangeEvent(object sender, ProfilChangeArgs profilChangeArgs)
        {
            InitValue();
        }

        public override void Started()
        {
            base.Started();
            InitValue();
        }

        public  void InitValue()
        {
            var profile = Settings.CurrentUserProfile;
            Email = profile?.Email;
            DisplayUsername = profile?.DisplayUsername;
            Picture = profile?.ExpandedProfilePictures?.KMedium?.DownloadURL;
        }

        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                RaisePropertyChanged(() => Picture);
            }
        }

        public string DisplayUsername
        {
            get { return _displayUsername; }
            set
            {
                _displayUsername = value;
                RaisePropertyChanged(() => DisplayUsername);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public void ShowViewModelAndroid(MvxViewModelRequest request)
        {
            ShowViewModel(request.ViewModelType,request.ParameterValues);
        }

    }
}