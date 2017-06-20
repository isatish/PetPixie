using System;
using MvvmCross.iOS.Views;
using Merial.PetPixie.Core;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using UIKit;
using System.Diagnostics;
//using Facebook.LoginKit;
//using Facebook.CoreKit;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using System.CodeDom.Compiler;
using Foundation;
using System.ComponentModel;
using Xamarin.Auth;

namespace Merial.PetPixie.iOS.Views
{
	public abstract class AuthenticationView<TViewModel> : MvxViewController<TViewModel> where TViewModel : ValidationFormViewModelBase, IAuthenticationViewModel, IMvxViewModel
	{
		public AuthenticationView(IntPtr handle) : base (handle)
		{
		}


        public AuthenticationView() 
        {
        }
		#region Fields
		string[] Permssions = new string[]{"public_profile","email"};
		private Account twitterAccount;
		#endregion

		#region UIViewController Methods




		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			DoViewDidLoad();


			//var x = this.Storyboard.InstantiateViewController("SignupView") as MvxViewController;
			//signup_facebook_button.LoginBehavior = LoginBehavior.Browser;
			//signup_facebook_button.ReadPermissions = readPermissions.ToArray(); 
			//TwitterLogin();
			//FacebookLogin();
		}

		protected abstract void DoViewDidLoad();


		public void OnCancel()
		{
			//  throw new NotImplementedException();
		}


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
				ViewModel.Started();
			}
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();
		}

		protected virtual void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}

		public override void ViewWillDisappear(bool animated)
		{
			
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
				ViewModel.Paused();
			}
			base.ViewWillDisappear(animated);
		}


		#endregion

		#region Private Methods

		public void TwitterLogin(UIButton btnTwitter)
		{
			var auth = new OAuth1Authenticator(
				consumerKey: Constants.Twitter.ConsumerKey,
				consumerSecret: Constants.Twitter.ConsumerSecret,
				requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
				authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
				accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
				callbackUrl: new Uri("http://merial.com")
				);
			auth.AllowCancel = true;
			auth.Completed += twitter_auth_Completed;

			btnTwitter.TouchDown += (s, e) =>
			{
			 	PresentViewController(auth.GetUI(),true,null);
			};

		}

		private void twitter_auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
		{
			DismissViewController(true, null);
			if (eventArgs.IsAuthenticated)
			{
				ViewModel.IsTwitterLoading = true;

				twitterAccount = eventArgs.Account;

				AccountStore.Create().Save(twitterAccount, "Twitter");
				string userId = eventArgs.Account.Properties["user_id"];
				string oauth_token = eventArgs.Account.Properties["oauth_token"];
				string oauth_token_secret = eventArgs.Account.Properties["oauth_token_secret"];

				foreach (var properties in eventArgs.Account.Properties)
					Debug.WriteLine($"TWITTER > {properties.Key} ", properties.Value);

				this.ViewModel.LoginWithTwitterCommand.Execute(new Tuple<string, string, string>(userId, oauth_token, oauth_token_secret));

			}
			else 
			{
				Debug.WriteLine("Not Authenticated");
			}
		}

		public void FacebookLogin(UIButton btnFacebook)
		{
			//Missing Permissions to read email

			//btnFacebook.TouchDown += (s, e) => {

			//	LoginManager fbLoginManager = new  LoginManager();
			//	fbLoginManager.LoginBehavior = LoginBehavior.Native;
			//	fbLoginManager.LogInWithReadPermissions(Permssions, this,(result, error) => {
			//		if (error != null)
			//		{
			//			new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
			//			return;
			//		}

			//		// Handle if the user cancelled the request
			//		if (result.IsCancelled)
			//		{
			//			new UIAlertView("The request was cancelled", "If you are using a Test App Id, please, make sure that your account have an Administrator role at:\rhttps://developers.facebook.com/apps/appid/roles/", null, "OK", null).Show();
			//			return;
			//		}

			//		// Do your magic if the request was successful
			//		SignWithFacebook();

			//		;});

			//};
		}


		#endregion

		#region Authentication Overrided Methods

		private void SignWithFacebook()
		{

		//ds	this.ViewModel.SignupWithFacebookCommand.Execute(
		//ds		new Tuple<string, string>(AccessToken.CurrentAccessToken.UserID, AccessToken.CurrentAccessToken.TokenString)); 
		}

		#endregion





	}
}

