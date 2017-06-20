using System.Threading.Tasks;

using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;
using UIKit;

namespace Merial.PetPixie.iOS.Services
{
	
	public class PopupService : IPopupService
    {
		
		private const string WorkInProgressTitle = "Work in progress";
		private const string WorkInProgressDefaultMessage = "This features is not yet available";


		//Not needed in iOS
		public void DisplayMessageAsToast(string message) {
			//var toast = Toast.MakeText(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity,
			//		message, ToastLength.Long);
			//toast.Show();
		}


        public Task DisplayMessageAsync(string title, string message)
        {
            var tcs = new TaskCompletionSource<bool>();
			var alert = new UIAlertView(); 
			alert.Title = title;
			alert.Message = message;
			alert.AddButton("OK");

			//Make the call thread-safe.
			Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction((alert.Show));
			alert.Clicked += (s, a) => { tcs.SetResult(true); };
            return tcs.Task;
        }

		public void DisplayMessage(string message, string title)
	    {
			UIAlertView alert = new UIAlertView();
			alert.Title = title;
			alert.Message = message;
			alert.AddButton("OK");

			//Make the call thread-safe.
			Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction((alert.Show));
        }

		public Task<bool> DisplayYesNoMessage(string title, string question, string actionYes, string actionNo)
		{
			var tcs = new TaskCompletionSource<bool>(); 
			var alert = new UIAlertView();
			alert.Title = title;
			alert.Message = question;
			alert.AddButton("Yes");
			alert.AddButton("No");

			//Make the call thread-safe.
			Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction((alert.Show));
			alert.Clicked+=(s,e)=>
			{
				if (e.ButtonIndex == 0)
				{
					tcs.SetResult(true);
				}
				else {
					tcs.SetResult(false);
				}
			};

            return tcs.Task;
        }


	    public Task DisplayWorkInProgressAsync(string message = null) {
			var tcs = new TaskCompletionSource<bool>();

			var alert = new UIAlertView();
			alert.Title = WorkInProgressTitle;
			alert.Message = message ?? WorkInProgressDefaultMessage;
			alert.AddButton("OK");

			//Make the call thread-safe.
			Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction(alert.Show);
			alert.Clicked += (s, e) => { tcs.SetResult(true);};
			return tcs.Task;
		}
	}
}