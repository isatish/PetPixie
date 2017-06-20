using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels {
	public class ProfileDetailFeedViewModel : ViewModelBase {
		private readonly IFeedService _feedService;
		private readonly IUserService _userService;
		private ObservableCollection<FeedItemViewModel> _feeds;
		private string _petId;
		private string _profileId;

	    public override ViewMessages Messages => ViewMessages.CreateFrom($"No Post yet.");


        public ProfileDetailFeedViewModel(IFeedService feedService, IUserService userService) {
			_feedService = feedService;
			_userService = userService;
			_profileId = Settings.CurrentUserProfile.Id;
		}

		public ObservableCollection<FeedItemViewModel> Feeds {
			get { return _feeds; }
			set {
				_feeds = value;
			    if (Feeds != null && Feeds.Any())
			    {
                    IsThereContent=ContentStatus.ContentProvided;
			    }
			    else
			    {
                    IsThereContent = ContentStatus.NoContent;
                }
				RaisePropertyChanged(() => Feeds);
			}
		}

		public IMvxCommand FeedClickCommand => new SafeMvxCommand<FeedItemViewModel>(item => {
			ShowViewModel<FeedItemViewModel, FeedItemViewModel.FeedItemParameters>(new FeedItemViewModel.FeedItemParameters { Feed = item.Feed });
		});

		public IMvxCommand LoadMoreFeedCommand => new SafeMvxCommandAsync<int>(LoadMoreDataAsync);

		private async Task LoadMoreDataAsync(int total) {
			if (_profileId == null)
				return;
            IsFetchingData = true;
            var feeds = new ObservableCollection<FeedItemViewModel>((await _feedService.GetFeedByUserAsync(_profileId, _petId, total))
				.Select(kfeed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(kfeed))));
			Feeds = new ObservableCollection<FeedItemViewModel>(Feeds.Concat(feeds));
            IsFetchingData = false;
        }

		protected override async Task<bool> LoadDataAsync() {
            IsFetchingData = true;
            IsThereContent =ContentStatus.Undefined;
            if (_profileId != null)
				Feeds = new ObservableCollection<FeedItemViewModel>((await _feedService.GetFeedByUserAsync(_profileId, _petId))
					.Select(kfeed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(kfeed))));
            MajMessages();

            IsThereContent = Feeds.Any() ? ContentStatus.ContentProvided : ContentStatus.NoContent;
            IsFetchingData = false;
            return await base.LoadDataAsync();
		}

		public void Init(ProfileFeedViewModel.ProfileFeedParameters parameters) {
			_petId = parameters.PetId;
			_profileId = parameters.ProfileId;
		}

        private void MajMessages()
        {
            var userID = Settings.CurrentUserProfile.Id;

            if (_profileId == userID)
            {
                Messages = ViewMessages.CreateFrom($"No Post yet.{Environment.NewLine}Starts now by clicking the share button.");
            }
            else
            {
                Messages = ViewMessages.CreateFrom($"No Post yet.");

            }
        }

        public async void SetParameters(string profileId, string petId) {
			_profileId = profileId;
            IsFetchingData = true;
            MajMessages();
            IsThereContent = ContentStatus.Undefined;

            if (Feeds == null || _petId != petId) {

				_petId = petId;
				if (_feedService != null)
					Feeds = new ObservableCollection<FeedItemViewModel>((await _feedService.GetFeedByUserAsync(_profileId, _petId))
						.Select(kfeed => new FeedItemViewModel(_feedService, _userService, FeedModel.CreateFrom(kfeed))));
			}
            IsThereContent = Feeds.Any() ? ContentStatus.ContentProvided : ContentStatus.NoContent;
            IsFetchingData = false;
        }
	}
}