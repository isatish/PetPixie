using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels {
	public class VetMapSettingsViewModel : ViewModelBase {
		public const string Distance = "distance";
		public const string Alphabetical = "A-Z";
		public const string Rating = "rating";
		private string _sortListSelected;
		private ObservableCollection<string> _sortListElements;
		private UserRatingModel _userRatingSelected;
		private ObservableCollection<UserRatingModel> _userRatingElements;





		public VetMapSettingsViewModel() {
			SortListElements = new ObservableCollection<string> {
				Distance,
				Alphabetical,
				Rating
			};
			UserRatingElements = new ObservableCollection<UserRatingModel> {
				new UserRatingModel {
					Text = "Any",
					Value = null
				},
				new UserRatingModel {
					Text = null,
					Value = 2
				},
				new UserRatingModel {
					Text = null,
					Value = 3
				},
				new UserRatingModel {
					Text = null,
					Value = 4
				}
			};
		}

		public IMvxCommand ValidateSettingsCommand => new SafeMvxCommand(() => {
			var mapSettings = Settings.MapSettings ?? new MapSettings();
			mapSettings.UserRating = UserRatingSelected;
			mapSettings.Sort = SortListSelected;
			Settings.MapSettings = mapSettings;
			Close(this);
		});

	    public override void Paused()
	    {
	        base.Paused();
            var mapSettings = Settings.MapSettings ?? new MapSettings();
            mapSettings.UserRating = UserRatingSelected;
            mapSettings.Sort = SortListSelected;
            Settings.MapSettings = mapSettings;

        }

	    public ObservableCollection<string> SortListElements {
			get { return _sortListElements; }
			set {
				_sortListElements = value;
				RaisePropertyChanged(() => SortListElements);
			}
		}

		public string SortListSelected {
			get { return _sortListSelected; }
			set {
				_sortListSelected = value;
				RaisePropertyChanged(() => SortListSelected);
			}
		}

		public ObservableCollection<UserRatingModel> UserRatingElements {
			get { return _userRatingElements; }
			set {
				_userRatingElements = value;
				RaisePropertyChanged(() => UserRatingElements);
			}
		}

		public UserRatingModel UserRatingSelected {
			get { return _userRatingSelected; }
			set {
				_userRatingSelected = value;
				RaisePropertyChanged(() => UserRatingSelected);
			}
		}

		protected override Task<bool> LoadDataAsync() {
			var mapSettings = Settings.MapSettings ?? new MapSettings();
			SortListSelected = mapSettings.Sort ?? SortListElements.FirstOrDefault();
			UserRatingSelected = mapSettings.UserRating ?? UserRatingElements.FirstOrDefault();
			return base.LoadDataAsync();
		}
	}
}