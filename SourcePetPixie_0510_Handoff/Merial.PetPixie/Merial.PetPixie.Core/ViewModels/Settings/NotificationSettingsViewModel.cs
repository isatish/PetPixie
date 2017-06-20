using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class NotificationSettingsViewModel : ViewModelBase
    {
        #region Fields

        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private KProfile _profile;
        private ObservableCollection<NotificationSettingModel> _notificationSettings;
        private bool _isLoading;

        #endregion

        #region Constructors

        public NotificationSettingsViewModel(IProfileService profileService, IUserService userService)
        {
            _profileService = profileService;
            _userService = userService;
        }

        #endregion

        #region Properties

        public ObservableCollection<NotificationSettingModel> NotificationSettings
        {
            get { return _notificationSettings; }
            set
            {
                //if (value == this._notificationSettings) return;
                _notificationSettings = value;

                this.RaisePropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == this._isLoading) return;
                _isLoading = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands
        public IMvxCommand SwitchSettingCommand => new SafeMvxCommand<NotificationSettingModel>(SwitchSettingExecute);
        public ICommand BackCommand => new SafeMvxCommand(() => Close(this));

        private void SwitchSettingExecute(NotificationSettingModel notificationSettingModel)
        {
            notificationSettingModel.IsChecked = !notificationSettingModel.IsChecked;
            var index = NotificationSettings.IndexOf(notificationSettingModel);
            if (index > -1)
                NotificationSettings[index] = notificationSettingModel;
            NotificationSettings = NotificationSettings;
            //  this.RaisePropertyChanged("NotificationSettings");
        }

        #endregion

        #region Methods

        public override void Paused()
        {
            base.Paused();
            SaveSettings();

        }

        protected override async  Task<bool> LoadDataAsync()
        {

	        this._profile = Settings.CurrentUserProfile;
	        if (_profile == null) {
				var currentProfileId = this._userService.GetProfileId();
				this._profile = await this._profileService.GetProfileById(currentProfileId);
			}

			this.NotificationSettings = new ObservableCollection<NotificationSettingModel>();
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.LikeMedia, NotificationType.Likes));
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.Comment, NotificationType.Comments));
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.Mention, NotificationType.MentionsOfYou));
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.NewFollower, NotificationType.FollowsYou));
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.FriendJoined, NotificationType.NewFriendsJoinPetAndPixie));
            this.NotificationSettings.Add(new NotificationSettingModel(this._profile.NotificationSettings.HealthAlert, NotificationType.NewHealthAlert));

            //foreach (var notificationSetting in this.NotificationSettings)
            //    notificationSetting.PropertyChanged += (sender, args) => this.SaveSettings();



            return await base.LoadDataAsync();
        }

        private void SaveSettings()
        {
            this._profile.NotificationSettings.LikeMedia    = this.NotificationSettings.Single(n => n.Type == NotificationType.Likes).IsChecked;
            this._profile.NotificationSettings.Comment      = this.NotificationSettings.Single(n => n.Type == NotificationType.Comments).IsChecked;
            this._profile.NotificationSettings.Mention      = this.NotificationSettings.Single(n => n.Type == NotificationType.MentionsOfYou).IsChecked;
            this._profile.NotificationSettings.NewFollower  = this.NotificationSettings.Single(n => n.Type == NotificationType.FollowsYou).IsChecked;
            this._profile.NotificationSettings.FriendJoined = this.NotificationSettings.Single(n => n.Type == NotificationType.NewFriendsJoinPetAndPixie).IsChecked;
            this._profile.NotificationSettings.HealthAlert  = this.NotificationSettings.Single(n => n.Type == NotificationType.NewHealthAlert).IsChecked;

            this._profileService.EditProfile(this._profile,true);
        }

        #endregion
    }
}
