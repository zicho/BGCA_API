using API.Core;
using API.Data.Models;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ServiceResponse> SendPrivateMessage(PrivateMessageModel model);
        Task<ServiceResponse<int>> GetUnreadMessagesCount(string userName);
        Task<ServiceResponse> MarkAllAsRead(string userName);
    }
}