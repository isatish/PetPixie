using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmValidation;

namespace Merial.PetPixie.Core.ViewModels
{
    public class SignupViewModel : AuthenticationViewModelBase // ValidationFormViewModelBase, IAuthenticationViewModel
    {
        #region Fields

        private readonly IUserService _userService;
	   
        private readonly IProfileService _profileService;
        private readonly IKinveyService _kinveyService;
        private readonly IDeviceHelper _deviceHelper;

		private string _username;
        #endregion

        #region Constructors

        public SignupViewModel(IUserService userService,IProfileService profileService, IKinveyService kinveyService, IDeviceHelper deviceInfoService) : base(userService, kinveyService, profileService)
        {
            _userService = userService;
            _profileService = profileService;
            _kinveyService = kinveyService;
            _deviceHelper = deviceInfoService;
            Settings.Email = null;
            Settings.Password = null;
            Settings.CurrentUser = null;

            App.PostSuccessFacebookSignupAction += (facebookUserID, facebookToken) =>
            {
                SignupWithFacebook(new Tuple<string, string>(facebookUserID, facebookToken));
            };
        }

        #endregion

        #region Commands

  //      public IMvxCommand SignupWithEmailCommand => new SafeMvxCommandAsync(async ()=> await EnsureIsConnected(SignupWithEmail));
  //      public IMvxCommand SignupWithFacebookCommand => new SafeMvxCommandAsync<Tuple<string,string>>(SignupWithFacebook);
  //      public IMvxCommand SignupWithTwitterCommand => new SafeMvxCommandAsync<Tuple<string,string,string>>(SignupWithTwitter);
		public IMvxCommand LoginCommand => new SafeMvxCommand(LogIn);
		//public IMvxCommand CloseScreen => new SafeMvxCommand(() => Close(this));

		#endregion

		#region Events

		public event EventHandler LoggedIn;

        #endregion

        #region Public Properties

  //      public string Email { get; set; }

		//public string Username
		//{
		//	get
		//	{
		//		return _username;
		//	}
		//	set
		//	{
		//		this.RaiseAndSetIfChanged(ref _username, value);
		//	}
		//}

		//public string Password { get; set; }

		//public string ConfirmPassword { get; set; }

        private bool _isTwitterLoading;

        public bool IsTwitterLoading
        {
            get { return _isTwitterLoading; }
            set
            {
                _isTwitterLoading = value;
                RaisePropertyChanged(() => IsTwitterLoading);
            }
        }

        private bool _isFacebookLoading;

        public bool IsFacebookLoading
        {
            get { return _isFacebookLoading; }
            set
            {
                _isFacebookLoading = value;
                RaisePropertyChanged(() => IsFacebookLoading);
            }
        }

        #endregion

        #region Methods
		private void LogIn()
		{
			ShowViewModel<LoginViewModel>();
		}
   //     public async Task SignupWithEmail() {
            
   //         _deviceHelper.HideKeyBoard();
   //         IsLoading = true;


   //         if (!Validate()) {
   //             IsLoading = false;
   //             return;
   //         }

                         
   //         Mvx.Resolve<IUserDialogs>().ShowLoading("Creating your Pet+Pixie account");


   //         var emailExist = await _userService.EmailExistAsync(Email);
   //         if (emailExist) {
   //             IsLoading = false;
   //             if (await PopupService.DisplayYesNoMessage("Error",
   //                 "The user with provided email is already created but not yet have " +
   //                 "a password. Do you want to get a password created and sent to your email?", "Ok", "Cancel"))
   //             {
   //                 if (await _userService.SendResetPasswordInstructionsAsync(Email))
   //                 {
   //                     await PopupService.DisplayMessageAsync("Confirmation","An email with your new password was sent to your email." +
   //                                                 " Please use the password to login.");
   //                 }
   //                 else
   //                 {
   //                     await PopupService.DisplayMessageAsync("Error", "An error occured while sending reset password instructions.");
   //                 }
   //             }
   //             return;
   //         }
   //          Mvx.Resolve<IUserDialogs>().HideLoading();
   //         ShowViewModel<CompleteAccountViewModel>(new { email = Email,username= Username,password=Password, confirmP =ConfirmPassword});
   //         IsLoading = false;
           
   //     }

   //     public async Task SignupWithFacebook(Tuple<string, string> facebookIds)
   //     {
   //         //this.IsFacebookLoading = true;
			//IsLoading = true;

   //         string userId = $"MerialFacebook{facebookIds.Item1}";
   //         string token  = facebookIds.Item2;

   //         try
   //         {
   //             await _userService.LoginAutomatedAsync(userId, token);
   //             var newProfileId = this._userService.GetProfileId();
   //             var newProfile = await this._profileService.GetProfileById(newProfileId);

   //             // Si l'utilisateur existe déjà en base
   //             if (newProfile.profileCompleteVersion >= Constants.Profile.AccountCompletedProfileLevel)
   //             {
   //                 Settings.Password = token;
   //                 Settings.Email = newProfile.Email;
   //                 Settings.CurrentUser = _kinveyService.GetClient().CurrentUser;
   //                 Settings.CurrentUserProfile = newProfile;

   //                 this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

   //                 var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
			//		ShowViewModel<MainViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
   //             } 
   //             // Sinon, on lui fait compléter ses informations
   //             else
   //             {
   //                 ShowViewModel<CompleteAccountViewModel>();
   //             }
   //         }
   //         catch (Exception e)
   //         {
   //             Logger.Error(e);
   //             PopupService.DisplayMessage("An unexpected error occured. Please try again","Error");
   //             //this.IsFacebookLoading = false;
			//	IsLoading = false;
   //             return;
   //         }
            
   //         //this.IsFacebookLoading = false;
			//IsLoading = false;
   //     }

   //     public async Task SignupWithTwitter(Tuple<string, string, string> twitterIds) {
   //         //IsTwitterLoading = true;
			//IsLoading = true;

   //         string userId = twitterIds.Item1;
   //         string oauth_token = twitterIds.Item2;
   //         string oauth_token_secret = twitterIds.Item3;

   //         var password = oauth_token + ";" + oauth_token_secret;
   //         var username = "MerialTwitter" + userId;

   //         try
   //         {
   //             await _userService.LoginAutomatedAsync(username, password);
   //             var newProfileId = this._userService.GetProfileId();
   //             var newProfile = await this._profileService.GetProfileById(newProfileId);

   //             // Si l'utilisateur existe déjà en base
   //             if (newProfile.profileCompleteVersion >= Constants.Profile.AccountCompletedProfileLevel)
   //             {
   //                 Settings.Password = password;
   //                 Settings.Email = newProfile.Email;
   //                 Settings.CurrentUser = _kinveyService.GetClient().CurrentUser;
   //                 Settings.CurrentUserProfile = newProfile;

   //                 this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

   //                 var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
			//		ShowViewModel<MainViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
   //             }
   //             // Sinon, on lui fait compléter ses informations
   //             else
   //             {
   //                 ShowViewModel<CompleteAccountViewModel>();
   //             }
   //         }
   //         catch (Exception e)
   //         {
   //             Logger.Error(e);
   //             PopupService.DisplayMessage("An unexpected error occured. Please try again","Error");
   //             //IsTwitterLoading = false;
			//	IsLoading = false;

   //             return;
   //         }
            
   //         //IsTwitterLoading = false;
			//IsLoading = false;
   //     }

        public override void AddValidationFields(ValidationHelper helper) {
            helper.AddRequiredRule((() => Email), "Email is required");
            helper.AddRule(() => Email, () => RuleResult.Assert(InputValidationHelper.ValidateEmail(Email), "Invalid email"));
			helper.AddRequiredRule((() => Username), "Required");
			helper.AddRule(() => Username, () => RuleResult.Assert(Username.Length >= 4, "Minimun 4 charaters"));;
			helper.AddRequiredRule((() => Password), "Required");
			helper.AddRule(() => Password, () => RuleResult.Assert(Password.Length >= 8, "Minimum 8 characters"));
			helper.AddRequiredRule((() => ConfirmPassword), "Required");
			helper.AddRule(() => ConfirmPassword, () => RuleResult.Assert(Password == ConfirmPassword, "Not matching"));
		}

        #endregion
    }
}