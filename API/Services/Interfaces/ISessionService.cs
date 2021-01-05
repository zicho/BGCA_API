using API.Controllers;
using API.Core;
using API.Data.Entities;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ISessionService
    {
        Task<ServiceResponse<Session>> Add(CreateSessionModel newSession);

        Task<ServiceResponse<bool>> Invite(InviteGameModel model);
    }
}