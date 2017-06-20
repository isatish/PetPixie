using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Merial.PetPixie.Core.ViewModels
{
    public class ProfileItemViewModel : ViewModelBase<ProfileModel>
    {
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        private ProfileModel _profile;
        private bool _isLoading;

        public ProfileItemViewModel(ProfileModel profileModel)
        {
            this._friendService = Mvx.Resolve<IFriendService>();
            this._userService = Mvx.Resolve<IUserService>(); ;
            this.Profile = profileModel;
            this.IsNotData = false;
            this.IsPartTitle = false;
            this.PartTitle = string.Empty;
        }

        public ProfileItemViewModel(string partTitle)
        {
            this._friendService = Mvx.Resolve<IFriendService>();
            this._userService = Mvx.Resolve<IUserService>();
            this.PartTitle = partTitle;
            this.IsPartTitle = true;
            this.IsNotData = false;

            this.Profile = new ProfileModel(string.Empty);
        }

        public ProfileItemViewModel()
        {
            this._friendService = Mvx.Resolve<IFriendService>();
            this._userService = Mvx.Resolve<IUserService>();
            this.IsPartTitle = false;
            this.IsNotData = true;
            
        }

        public bool IsPartTitle { get; private set; }

        public bool IsNotData
        {
            get { return _isNotData; }
            set
            {
                _isNotData = value;
                this.RaisePropertyChanged(() => this.IsNotData);
            }
        }

        public string PartTitle { get; private set; }

        public EventHandler FollowingChangedEventHandler;

        public EventHandler InvitCommandEventHandler;
        private bool _isNotData = false;

        public ProfileModel Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                this.RaisePropertyChanged(() => this.Profile);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }

        public ICommand FollowCommand => new SafeMvxCommandAsync(FollowProfile);

        public ICommand UnfollowCommand => new SafeMvxCommandAsync(UnfollowProfile);

        public ICommand ShowProfileCommand => new SafeMvxCommand(ShowProfile);

        public ICommand InviteCommand => new SafeMvxCommand(InviteProfile);

        private void ShowProfile()
        {
            if (!this.Profile.IsRegisteredInPetPal) return;

            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter{ ProfileId = this.Profile.ProfileId });
        }

        protected override void RealInit(ProfileModel parameter)
        {
            this.Profile = parameter;
        }

        private async Task FollowProfile()
        {
            this.Profile.IsLoading = true;

            var currentProfileId = this._userService.GetProfileId();
            try
            {
                var relation = await this._friendService.FollowProfile(currentProfileId, this.Profile.ProfileId);

                if (relation != null) this.Profile.IsFollowedByCurrentUser = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Debug.WriteLine(e.Message);
                // tODO : Afficher un message d'erreur
            }

            FollowingChangedEventHandler?.Invoke(this, EventArgs.Empty);

            this.Profile.IsLoading = false;
        }

        private async Task UnfollowProfile()
        {
            this.Profile.IsLoading = true;

            var currentProfileId = this._userService.GetProfileId();
            try
            {
                var result = await this._friendService.UnfollowProfile(currentProfileId, this.Profile.ProfileId);

                if (result) this.Profile.IsFollowedByCurrentUser = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Debug.WriteLine(e.Message);
                // tODO : Afficher un message d'erreur
            }

            FollowingChangedEventHandler?.Invoke(this, EventArgs.Empty);
            this.Profile.IsLoading = false;
        }

        private void InviteProfile()
        {
            if (this.Profile.IsRegisteredInPetPal) return;
            InvitCommandEventHandler.Invoke(this, EventArgs.Empty);
        }
    }
}
