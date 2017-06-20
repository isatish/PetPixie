using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmValidation;

namespace Merial.PetPixie.Core.ViewModels
{
    public class LoginTourViewModel : ValidationFormViewModelBase
    {
        #region Fields

        private readonly IUserService _userService;
	   
        private readonly IProfileService _profileService;
        private readonly IKinveyService _kinveyService;
        private readonly IDeviceHelper _deviceHelper;

		private string _username;
        #endregion

        #region Constructors

        public LoginTourViewModel(IUserService userService,IProfileService profileService, IKinveyService kinveyService, IDeviceHelper deviceInfoService)
        {
            _userService = userService;
            _profileService = profileService;
            _kinveyService = kinveyService;
            _deviceHelper = deviceInfoService;
            Settings.Email = null;
            Settings.Password = null;
            Settings.CurrentUser = null;
        }

        #endregion

        #region Commands

        public IMvxCommand SignupCommand => new SafeMvxCommandAsync(async ()=> await EnsureIsConnected(Signup));
		public IMvxCommand LoginCommand => new SafeMvxCommand(LogIn);
//		public IMvxCommand CloseScreen => new SafeMvxCommand(() => Close(this));

		#endregion

		#region Events

		public event EventHandler LoggedIn;

        #endregion

        #region Public Properties

        public string Email { get; set; }

		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				this.RaiseAndSetIfChanged(ref _username, value);
			}
		}

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

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
        public async Task Signup() {

            ShowViewModel<SignupViewModel>();
           
        }


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