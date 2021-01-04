using API.Core;
using API.Data.Entities.Messaging;
using API.Data.Models;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository<PrivateMessage> _messageRepository;
        private readonly UserRepository _userRepository;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext;

        public MessageService(
            IBaseRepository<PrivateMessage> messageRepository,
            UserRepository userRepository,
            IHubContext<NotificationHub, INotificationHub> notificationHubContext)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _notificationHubContext = notificationHubContext;
        }

        public async Task<ServiceResponse<bool>> SendPrivateMessage(PrivateMessageModel model)
        {
            try
            {
                var sender = await _userRepository.GetByUsername(model.Sender);
                var recipient = await _userRepository.GetByUsername(model.Recipient);

                await _messageRepository.Add(new PrivateMessage
                {
                    Sender = sender, Recipient = recipient,
                    Subject = model.Subject, Content = model.Content,
                    CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow
                });

                await _notificationHubContext.Clients.Group(model.Recipient).SendPrivateMessage(model.Recipient, model.Subject, model.Content);

                return new ServiceResponse<bool>();
            } catch
            {
                return new ServiceResponse<bool>(false);
            }
        }
    }
}
