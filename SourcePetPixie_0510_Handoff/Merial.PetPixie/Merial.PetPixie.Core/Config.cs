using System;
using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core {
    public static class Config
    {
        public static class MvxPresentation
        {
            public const string PresentationNavigation = "NavigationMode";
            public const string NavigationClearStack = "NavigationClearStack";
            public const string NavigationClearTop = "NavigationClearTop";
            public const string ReloadScreen = "ReloadScreen";
        }

        public static class UploadImage
        {
            public const string Type = "KinveyFile";
            public const string MimeType = "image/jpeg";

        }
        

        public static class CachePersitenceConfig
        {
            public static readonly TimeSpan BreedPersistance = new TimeSpan(24, 0, 0);
            public static readonly TimeSpan ReminderTypePersistance = new TimeSpan(24, 0, 0);
            public const string SurveyId = "SurveyId";

            public static TimeSpan GetPersistance(LocalCachingPolicy persistence)
            {
                switch (persistence)
                {
                    case LocalCachingPolicy.BreedPersistance:
                        return BreedPersistance;
                    case LocalCachingPolicy.ReminderTypePersistance:
                        return ReminderTypePersistance;
                    default:
                        return new TimeSpan(0);
                }
            }
        }

        public const string HockeyApp_AappId = "b4a13fa780d240729fa97b822a86e571";// "4e2fbc6959c446afba1e9da09892bd51";// "ff33bba78e314650a2179a8c39d7bc22";
    }


}