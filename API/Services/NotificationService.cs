using API.Core;
using API.Data.Entities.Messaging;
using API.Data.Models;
using API.Repositories;
using API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _userRepository;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext;

        public NotificationService(
            NotificationRepository notificationRepository,
            UserRepository userRepository,
            IHubContext<NotificationHub, INotificationHub> notificationHubContext)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _notificationHubContext = notificationHubContext;
        }

        public async Task<ServiceResponse> SendNotification(NotificationModel model)
        {
            try
            {
                var sender = await _userRepository.GetByUsername(model.Sender);
                var recipient = await _userRepository.GetByUsername(model.Recipient);

                await _notificationRepository.Add(new Notification
                {
                    Sender = sender,
                    Recipient = recipient,
                    Subject = model.Subject,
                    Content = model.Content
                });

                await _notificationHubContext.Clients.Group(model.Recipient).SendNotice(model);

                return new ServiceResponse();
            }
            catch
            {
                return new ServiceResponse(false);
            }
        }

        public async Task<ServiceResponse<int>> GetUnreadNotificationsCount(string username)
        {
            try
            {
                var unreadNotifications = await _notificationRepository.GetWhere(x => x.IsRead == false && x.Recipient.Username == username);
                return new ServiceResponse<int> { Data = unreadNotifications.ToList().Count() };
            }
            catch
            {
                return new ServiceResponse<int>(false);
            }
        }

        public async Task<ServiceResponse<List<Notification>>> GetNotifications(string username, int limit = 0, bool unreadOnly = false)
        {
            try
            {
                var unreadMessages = await _notificationRepository.GetNotificationsForUser(username, limit, unreadOnly);

                return new ServiceResponse<List<Notification>> { Data = unreadMessages.ToList() };
            }
            catch
            {
                return new ServiceResponse<List<Notification>>(false);
            }
        }

        public async Task<ServiceResponse> MarkAllAsRead(string username)
        {
            try
            {
                var unreadMessages = await _notificationRepository.GetWhere(x => x.IsRead == false && x.Recipient.Username == username);

                foreach (var m in unreadMessages)
                {
                    m.IsRead = true;
                }

                await _notificationRepository.SaveChangesAsync();

                return new ServiceResponse();
            }
            catch
            {
                return new ServiceResponse(false);
            }
        }
    }
}