using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.Core.Services
{
    public class NotificationService:KinveyServiceBase, INotificationService
    {
        #region Fields

        private readonly IUserService _userService;
        private int _nbrNewNotifications;
        private bool _isLoading;
        private IList<KNotification> _lastLoadedNewNotifications; // Used to check if Read=true has to be sent

        #endregion

        #region Constructors

        public NotificationService(IKinveyService kinveyService, IUserService userService) : base(kinveyService)
        {
            _userService = userService;
        }

        #endregion

        #region Events

        public event EventHandler NewNotificationArrived;

        #endregion

        #region Properties

        public int NbrNewNotifications
        {
            get
            {
                return _nbrNewNotifications;
            }
            set
            {
                if (_nbrNewNotifications == value) return;
                _nbrNewNotifications = value;
                
                if (this.NewNotificationArrived != null && this.NewNotificationArrived.GetInvocationList().Any())
                    foreach (EventHandler target in this.NewNotificationArrived.GetInvocationList())
                        target.BeginInvoke(this, EventArgs.Empty, null, null);
            }
        }
        
        #endregion

        #region Public Methods

        public async Task<List<KNotification>> GetAll()
        {
            if (_isLoading) return new List<KNotification>();

            _isLoading = true;

            string userId = _userService.GetProfileId();
            var allNotification = await base.GetAppDataAsync<KNotification>("Notification", new { profile_id = userId });

            this._lastLoadedNewNotifications = allNotification.Where(notification => notification.Read).ToList();
            this.NbrNewNotifications = _lastLoadedNewNotifications.Count;

            _isLoading = false;

            return allNotification.OrderByDescending(notification => notification.CreatedTime).ToList();
        }

        public async Task<List<KNotification>> GetNews()
        {
            if (_isLoading) return new List<KNotification>();

            _isLoading = true;

            string userId = _userService.GetProfileId();
            this._lastLoadedNewNotifications = await base.GetAppDataAsync<KNotification>("Notification", new { profile_id = userId, read = true });  //dschange this back to false after the layout is finalized
            this.NbrNewNotifications = _lastLoadedNewNotifications.Count;

            _isLoading = false;

            return this._lastLoadedNewNotifications.OrderByDescending(notification => notification.CreatedTime).ToList();
        }

        public async Task SetNewAsReadFromId(string notificationId)
        {
            try
            {
                var notification = await base.GetAppDataEntityAsync<IEnumerable<KNotification>>("Notification", notificationId);
                await this.SetNewAsRead(notification.Single());
            }
            catch (Exception e)
            { // TODO
            }
        }

        public async Task SetNewAsRead(KNotification notification)
        {
            try
            {
                notification.Read = true;
                await base.SaveAppdataAsync("Notification", notification);

                this.NbrNewNotifications--;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public void IncrementNbrNews()
        {
            this.NbrNewNotifications++;
        }

        #endregion
    }
}