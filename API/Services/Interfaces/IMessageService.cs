using API.Core;
using API.Data.Models;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ServiceResponse<bool>> SendPrivateMessage(PrivateMessageModel model);
    }
}