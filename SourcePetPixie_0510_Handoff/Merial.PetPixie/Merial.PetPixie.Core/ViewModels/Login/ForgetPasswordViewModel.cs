using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
        public class ForgetPasswordViewModel : ValidationFormViewModelBase
        {
                private IUserService _userService;
                private IDeviceHelper _deviceHelper;
                public IMvxCommand ResetPasswordCommand => new SafeMvxCommandAsync(ResetPassword);
                public string Email { get; set; }
                public ICommand BackCommand => new SafeMvxCommand(() => Close(this));

                public ForgetPasswordViewModel(IUserService userService, IDeviceHelper deviceHelper)
                {
                        _userService = userService;
                        _deviceHelper = deviceHelper;
                }

                private async Task ResetPassword()
                {
                        if (!Validate())
                        {
                                await PopupService.DisplayMessageAsync("Invalid email", "Please enter a valid email address");

                                return;
                        }
                                IsLoading = true;
                        _deviceHelper.HideKeyBoard();
                        try
                        {
                               // var loader = new Shared.Loader();
                                //loader.SetLoadingMessage("Processing password reset request");

                                        MessagingCenter.Send(this, "ShowLoadingMessage");

                                Email = Email.ToLower();
                                var success = await _userService.SendResetPasswordInstructionsAsync(Email);
                                if (!success)
                                {
                                        IsLoading = false;
                                        await PopupService.DisplayMessageAsync("Information", "User does not exist. Maybe you would like to sign up");
                                        return;
                                }
                                     MessagingCenter.Send(this, "StopLoading");
                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                IsLoading = false;
                                await PopupService.DisplayMessageAsync("Error", "An error occured while sending reset password instructions.");
                        }
                        await PopupService.DisplayMessageAsync("Information", "You will receive an email with instructions how to reset your password.");
                        IsLoading = false;
                        ShowViewModel<LoginViewModel>();

                }

                public override void AddValidationFields(ValidationHelper helper)
                {
                        helper.AddRequiredRule((() => Email), "Email is required");
                        helper.AddRule(() => Email, () => RuleResult.Assert(InputValidationHelper.ValidateEmail(Email), "Invalid email"));
                }
        }
}