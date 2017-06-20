using System.Collections.Generic;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Email;
using MvvmCross.Plugins.PhoneCall;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.ViewModels
{
	public class MyVetInfoViewModel : ViewModelBase<VetInfoParameters> {

        #region Fields

        private readonly IProfileService _profileService;
		private readonly IUserService _userService;
		private KVet _selectedVet;
		private string _name = "";
		private string _address = "";
		private string _phone = "";
		private string _email = "";
		private string _website = "";
        private bool _isInMyList = false;
        private bool _isNotInMyList = true;

        #endregion

        #region Public Constructors

        public MyVetInfoViewModel(IProfileService profileService, IUserService userService) {
			_profileService = profileService;
			_userService = userService;
		}

        #endregion

        #region Properties

        public string Name {
			get { return _name; }
			set { _name = value; RaisePropertyChanged(() => Name); }
		}

		public string Address {
			get { return _address; }
			set { _address = value; RaisePropertyChanged(() => Address); }
		}

		public string Phone {
			get { return _phone; }
			set { _phone = value; RaisePropertyChanged(() => Phone); }
		}

		public string Email {
			get { return _email; }
			set { _email = value; RaisePropertyChanged(() => Email); }
		}

		public string Website {
			get { return _website; }
			set { _website = value; RaisePropertyChanged(() => Website); }
		}

		public bool IsInMyList {
			get { return _isInMyList; }
			set {
				_isInMyList = value;
                _isNotInMyList = !value;
				RaisePropertyChanged(() => IsInMyList);
			}
		}

        public bool IsNotInMyList {
            get { return _isNotInMyList; }
            set {
                _isNotInMyList = value;
                RaisePropertyChanged(() => IsNotInMyList);
            }
        }



		public KVet SelectedVet {
			get { return _selectedVet; }
			set { _selectedVet = value; RaisePropertyChanged(() => SelectedVet); }
		}

        #endregion

        #region Life Cycle Methods

        protected override void RealInit(VetInfoParameters vet) {
			Name = vet.SelectedVet.Name;
			Address = vet.SelectedVet.Address;
			Phone = vet.SelectedVet.PhoneNumber;
			Email = vet.SelectedVet.Email;
			Website = vet.SelectedVet.Website;
			SelectedVet = vet.SelectedVet;
			IsInMyList = Settings.CurrentUserProfile.VetIds.Any(v => v.Equals(vet.SelectedVet.Id));
            IsNotInMyList = !IsInMyList;
		}

        #endregion

        #region Commands

        public IMvxCommand AddAsMyVetCommand => new SafeMvxCommandAsync(AddAsMyVetCommandExecute);
		public IMvxCommand RemoveAsMyVetCommand => new SafeMvxCommandAsync(RemoveAsMyVetCommandExecute);
		public IMvxCommand CallPhoneVetCommand => new SafeMvxCommand(OnCallPhoneVet);
		public IMvxCommand EmailVetCommand => new SafeMvxCommand(OnEmailVet);
		public IMvxCommand NavigateToVetCommand => new SafeMvxCommand(OnNavigateToVet);
        public IMvxCommand FindVetsCommand => new SafeMvxCommand(OnFindVets);

    
              
        private void OnFindVets() {
            ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters {SelectedVet = null});
        }

        private void OnNavigateToVet() {
			var navigationService = Mvx.Resolve<INavigationService>();
			navigationService.NavigateToAddress(Address);
		}

		private void OnEmailVet() {
			var mvxComposeEmailTask = Mvx.Resolve<IMvxComposeEmailTask>();
			mvxComposeEmailTask.ComposeEmail(Email);
		}

		private void OnCallPhoneVet()
        {
            var mvxPhoneCallTask = Mvx.Resolve<IMvxPhoneCallTask>();
			mvxPhoneCallTask.MakePhoneCall(Name, Phone);
		}

		private async Task RemoveAsMyVetCommandExecute()
        {
            IsFetchingData = true;
            var currentProfile = Settings.CurrentUserProfile;
            if (!(currentProfile.Vets == null || currentProfile.Vets.Length == 0))
            {
                var vets = currentProfile.Vets.ToList();
                vets.RemoveAll(k => k.Id.Equals(SelectedVet.Id));
                currentProfile.Vets = vets.ToArray();
            }
            if (!(currentProfile.VetIds == null || currentProfile.VetIds.Length == 0))
            {
                var vetids = currentProfile.VetIds.ToList();
                vetids.Remove(SelectedVet.Id);
                currentProfile.VetIds = vetids.ToArray();
            }

            await Task.WhenAll(
                _profileService.EditProfile(currentProfile),
                _profileService.RemoveRateVet(SelectedVet.Id));

            IsInMyList = false;
            IsFetchingData = false;
        }

		private async Task AddAsMyVetCommandExecute() {
			if (IsMaxVetReach())
				return;
			IsFetchingData = true;
			var currentProfile = Settings.CurrentUserProfile;
			var vets = currentProfile.Vets != null ? currentProfile.Vets.ToList() : new List<KVet>();
			vets.Add(SelectedVet);
			currentProfile.Vets = vets.ToArray();
			var vetids = currentProfile.VetIds != null ? currentProfile.VetIds.ToList() : new List<string>();
			vetids.Add(SelectedVet.Id);
			currentProfile.VetIds = vetids.ToArray();
			await _profileService.EditProfile(currentProfile);
			IsInMyList = true;
			IsFetchingData = false;
			ShowViewModel<MyVetsViewModel>();
		}

        #endregion

        #region Methods and Operators

        public bool IsMaxVetReach() {
			var currentProfile = Settings.CurrentUserProfile;
			if (currentProfile.Vets.Length == 4) {
				Mvx.Resolve<IPopupService>().DisplayMessage("You can only add 2 vet to your list.", "Maximum vet reach");
				return true;
			}
			return false;
		}

        #endregion
    }

	public class VetInfoParameters {
		public KVet SelectedVet { get; set; }
        //public VetModel SelectedVet { get; set; }

	}
}