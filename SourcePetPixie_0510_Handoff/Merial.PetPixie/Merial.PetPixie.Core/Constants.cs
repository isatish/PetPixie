using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core
{
    public class Constants
    {
        // DEV
        //public class Kinvey
        //{
        //    public const string KeyApp = "kid_WJP0mz1Qy-";
        //    public const string KeySecret = "d0e8c284551d4099a1371c86ed62cf9f";
        //    public const string RedirectUri = "https://services-qa.merial.com/kinvey_Android_STG/api/Authenticate";
        //}

        // UAT
        public class Kinvey
        {
			//AndroidStaging 
			//public const string KeyApp = "kid_Z1nKQD1KT";
			//public const string KeySecret = "f01e4f4f81bf4c919e83bee54fe945bd";
			//public const string RedirectUri = "https://services-qa.merial.com/kinvey_android/api/Authenticate";
			//Staging
			#if STAGING
			public const string KeyApp = "kid_-1m-wEn7-g";
			public const string KeySecret = "0aa11a90a53c49d6827a8dbbd2c10308";
			public const string RedirectUri = "https://services-qa.merial.com/kinvey_mobile/api/Authenticate";
			
            #elif PROD
			//Prod
			public const string KeyApp = "kid_WJsbL4CTo";
			public const string KeySecret = "b148ff72253b489cb125eae04a8bdbff";
			public const string RedirectUri = "https://services.merial.com/PetPixie/api/Authenticate";
			#endif

            //dsmvx
            //public const string KeyApp =  "kid_-1m-wEn7-g";
            //public const string KeySecret = "0aa11a90a53c49d6827a8dbbd2c10308";
            //public const string RedirectUri = "https://services-qa.merial.com/kinvey_mobile/api/Authenticate";

            //public const string KeyApp = "kid_WJsbL4CTo";
           // public const string KeySecret = "b148ff72253b489cb125eae04a8bdbff";
            //public const string RedirectUri = "https://services.merial.com/PetPixie/api/Authenticate";



            //public const string KeyApp = "kid_WJsbL4CTo";
            //public const string KeySecret = "b148ff72253b489cb125eae04a8bdbff";
            //public const string RedirectUri = "https://services.merial.com/PetPixie/api/Authenticate";

        }

        public class Profile
        {
            public const int AccountCompletedProfileLevel = 1;
        }
        
        public class Connection
        {
            public const string KinveyUsername = "MerialKinvey";
            public const string FacebookUsername = "MerialFacebook";
            public const string TwitterUsername = "MerialTwitter";
        }

        public class AppUrls
        {
            public static string UrlRoot = "https://petpluspixie.com/";
            public static string WebAppPostUrl = $"{UrlRoot}post/";
        }

        public class FeedAccess
        {
            public const string Feed = "feed";
            public const string Discover = "discover";
            public const string Profile = "profile";
        }

        public class Facebook
        {
            public const string ApplicationId = "1442118239411883";    //"1442118239411883";//  "879420555510025"; 
        }

        public class Twitter
        {
            public const string ConsumerKey = "xTar2PIgcdoTyET7pek0P6Pz8";// OLD "BGbncwhTeofPrDZrix7Y4aS91";
            public const string ConsumerSecret = "3KyeIe9wnxI08HyTjEI52epMn12xIy0cWHBP366EFrDJNAZzs3";// OLD "7PqnoAPROxDroGM92IeU80dnenAdt9WMwnPtefCU8hgHUu0ljm";
        }

        public class Feed
        {
            public const string ProductName = "Pet+Pixie";
        }

        public class PasswordReset {
           public const string User = "MerialKinveyforgot-password";
           public const string Password = "qbrEzl5O*^z#RDeT9$Df";

          //  public const string User = "chicksabcs@gmail.com";
          //  public const string Password = "cowboy43";


        }

		public class Pictures {
			public const string FolderName = "PetPixies";
			public const string TakeAPicture = "Take a picture";
			public const string SelectAPicture = "Select a picture";
			public const string FileNameBase = "PetPixie_{0}.jpg";
		}

		public class Video {
			public const string FolderName = "PetPixiesVideo";
			public const string RecordVideo = "Record a video";
			public const string SelectAVideo = "Select a video";
		}

		public class MediaType {
			public enum type { Picture,Video};
		}

        public class Google {
			public const string SenderId = "484589310128"; // "955811969358";
        }

        public class InviteFriendMode
        {
            public const string Sms = "Sms";
            public const string Mail = "Email";
        }

        public class AppUrl
        {
            public const string About = "https://www.petpluspixie.com/about";
            public const string TermOfUse = "https://www.petpluspixie.com/terms";
            public const string Privacy = "https://www.petpluspixie.com/privacy";
            public const string GuideLines = "https://www.petpluspixie.com/guidelines";
        }
        public static class ImageService
        {
            public const string ExtraReturnFile = "returnFile";
            public const string ExtraReturnData = "return-data";
            public const string ExtraImageUri = "image-uri";
            public const string ExtraData = "data";
            public const string ExtraUseTempFile = "useTempFile";
            public const string MineTypeLocal = "localImage";

        }
    }
}