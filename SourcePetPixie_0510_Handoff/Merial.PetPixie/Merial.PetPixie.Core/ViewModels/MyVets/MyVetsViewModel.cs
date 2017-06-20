using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.EventHandler;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels {
	public class MyVetsViewModel : ViewModelBase {
        
		private List<VetModel> _vets;
        private List<KVet> _vetSearchResults;


		private bool _redirect;
        private bool _isMenuVisible = false;

           private ILocationService _locationService;
        private LocationChangedEventArgs _currentLocation;
        private readonly IVetService _vetService;
                
        #region Constructors
        //public MyVetsViewModel(IVetService vetService, ILocationService locationService)
        public MyVetsViewModel(IVetService vetService)  //DS What happened to the Location service
        {
            _vetService = vetService;
           // _locationService = locationService;
        }

        #endregion



		#region Properties
		public List<VetModel> Vets {
			get { 
                return _vets; 
            }
			set {
				_vets = value;
				RaisePropertyChanged(() => Vets);
			}
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


        private VetModel _selectedItem = null;
        public VetModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

               // SomeCommand.Execute(_selectedItem);


                ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters
                {
                   //ds SelectedVet = Settings.CurrentUserProfile.Vets.FirstOrDefault(v => v.Id.Equals(_selectedItem.Id))
                     SelectedVet = VetSearchResults.FirstOrDefault(v => v.Id.Equals(_selectedItem.Id))
                                           
                });

                SelectedItem = null;
            }
        }

                
        public bool IsMenuVisibile
        {
            get { return _isMenuVisible; }
            set
            {
                _isMenuVisible = value;
                RaisePropertyChanged(() => IsMenuVisibile);
            }
        }
		#endregion

		#region Commands
		public IMvxCommand FindVetsCommand => new SafeMvxCommand(OnFindVets);


		public IMvxCommand VetSelectedCommand => new SafeMvxCommand<VetModel>(OnVetSelected);

		#region Actions
		private void OnFindVets() {
			ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters {SelectedVet = null});
		}
		private void OnVetSelected(VetModel vet) {
			ShowViewModel<MyVetInfoViewModel, VetInfoParameters>(new VetInfoParameters {
				SelectedVet = Settings.CurrentUserProfile.Vets.FirstOrDefault(v => v.Id.Equals(vet.Id))
                // SelectedVet = Vets.FirstOrDefault(v => v.Id.Equals(vet.Id))
			});
		}
		#endregion
		#endregion

		protected override Task<bool> LoadDataAsync() {
		Vets = new List<VetModel>(Settings.CurrentUserProfile.Vets.Select(VetModel.CreateFrom));






            //			if ((Vets == null || !Vets.Any()) && _redirect) {
            //				_redirect = false;
            //				ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters { SelectedVet = null });
            //			}
            MessagingCenter.Send(this, "StopLoading");
			return base.LoadDataAsync();
		}



        //public async Task<List<KVet>> SearchByLatLong(double lat, double lng, bool near)
        //{
        //   VetSearchResults = await _vetService.GetByLatLng(lat, lng, 10, false);

        //   Vets = new List<VetModel>();
        //    foreach (KVet vet in VetSearchResults.Take(3))
        //    {
        //        var vetModel = new VetModel() { Acl = vet.Acl, Address = vet.Address, GeoLoc = vet.Geoloc, Id = vet.Id, Kmd = vet.Kmd, Name = vet.Name };
        //        Vets.Add(vetModel);

             
        //    }

        //    return VetSearchResults;
        //}

		public async void Init(bool redirect) {
			_redirect = redirect;

            //How does the current app trigger the load data
          //  if (redirect == false)
            {

           //     double lat = 25.9564812;
            //    double lng = -80.1392121;

               // await SearchByLatLong(lat, lng,true);
                LoadDataAsync();

            }
		}
	}
}