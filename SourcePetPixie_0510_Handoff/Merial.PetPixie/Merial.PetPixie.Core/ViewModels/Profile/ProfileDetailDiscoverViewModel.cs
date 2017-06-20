using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels {
	public class ProfileDetailDiscoverViewModel : ViewModelBase {
		private const int ElementByLine = 3;
		private readonly IFeedService _feedService;
		private ObservableCollection<DiscoverItemViewModel> _items;
		private readonly Random _random;
		private string _petId;
		private string _profileId;
	    private  ViewMessages _messages = ViewMessages.CreateFrom($"No Post yet.");


	    public override ViewMessages Messages
	    {
	        get { return _messages; }
	        set
	        {
	            _messages = value;
                RaisePropertyChanged(() => Messages);
            }
	    }

	    public ObservableCollection<DiscoverItemViewModel> Items {
			get { return _items; }
			set {

				_items = value;
                if (Items != null && Items.Any())
                {
                    IsThereContent = ContentStatus.ContentProvided;
                }
                else
                {
                    IsThereContent = ContentStatus.NoContent;
                }
                RaisePropertyChanged(() => Items);
			}
		}

		public IMvxCommand LoadMoreDiscoverCommand => new SafeMvxCommandAsync<int>((async total =>
		{
		    IsFetchingData = true;
            Items = await GetDiscoverElements(total, Items);
            IsFetchingData = false;
        }));

		public ProfileDetailDiscoverViewModel(IFeedService feedService) {
			_feedService = feedService;
			_random = new Random();
		}

		protected override async Task<bool> LoadDataAsync() {
            IsFetchingData = true;
            IsThereContent = ContentStatus.Undefined;
            if (_profileId != null)
				Items = await GetDiscoverElements(0, new ObservableCollection<DiscoverItemViewModel>());

            IsThereContent = Items.Any() ? ContentStatus.ContentProvided : ContentStatus.NoContent;
            IsFetchingData = false;
            return await base.LoadDataAsync();
		}

	    private void MajMessages()
	    {
	        var userID = Settings.CurrentUserProfile.Id;

            if (_profileId == userID)
	        {
	            Messages =ViewMessages.CreateFrom($"No Post yet.{Environment.NewLine}Starts now by clicking the share button.");
	        }
	        else
	        {
	            Messages =ViewMessages.CreateFrom($"No Post yet.");

            }
        }

	    private async Task<ObservableCollection<DiscoverItemViewModel>> GetDiscoverElements(int total, ObservableCollection<DiscoverItemViewModel> previousItems) {
			if (_profileId == null)
				return previousItems;
			var discovers = await _feedService.GetFeedByUserAsync(_profileId, _petId, CountTotalPreviousItems(previousItems));
			if (discovers.Count == 0) {
				return previousItems;
			}
			var items = new ObservableCollection<DiscoverItemViewModel>();
			var position = 0;

			// Check if previous items are all fill
			if (previousItems.Count > 0) {
				var lastPreviousItem = previousItems.Last();
				if (lastPreviousItem.Image2 == null) {
					lastPreviousItem.Image2 = FeedModel.CreateFrom(discovers.FirstOrDefault());
					++position;
					if (lastPreviousItem.Image3 == null && discovers.Count >= 2) {
						lastPreviousItem.Image3 = FeedModel.CreateFrom(discovers.Skip(1).FirstOrDefault());
						++position;
					}
				}
			}

			// assign left discover item to new vm
			while (position < discovers.Count) {
				items.Add(CreateOneModel(discovers.Skip(position).Take(ElementByLine).ToArray()));
				position += ElementByLine;
			}

			return new ObservableCollection<DiscoverItemViewModel>(previousItems.Concat(items));
		}

		private int CountTotalPreviousItems(ObservableCollection<DiscoverItemViewModel> previousItems) {
			var count = 0;
			foreach (var discoverItemViewModel in previousItems) {
				if (discoverItemViewModel.Image3 != null) {
					count += ElementByLine;
				}
				else if (discoverItemViewModel.Image2 != null) {
					count += ElementByLine - 1;
				}
				else {
					++count;
				}
			}
			return count;
		}

		private DiscoverItemViewModel CreateOneModel(IReadOnlyList<KFeed> items) {
			return new DiscoverItemViewModel {
				Image1 = items.Count > 0 ? FeedModel.CreateFrom(items[0]) : null,
				Image2 = items.Count > 1 ? FeedModel.CreateFrom(items[1]) : null,
				Image3 = items.Count > 2 ? FeedModel.CreateFrom(items[2]) : null,
				Choice = items.Count > 2?_random.Next(1, 3):3
			};
		}


		public void Init(ProfileFeedViewModel.ProfileFeedParameters parameters) {
			_petId = parameters.PetId;
			_profileId = parameters.ProfileId;
            MajMessages();

        }

        public async void SetParameters(string profileId, string petId) {
			_profileId = profileId;
            MajMessages();
            IsFetchingData = true;
            IsThereContent = ContentStatus.Undefined;
            if (Items == null || _petId != petId) {
				_petId = petId;
				Items = await GetDiscoverElements(0, new ObservableCollection<DiscoverItemViewModel>());
			}
            IsThereContent = Items.Any() ? ContentStatus.ContentProvided : ContentStatus.NoContent;

            IsFetchingData = false;
        }
	}
}