using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels
{
    public class NotificationViewModel : ViewModelBase<KNotification>
    {
        private KNotification _notificationBase;
        private NotificationModel _notificationModel;

        public NotificationViewModel(KNotification notification)
        {
            this.GetNotificationInfo(notification);
        }

        public NotificationModel NotificationModel
        {
            get { return _notificationModel; }
            set
            {
                if (_notificationModel == value) return;
                _notificationModel = value;

                this.RaisePropertyChanged();
            }
        }

        protected override void RealInit(KNotification parameter)
        {
            this.GetNotificationInfo(parameter);
        }

        private void GetNotificationInfo(KNotification notification)
        {
            this._notificationBase = notification;
            this.NotificationModel = new NotificationModel(notification);
        }
    }
}
