using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using UIKit;
using Xamarin.Forms;
using MvvmCross.Forms.Presenter.iOS;
using MvvmCross.Forms.Presenter.Core;
using MvvmCross.Platform;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.iOS.Services;
using Merial.PetPixie.Core;
using Merial.PetPixie.Core.Plugins;
using Common.Logging;
using Merial.PetPixie.iOS.Core;
using Exakis.Common.Logging;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Json;
using Xamarin;
using Acr.UserDialogs;
//using FFImageLoading.Forms.Touch;
//using FFImageLoading.Cross;

namespace Merial.PetPixie.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

        //dslocaliza  Mvx.RegisterSingleton<ILocalizeService>(new Services.LocalizeService());

            Mvx.RegisterSingleton<IAuthentication>(new Authentication());
            Mvx.RegisterSingleton<IDeviceHelper>(new DeviceHelper());
            Mvx.RegisterSingleton<IKinveyClientProvider>(new KinveyClientProvider());
            Mvx.RegisterSingleton<IPopupService>(new PopupService());
            Mvx.RegisterSingleton<IImageService>(new ImageService());
            Mvx.RegisterSingleton<ICacheService>(new CacheService());
            Mvx.RegisterSingleton<INavigationService>(new NavigationService());
            Mvx.RegisterSingleton<IBitmapService>(new BitmapService());
            Mvx.RegisterSingleton<IByteService>(new ByteService());
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<ILocationService>(new LocationService());
            Mvx.RegisterSingleton<IFacebookService>(new FacebookService());
            Mvx.RegisterSingleton<ITwitterService>(new TwitterService());
            Mvx.RegisterSingleton<IInitService>(new InitService());
          
            Mvx.RegisterSingleton<ILog>(new StaticFileLogger());
            //Mvx.RegisterSingleton<IDeviceInfoService>(new DeviceInfoService(ApplicationContext));//iOS not needed
            Mvx.RegisterSingleton<ICalendarService>(new CalendarService());
             
            Mvx.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            //Mvx.RegisterType<MvxImageLoadingView, MvxImageLoadingView()
            //    [Register("MvxImageLoadingView")]
           
        }

        protected override IMvxApplication CreateApp()
        {
            return new Merial.PetPixie.Core.App();
        }

        public override void LoadPlugins(MvvmCross.Platform.Plugins.IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<Acr.UserDialogs.IUserDialogs>();

        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
             
            Forms.Init();
            FormsMaps.Init();

            MR.Gestures.iOS.Settings.LicenseKey = "DATF-LK4L-X3N7-ZTFS-U2BK-6W7X-GCHY-RZW3-6BB6-LLXJ-NSLE-2WGD-4HDE";


            var xamarinFormsApp = new MvxFormsApp();

            return new  MvxFormsIosPagePresenter(Window, xamarinFormsApp);
            //return new MvxFormsMasterDetailPagePresenter();// MvxFormsMasterDetailPagePresenter(Window, xamarinFormsApp);
        }
    }
}
