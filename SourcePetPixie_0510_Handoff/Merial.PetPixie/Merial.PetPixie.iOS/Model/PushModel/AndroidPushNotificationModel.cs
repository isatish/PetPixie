
using Merial.PetPixie.Core.Models.PushModels;
using Newtonsoft.Json;

namespace Merial.PetPixie.iOS.Model.PushModel
{
    public class iOSPushNotificationModel : PushNotificationModel
    {
        private const string ExtraNotificationName = "Notification";

    //    public static PushNotificationModel GetNotificationFromIntent(Intent intent)
    //    {
    //        var serializedNotification = intent.GetStringExtra(ExtraNotificationName);
    //        return JsonConvert.DeserializeObject<AndroidPushNotificationModel>(serializedNotification);
    //    }

    //    public Bundle GetBundle()
    //    {
    //        Bundle extras = new Bundle();
    //        extras.PutString(ExtraNotificationName, JsonConvert.SerializeObject(this));

    //        return extras;
    //    }
    }
}
