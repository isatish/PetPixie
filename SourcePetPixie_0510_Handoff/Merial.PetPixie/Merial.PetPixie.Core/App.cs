using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using Xamarin.Forms;
using MvvmCross.Platform;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using System.Threading.Tasks;
using System;
using KinveyXamarin;
using Merial.PetPixie.Core.Models.Enums;
using Acr.UserDialogs;

namespace Merial.PetPixie.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes().
                EndingWith("Repository")
                .AsTypes()
                .RegisterAsLazySingleton();
                
			if (Device.OS == TargetPlatform.Android || Device.OS == TargetPlatform.iOS)
			{
				//dslocalization Resources.AppResources.Culture = Mvx.Resolve<Services.Contracts.ILocalizeService>().GetCurrentCultureInfo();
			}



            var kinveyClientProvider = Mvx.Resolve<IKinveyClientProvider>();
            kinveyClientProvider.CreateClient();

            var userService = Mvx.Resolve<IUserService>();

            //Test Login by forcing Login
            //userService.Logout();

            var isConnected = userService != null && userService.UserConnected();


                        
            var styles = new Styles();
            Xamarin.Forms.Application.Current.Resources = styles.Resources;

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            //If the user is already connected it is redirected to the home page
            if (isConnected)
            {
                Mvx.Resolve<IProfileService>().ReloadProfile();
                // RegisterAppStart<ViewModels.MainViewModel>();

             
               
                RegisterAppStart<ViewModels.FeedViewModel>();
              //  RegisterAppStart<ViewModels.SettingsViewModel>();
              //   RegisterAppStart<ViewModels.AnimationLoaderViewModel>();


            }
            //Otherwise, it will be on the login screen
            else {
               // RegisterAppStart<ViewModels.LoginViewModel>();
               // var appStart = new MvxAppStart<LoginPagerViewModel>();
               // Mvx.RegisterSingleton<IMvxAppStart>(appStart);


                               
                //  RegisterAppStart<ViewModels.TermsOfServiceViewModel>();
                //   RegisterAppStart<ViewModels.FeedViewModel>();
                //  RegisterAppStart<ViewModels.RootMainViewModel>();
                //  RegisterAppStart<ViewModels.SettingsViewModel>();
               //   RegisterAppStart<ViewModels.MyPackViewModel>();
               //   RegisterAppStart<ViewModels.PictureFunViewModel>();
                //RegisterAppStart<ViewModels.MyVetsViewModel>();


                RegisterAppStart<ViewModels.LoginTourViewModel>();

          //      RegisterAppStart<ViewModels.AnimationLoaderViewModel>();
            }
 
         //     RegisterAppStart<ViewModels.CompleteAccountViewModel>();

        }
 

        public static Action<string, string> PostSuccessFacebookLoginAction { get; set; }
        public static Action<string, string> PostSuccessFacebookSignupAction { get; set; }
        public static byte[] FunPictureBytes;
        public static byte[] FunPictureUpdatedBytes;


    }
}
