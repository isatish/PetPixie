using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
	public class LoginPagerViewModel :MvxViewModel
	{
		#region Commands
		public IMvxCommand LoginCommand => new SafeMvxCommand(LogIn);
		public IMvxCommand SignUpCommand => new SafeMvxCommand(SignUp);
		#endregion
		#region Methods
		private void SignUp()
		{
			ShowViewModel<SignupViewModel>();
		}

		private void LogIn()
		{
			ShowViewModel<LoginViewModel>();
		}
		#endregion
	}
}

