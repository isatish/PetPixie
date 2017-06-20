using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels {
	public class ProfileFeedViewModel : ViewModelBase<ProfileFeedViewModel.ProfileFeedParameters> {
		private DefaultPageType _defaultPage;
		private string _petId;
		private string _profileId;

		public enum DefaultPageType {
			Feed,
			Discover
		}

		public ProfileFeedViewModel() {
			DefaultPage = DefaultPageType.Feed;
		}

		public DefaultPageType DefaultPage {
			get { return _defaultPage; }
			set {
				_defaultPage = value;
				RaisePropertyChanged(() => DefaultPage);
			}
		}

		public string PetId {
			get { return _petId; }
			set {
				_petId = value;
				RaisePropertyChanged(() => PetId);
			}
		}

		public string ProfileId {
			get { return _profileId; }
			set {
				_profileId = value;
				RaisePropertyChanged(() => ProfileId);
			}
		}


		protected override void RealInit(ProfileFeedParameters parameter) {
			DefaultPage = parameter.Page;
			PetId = parameter.PetId;
			ProfileId = parameter.ProfileId;
		}

		#region classes
		public class ProfileFeedParameters {
			public DefaultPageType Page { get; set; }
			public string PetId { get; set; }
			public string ProfileId { get; set; }
		}
		#endregion
	}
}