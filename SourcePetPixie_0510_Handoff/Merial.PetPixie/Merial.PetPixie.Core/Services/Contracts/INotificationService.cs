using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Kinvey;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface INotificationService
    {
        event EventHandler NewNotificationArrived;
        int NbrNewNotifications { get; }
        void IncrementNbrNews();
        Task SetNewAsRead(KNotification notification);
        Task SetNewAsReadFromId(string notificationId);
        Task<List<KNotification>> GetAll();
        Task<List<KNotification>> GetNews();
    }
}