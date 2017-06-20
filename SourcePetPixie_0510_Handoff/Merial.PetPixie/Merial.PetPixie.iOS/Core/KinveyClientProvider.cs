using Foundation;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using SQLite.Net.Platform.XamarinIOS;

namespace Merial.PetPixie.iOS.Core {
	public class KinveyClientProvider : IKinveyClientProvider {
		public void CreateClient() {
			var service = Mvx.Resolve<IKinveyService>();
			//service.CreateClient(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), new SQLitePlatformIOS());
			service.CreateClient(NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].ToString(), new SQLitePlatformIOS());
		}
	}
}