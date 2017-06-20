using System;
using System.Linq;
//using Facebook.CoreKit;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Merial.PetPixie.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;
//using UXDivers.Artina.Shared;
using Xamarin.Forms.Platform;
using Xamarin;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Maps;
using System.IO;
using System.Threading.Tasks;
using Facebook.CoreKit;
using Acr.UserDialogs;
//using FFImageLoading.Forms.Touch;

namespace Merial.PetPixie.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
        // class-level declarations
        string appId = Constants.Facebook.ApplicationId;//App id
        string appName = "Pet+Pixie";//App name

        private const string FacebookAppId = Constants.Facebook.ApplicationId;// "1442118239411883";
        private const string FacebookAppName = "Pet+Pixie";  // "b881053747aadcc511c764eedb21ff80";


        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, Window);
            setup.Initialize();



            MR.Gestures.iOS.Settings.LicenseKey = "DATF-LK4L-X3N7-ZTFS-U2BK-6W7X-GCHY-RZW3-6BB6-LLXJ-NSLE-2WGD-4HDE";
            //    Xamarin.Forms.Init();//platform specific init
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            FAB.iOS.FloatingActionButtonRenderer.InitControl();

             
                UISwitch.Appearance.OnTintColor = UIColor.Red;
            UISwitch.Appearance.OnImage = new UIImage("customswitch.png");

            UISlider.Appearance.MinimumTrackTintColor = UIColor.Red;
            UISlider.Appearance.MaximumTrackTintColor = UIColor.Purple;


        //    FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

          //  CachedImageRenderer.Init();

   //ds         Profile.EnableUpdatesOnAccessTokenChange(true);
   //ds         Settings.AppID = appId;
   //ds         Settings.DisplayName = appName;
                    
            string appId = Constants.Facebook.ApplicationId;//App id
            string appName = "Pet+Pixie";//App name

          //  FBSettings.DefaultAppID = FacebookAppId;
          //  FBSettings.DefaultDisplayName = FacebookAppName;

            Settings.AppID = appId;
            Settings.DisplayName = appName;



            Xamarin.FormsMaps.Init();
          //  Xamarin.FormsMaps.Init
            //FormsMaps.Init();


            //DS - Added for unhandled exception handling            
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();



           // CarouselViewRenderer.Init();

          //  CarouselViewRenderer.Init();
            Window.MakeKeyAndVisible();

            LogFonts();
            return true;
        }

        /// <summary>
        /// Logs the installed fonts to the debug window.
        /// </summary>
        private void LogFonts()
        {
            foreach (NSString family in UIFont.FamilyNames)
            {
                foreach (NSString font in UIFont.FontNamesForFamilyName(family))
                {
                    Console.WriteLine(@"{0}", font);
                }
            }
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.


          //  base.OnActivated(application);
            // We need to properly handle activation of the application with regards to SSO
            // (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
          //  FBSession.ActiveSession.HandleDidBecomeActive();
        }




        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        //public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        //{
            
        ////    base.OpenUrl(application, url, sourceApplication, annotation);
        // //   return FBSession.ActiveSession.HandleOpenURL(url);
        //}

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

     //ds   public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
     //ds   {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
  //ds          return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
    //ds    }



              
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            LogUnhandledException(newExc);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
                DateTime.Now, exception.ToString());
                File.WriteAllText(errorFilePath, errorMessage);

                // Log to Android Device Logging.
                //System.Web.Util.Log.Error("Crash Report", errorMessage);
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }



    }
}


