using System;
using System.Collections.Generic;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.EventHandler;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using Plugin.Geolocator;

namespace Merial.PetPixie.Core.ViewModels {
	public class VetMapViewModel : ViewModelBase<VetMapParameters> {
		private readonly IVetService _vetService;
		private KVet _selectedVet;
		private ObservableCollection<KVet> _vetListItems;
		private string _address;
		private string _name;
		private string _selectedVetName;

	    private ILocationService _locationService;
	    private LocationChangedEventArgs _currentLocation;
                
        private List<KVet> _vetSearchResults;

	    public LocationChangedEventArgs CurrentLocation
	    {
	        get
	        {
              

 
                if (_currentLocation == null)
	            {
                    _currentLocation = _locationService.RequestLocation();
	            }
                return _currentLocation;
	        }
	        private set { _currentLocation = value; }
	    }

	    #region Constructors
		public VetMapViewModel(IVetService vetService, ILocationService locationService)
        //public VetMapViewModel(IVetService vetService)  //DS What happened to the Location service
		{
		    _vetService = vetService;
		    _locationService = locationService;
		}

	    #endregion

		#region Commands
		public IMvxCommand SettingsCommand => new SafeMvxCommand(() => ShowViewModel<VetMapSettingsViewModel>());
		public IMvxCommand ListVetResultsCommand => new SafeMvxCommand(OnListVetResults);
		public IMvxCommand VetInfoCommand => new SafeMvxCommand(OnVetInfoCommandExecute);
		public IMvxCommand VetSelectedCommand => new SafeMvxCommand<KVet>(OnVetSelected);
        
		public IMvxCommand PositionChangedCommand => new SafeMvxCommandAsync<LatLngDistance>(OnPositionChanged);

		private void OnVetSelected(KVet vet) {
			ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters { SelectedVet = vet});
		}
		private async Task OnPositionChanged(LatLngDistance latLngDistance) {
			IsFetchingData = true;
            var mapSettings = Settings.MapSettings;
		    var sortByDistance = !string.IsNullOrWhiteSpace(mapSettings?.Sort) &&
		                         mapSettings.Sort == VetMapSettingsViewModel.Distance;



            var kVets = await _vetService.GetByLatLng(latLngDistance.Latitude, latLngDistance.Longitude, latLngDistance.NorthSouthDistance * 1.25, sortByDistance);
			
			if (mapSettings != null) {
				if (!string.IsNullOrWhiteSpace(mapSettings.Sort)) {
					switch (mapSettings.Sort) {
						case VetMapSettingsViewModel.Distance:
							// TODO default sort by distance, with kinvey
							break;
						case VetMapSettingsViewModel.Alphabetical:
							kVets = kVets.OrderBy(v => v.Name).ToList();
							break;
						case VetMapSettingsViewModel.Rating:
							kVets = kVets.OrderBy(v => v.Rating?.Value).ToList();
							break;
					}
				}
				if (mapSettings.UserRating?.Value != null) {
					kVets = kVets.Where(v => v.Rating != null && v.Rating.Value >= mapSettings.UserRating.Value.Value).ToList();
				}
			}
			if (SelectedVet != null) {
				var vet = kVets.FirstOrDefault(v => v.Id.Equals(SelectedVet.Id));
				if (vet != null) {
					_selectedVet = vet;
					kVets.Remove(vet);
					kVets.Insert(0, vet);
				}
				else {
					SelectedVet = null;
				}
			}
			VetListItems = new ObservableCollection<KVet>(kVets);
			IsFetchingData = false;
		}

		private void OnListVetResults() {
			ShowViewModel<VetMapSearchAddressViewModel, VetResultsParameters>(new VetResultsParameters { VetList = this.VetListItems.ToList() });
		}

		private void OnVetInfoCommandExecute() {
			ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters { SelectedVet = SelectedVet });
		}
		#endregion

		#region Proprietes
		public string Name {
			get { return _name; }
			set { _name = value; RaisePropertyChanged(() => Name); }
		}

		public string Address {
			get { return _address; }
			set { _address = value; RaisePropertyChanged(() => Address); }
		}

		public ObservableCollection<KVet> VetListItems {
			get { return _vetListItems; }
			set { _vetListItems = value; RaisePropertyChanged(() => VetListItems); }
		}

		public KVet SelectedVet {
			get { return _selectedVet; }
			set {
				_selectedVet = value;
				RaisePropertyChanged(() => SelectedVet);
				if (VetListItems != null && VetListItems.Any() && value != null) {
					var vet = VetListItems.FirstOrDefault(v => v.Id.Equals(SelectedVet.Id));
					if (vet != null) {
						VetListItems.Remove(vet);
						VetListItems.Insert(0, vet);
						RaisePropertyChanged(() => VetListItems);
					}
				}
			}
		}

		public string SelectedVetName {
			get { return _selectedVetName; }
			set { _selectedVetName = value; RaisePropertyChanged(() => SelectedVetName); }
		}


                
        public List<KVet> VetSearchResults
        {
            get
            {
                return _vetSearchResults;
            }
            set
            {
                _vetSearchResults = value;
                RaisePropertyChanged(() => VetSearchResults);
            }
        }

		#endregion

		public async Task<List<KVet>> SearchVetByName(string expression) {
			return (await _vetService.SearchByName(expression));
		}


        public async Task<List<KVet>> SearchByLatLong(double lat, double lng, bool near)
        {
           VetSearchResults = await _vetService.GetByLatLng(lat, lng, 10, false);


               MessagingCenter.Send<VetMapViewModel, List<KVet>>(this, "Initialized", VetSearchResults);
            return VetSearchResults;
        }


        protected override async void RealInit(VetMapParameters vetMapParameters)
        {
            SelectedVet = vetMapParameters.SelectedVet;
            if (SelectedVet?.Name != null)
            {
                SelectedVetName = SelectedVet.Name;
            }

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var currPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

           // double lat = 25.9564812;
           // double lng = -80.1392121;

            SearchByLatLong(currPosition.Latitude, currPosition.Longitude, true);


        }

        public override void Started()
	    {
	        base.Started();
            if (!IsLocationLauch)
            {
                _locationService.Start();
                IsLocationLauch = true;
                _locationService.LocationChanged+=LocationServiceOnLocationChanged;
            }
        }

	 

	    public override void Paused()
	    {
	        base.Paused();
	    }

	    public override void Destroyed()
	    {
	        base.Destroyed();
            _locationService?.Stop();
            IsLocationLauch = false;
	        if (_locationService != null)
                _locationService.LocationChanged -= LocationServiceOnLocationChanged;
	    }

	    public bool IsLocationLauch { get; set; }

        private void LocationServiceOnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            CurrentLocation = args;
        }
    }

	public class VetMapParameters {
		public KVet SelectedVet { get; set; }

	}
}