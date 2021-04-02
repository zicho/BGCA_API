using API.Core;
using API.Data.Entities.Messaging;
using API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface INotificationService
    {
        Task<ServiceResponse> SendNotification(NotificationModel model);

        Task<ServiceResponse<int>> GetUnreadNotificationsCount(string userName);

        Task<ServiceResponse<List<Notification>>> GetNotifications(string userName, int limit = 0, bool unreadOnly = false);

        Task<ServiceResponse> MarkAllAsRead(string userName);
    }
}