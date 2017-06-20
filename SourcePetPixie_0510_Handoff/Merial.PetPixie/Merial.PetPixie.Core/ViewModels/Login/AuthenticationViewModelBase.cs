using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Acr.UserDialogs;
//using Facebook.CoreKit;
//using Facebook.LoginKit;
//using Xamarin.Auth;


//using Merial.PetPixie.Core.ViewModels.Merial.PetPixie.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class AuthenticationViewModelBase : ValidationFormViewModelBase, IAuthenticationViewModel
    {
        #region Fields

        private readonly IKinveyService _kinveyService;
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;



        private bool _isTwitterLoading;
        private bool _isFacebookLoading;
        private bool _showTour;

        private bool _flag = false;   
        private string _email = "";
        private string _password = "";
        private string _confirmPassword = "";
        private string _username = "";

        string[] Permssions = new string[] { "public_profile", "email" };


        #endregion

        #region Constructors

        public AuthenticationViewModelBase(IUserService userService, IKinveyService kinveyService, IProfileService profileService)
        {
            _userService = userService;
            _kinveyService = kinveyService;
            _profileService = profileService;
            IsLoading = false;

            App.PostSuccessFacebookLoginAction += (facebookUserID, facebookToken ) =>
            {
                LoginWithFacebook(new Tuple<string, string>(facebookUserID, facebookToken));
            };







#if DEBUG
            //Email = "seRgio.cArrillo.carranza@gmail.com";
            //Password = "1234567";
            //Email = "sergio.carrillo@merial.com";
            //Password = "aaaa1234";

#endif
#if STAGING
            //		Email = "s1@test.com";
            //		Password = "123456789";
#endif

            //Email = "chicksabcs@gmail.com";
            //Password = "cowboy43";


            //Email = "sergio.carrillo@merial.com";
            //Password = "aaaa1234";
        }

        #endregion

        #region Events

        public event EventHandler LoggedIn;

        #endregion

        #region Commands

        //public IMvxCommand LoginCommand => new SafeMvxCommandAsync(async () => await EnsureIsConnected(Login));
         public IMvxCommand LoginCommand => new SafeMvxCommandAsync(async () => await EnsureIsConnected(LoginWithEmail));
        public IMvxCommand SignUpCommand => new SafeMvxCommand(SignUp);
        public IMvxCommand SignUpWithEmailCommand   => new SafeMvxCommandAsync(SignupWithEmail);
        public IMvxCommand LoginWithFacebookCommand => new SafeMvxCommandAsync<Tuple<string, string>>(LoginWithFacebook);
        public IMvxCommand LoginWithTwitterCommand => new SafeMvxCommandAsync<Tuple<string, string, string>>(LoginWithTwitter);
        public IMvxCommand ForgetPasswordCommand => new SafeMvxCommand(ForgetPassword);


        #endregion

        #region Properties

        public bool IsTwitterLoading
        {
            get { return _isTwitterLoading; }
            set
            {
                _isTwitterLoading = value;
                RaisePropertyChanged(() => IsTwitterLoading);
            }
        }

        public bool IsFacebookLoading
        {
            get { return _isFacebookLoading; }
            set
            {
                _isFacebookLoading = value;
                RaisePropertyChanged(() => IsFacebookLoading);
            }
        }

        public bool ShowTour
        {
            get { return _showTour; }
            set
            {
                _showTour = value;
                RaisePropertyChanged(() => ShowTour);
            }
        }



        public bool Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                RaisePropertyChanged(() => Flag);
            }
        }


        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }
        

 

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

                
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }

               
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }



                //public string Password { get; set; }

        //public string ConfirmPassword { get; set; }


         
        #endregion

        #region Methods

        public override void Start()
        {
            base.Start();
        }

        private void ForgetPassword()
        {
            ShowViewModel<ForgetPasswordViewModel>();
        }

        private void SignUp()
        {
            //back to previous page
            ShowViewModel<SignupViewModel>();
        }




                public async Task ShowValidationErrors()
                {
                        var validationErrors = ValidationErrorMessages();
                        var vals = Errors;
                        string valError = "";
                        foreach (var error in Errors)
                        {
                                if (error.Value.ToLower() == "required")
                                {
                                        valError += error.Key + " is required";
                                }
                                else
                                {
                                        valError += error.Value;
                                }
                                valError += System.Environment.NewLine;
                        }
                        await PopupService.DisplayMessageAsync("Please correct the following", System.Environment.NewLine + valError);

                }


        public async Task SignupWithEmail()
        {

         //ds   _deviceHelper.HideKeyBoard();
            IsLoading = true;


            if (!Validate())
            {
                IsLoading = false;
                  ShowValidationErrors();
                return;
            }


            Mvx.Resolve<IUserDialogs>().ShowLoading("Creating your Pet+Pixie account");


            var emailExist = await _userService.EmailExistAsync(Email);
            if (emailExist)
            {
                IsLoading = false;
                if (await PopupService.DisplayYesNoMessage("Error",
                    "The user with provided email is already created but not yet have " +
                    "a password. Do you want to get a password created and sent to your email?", "Ok", "Cancel"))
                {
                    if (await _userService.SendResetPasswordInstructionsAsync(Email))
                    {
                        await PopupService.DisplayMessageAsync("Confirmation", "An email with your new password was sent to your email." +
                                                    " Please use the password to login.");
                    }
                    else
                    {
                        await PopupService.DisplayMessageAsync("Error", "An error occured while sending reset password instructions.");
                    }
                }
                return;
            }
            Mvx.Resolve<IUserDialogs>().HideLoading();
            ShowViewModel<CompleteAccountViewModel>(new { email = Email, username = Username, password = Password, confirmP = ConfirmPassword });
            IsLoading = false;

        }


                public async Task LoginWithEmail()
                {
                        try
                        {
                                IsLoading = true;
                                Mvx.Resolve<IUserDialogs>().ShowLoading("Logging into Pet+Pixie");
                                //ds var loginCredentials = new Tuple<string, string>(Email.ToLower(), Password.ToLower());
                                var loginCredentials = new Tuple<string, string>(Email.ToLower(), Password.ToLower());
                                await LoginToAppServices(loginCredentials, false);

                                Settings.Email = Email;
                                Settings.Password = Password;

                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");

                                IsLoading = false;
                                return;
                        }

                        IsLoading = false;
                }

                public async Task LoginWithFacebook(Tuple<string, string> facebookIds)
                {
                        //this.IsFacebookLoading = true;
                        IsLoading = true;


                        try
                        {

                                Mvx.Resolve<IUserDialogs>().ShowLoading("Logging into application");

                                string userId = $"MerialFacebook{facebookIds.Item1}";
                                string token = facebookIds.Item2;

                                var loginCredentials = new Tuple<string, string>(userId, token);

                                await LoginToAppServices(loginCredentials, true );

                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                                //this.IsFacebookLoading = false;
                                IsLoading = false;
                                return;
                        }

                        //this.IsFacebookLoading = false;
                        IsLoading = false;
                }


                public async Task LoginWithTwitter(Tuple<string, string, string> info)
                {
                        try
                        {
                                //IsTwitterLoading = true;
                                IsLoading = true;

                                string userId = info.Item1;
                                string oauth_token = info.Item2;
                                string oauth_token_secret = info.Item3;

                                var password = oauth_token + ";" + oauth_token_secret;
                                var username = Constants.Connection.TwitterUsername + userId;


                                var loginCredentials = new Tuple<string, string>(username, password);
                                await LoginToAppServices(loginCredentials, true );

                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                                //this.IsFacebookLoading = false;
                                IsLoading = false;
                                return;
                        }

                        //this.IsFacebookLoading = false;
                        IsLoading = false;
                }




                public async Task SignupWithFacebook(Tuple<string, string> facebookIds)
                {
                        //this.IsFacebookLoading = true;
                        IsLoading = true;

                        string userId = $"MerialFacebook{facebookIds.Item1}";
                        string token = facebookIds.Item2;
                        Mvx.Resolve<IUserDialogs>().ShowLoading("Creating Pet+Pixie account....");
                        try
                        {
                                await _userService.LoginAutomatedAsync(userId, token);
                                var newProfileId = this._userService.GetProfileId();
                                var newProfile = await this._profileService.GetProfileById(newProfileId);

                                // If the user already exists in the database
                                if (newProfile.profileCompleteVersion >= Constants.Profile.AccountCompletedProfileLevel)
                                {
                                        Settings.Password = token;
                                        Settings.Email = newProfile.Email;
                                        Settings.CurrentUser = _kinveyService.GetClient().CurrentUser;
                                        Settings.CurrentUserProfile = newProfile;

                                        //dsAustin
                                        // await _profileService.EditProfile(Profile.UpdateKProfile(Settings.CurrentUserProfile), true);

                                        this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

                                        var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
                                        //dsShowViewModel<MainViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
                                        ShowViewModel<FeedViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
                                }
                                // Otherwise, we have him complete his informations
                                else
                                {
                                        ShowViewModel<CompleteAccountViewModel>();
                                }
                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                                //this.IsFacebookLoading = false;
                                IsLoading = false;
                                return;
                        }

                        //this.IsFacebookLoading = false;
                        IsLoading = false;
                }

                public async Task LoginToAppServices(Tuple<string, string> userCredentials, bool isOAuth)
                {

                        string userName = userCredentials.Item1;
                        string password = userCredentials.Item2;

                        if (!isOAuth)
                        {
                                if (!Validate())
                                {
                                        await ShowValidationErrors();
                                        return;
                                }
                        }

                        IsLoading = true;

                        User user = null;
                        try
                        {
                                var isConnected = _userService.UserConnected();

                                if (!isConnected)
                                {
                                        Mvx.Resolve<IUserDialogs>().ShowLoading("Logging in....");
                                        //ds        Email = Email.ToLower();
                                        user = await _userService.LoginAutomatedAsync(userName, password);
                                        Settings.Email = Email;
                                        Settings.Password = Password;
                                        Settings.CurrentUser = user;
                                        var currentProfileId = _userService.GetProfileId();
                                        await _profileService.ReloadProfile(currentProfileId);

                                }

                                IsLoading = false;
                                this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);
                                var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
                                ShowViewModel<FeedViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                IsLoading = false;
                                PopupService.DisplayMessage("Invalid email or password", "Error");
                        }

                }
    
            
        #endregion

        

        #region "ORIGINAL METHODS"


        public async Task Login()
        {
            if (!Validate())
                return;

            IsLoading = true;

            User user = null;
            try
            {
                var isConnected = _userService.UserConnected();

                if (!isConnected)
                {
                    Mvx.Resolve<IUserDialogs>().ShowLoading("Logging in....");
                    Email = Email.ToLower();
                    user = await _userService.LoginAutomatedAsync(Email, Password);
                    Settings.Email = Email;
                    Settings.Password = Password;
                    Settings.CurrentUser = user;
                    var currentProfileId = _userService.GetProfileId();
                    await _profileService.ReloadProfile(currentProfileId);

                }

                IsLoading = false;
                this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);
                var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
                ShowViewModel<FeedViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                IsLoading = false;
                PopupService.DisplayMessage("Invalid email or password", "Error");
            }
        }


        public async Task SignupWithTwitter(Tuple<string, string, string> info)
        {
            //IsTwitterLoading = true;
			IsLoading = true;

            string userId = info.Item1;
            string oauth_token = info.Item2;
            string oauth_token_secret = info.Item3;

            var password = oauth_token + ";" + oauth_token_secret;
            var username = Constants.Connection.TwitterUsername + userId;

            try
            {
                await _userService.LoginAutomatedAsync(username, password);

                var profileId = this._userService.GetProfileId();
                var profile = await this._profileService.GetProfileById(profileId);
               
                // Si l'utilisateur existe déjà en base
                if (profile.profileCompleteVersion >= Constants.Profile.AccountCompletedProfileLevel)
                {
                    Settings.Password = password;
                    Settings.Email = profile.Email;
                    Settings.CurrentUser = _kinveyService.GetClient().CurrentUser;
                    Settings.CurrentUserProfile = profile;

                    this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

                    var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
					ShowViewModel<MainViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
                }
                // Sinon, on lui fait compléter ses informations
                else
                {
                    ShowViewModel<CompleteAccountViewModel>();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                PopupService.DisplayMessage("An unexpected error occured. Please try again","Error");
                //IsTwitterLoading = false;
				IsLoading = false;
                return;
            }
            //IsTwitterLoading = false;
			IsLoading = false;
        }

                    
        public async Task SigninWithFacebook(Tuple<string, string> facebookIds)
        {
            //this.IsFacebookLoading = true;
            IsLoading = true;

             //dsloading Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed");

            string userId = $"MerialFacebook{facebookIds.Item1}";
            string token  = facebookIds.Item2;

            try
            {


                await _userService.LoginAutomatedAsync(userId, token);
                var newProfileId = this._userService.GetProfileId();
                var newProfile = await this._profileService.GetProfileById(newProfileId);

                // If the user already exists in the database
                if (newProfile.profileCompleteVersion >= Constants.Profile.AccountCompletedProfileLevel)
                {
                    Settings.Password = token;
                    Settings.Email = newProfile.Email;
                    Settings.CurrentUser = _kinveyService.GetClient().CurrentUser;
                    Settings.CurrentUserProfile = newProfile;

                    this.LoggedIn?.BeginInvoke(this, EventArgs.Empty, null, null);

                    var presentationBundle = new MvxBundle(new Dictionary<string, string> { { Config.MvxPresentation.PresentationNavigation, Config.MvxPresentation.NavigationClearStack } });
                   
                    ShowViewModel<FeedViewModel>(new { showTour = true }, presentationBundle: presentationBundle);
                
                } 
                // Otherwise, we have it. Complete Account ViewModel
                else
                {
                    ShowViewModel<CompleteAccountViewModel>();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                PopupService.DisplayMessage("An unexpected error occured. Please try again","Error");
                //this.IsFacebookLoading = false;
                IsLoading = false;
                return;
            }
            
            //this.IsFacebookLoading = false;
            IsLoading = false;
        }





                #endregion


                public override void AddValidationFields(ValidationHelper helper)
                {
                        helper.AddRequiredRule((() => Email), "Email is required");
                        helper.AddRule(() => Email, () => RuleResult.Assert(InputValidationHelper.ValidateEmail(Email), "Invalid email"));
                        helper.AddRequiredRule((() => Password), "Pasword is required");
                        //helper.AddRule(() => Password, () => RuleResult.Assert(Password.Length >= 8, "Minimum 8 characters"));
                }


    
  //      public void Init(bool flag)
		//{
		//	Flag = flag;
		//}
    }
}
