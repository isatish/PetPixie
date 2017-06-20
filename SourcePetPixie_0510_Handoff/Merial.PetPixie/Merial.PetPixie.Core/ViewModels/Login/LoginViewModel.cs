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




namespace Merial.PetPixie.Core.ViewModels
{
    public class LoginViewModel : AuthenticationViewModelBase
    {
        #region Fields

        private readonly IKinveyService _kinveyService;
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;



        private bool _isTwitterLoading;
        private bool _isFacebookLoading;

        string[] Permssions = new string[] { "public_profile", "email" };


        #endregion

        #region Constructors

        public LoginViewModel(IUserService userService, IKinveyService kinveyService, IProfileService profileService) : base(userService, kinveyService, profileService )
        {
            _userService = userService;
            _kinveyService = kinveyService;
            _profileService = profileService;
            IsLoading = false;

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


                        Email = Settings.Email;
                        Password = Settings.Password;
        
        }

        #endregion

        #region Events

        public event EventHandler LoggedIn;

        #endregion

        #region Commands
        #endregion

        #region Properties
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





 
 

 
    
            
        #endregion

                
        public override void AddValidationFields(ValidationHelper helper)
        {
            helper.AddRequiredRule((() => Email), "Email is required");
            helper.AddRule(() => Email, () => RuleResult.Assert(InputValidationHelper.ValidateEmail(Email), "Invalid email"));
            helper.AddRequiredRule((() => Password), "Pasword is required");
            //helper.AddRule(() => Password, () => RuleResult.Assert(Password.Length >= 8, "Minimum 8 characters"));
        }


    
        public void Init(bool flag)
		{
			Flag = flag;
		}
    }
}
