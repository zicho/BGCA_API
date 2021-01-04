using API.Core;
using API.Data.Entities.Messaging;
using API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ServiceResponse> SendPrivateMessage(PrivateMessageModel model);
        Task<ServiceResponse<int>> GetUnreadMessagesCount(string userName);
        Task<ServiceResponse<List<PrivateMessage>>> GetPrivateMessages(string userName, int limit = 0, bool unreadOnly = false);
        Task<ServiceResponse> MarkAllAsRead(string userName);
    }
}