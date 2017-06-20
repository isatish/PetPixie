using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels {
	public class VetMapSearchAddressViewModel : ViewModelBase<VetResultsParameters> {
		#region Proprietes
		private string _name;
		public string Name {
			get { return _name; }
			set { _name = value; RaisePropertyChanged(() => Name); }
		}

		private string _address;
		public string Address {
			get { return _address; }
			set { _address = value; RaisePropertyChanged(() => Address); }
		}

		private ObservableCollection<KVet> _filteredVetListItems;
		public ObservableCollection<KVet> FilteredVetListItems {
			get { return _filteredVetListItems; }
			set { _filteredVetListItems = value; RaisePropertyChanged(() => FilteredVetListItems); }
		}

		private ObservableCollection<KVet> _vetListItems;
		public ObservableCollection<KVet> VetListItems {
			get { return _vetListItems; }
			set { _vetListItems = value; RaisePropertyChanged(() => VetListItems); }
		}

		#endregion

		protected override void RealInit(VetResultsParameters vetResultsParameters) {
			VetListItems = new ObservableCollection<KVet>();
			if (vetResultsParameters.VetList != null)
				foreach (var vet in vetResultsParameters.VetList) {
					VetListItems.Add(vet);
				}
			FilteredVetListItems = VetListItems;
		}
        
		public ICommand SelectedVetCommand {
			get {
				return 
                    new SafeMvxCommand<KVet>(item => {
						ShowViewModel<VetMapViewModel, VetMapParameters>(new VetMapParameters { SelectedVet = item },
							new MvxBundle(new Dictionary<string, string> {
								{Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearTop}
						 }));
					});
			}
		}

	}

	public class VetResultsParameters {
		public List<KVet> VetList { get; set; }
	}
}