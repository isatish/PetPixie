using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform.iOS.Platform;

namespace Merial.PetPixie.iOS.Services {
	public class NavigationService : MvxIosTask, INavigationService {
		public void NavigateToAddress(string address) {
			//var intent = new Intent(Intent.ActionView, Uri.Parse("http://maps.google.co.in/maps?q=" + address));
			//StartActivity(intent);
		}
	}
}