using API.Controllers;
using API.Core;
using API.Data.Entities;
using API.Data.Models;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ISessionService
    {
        Task<ServiceResponse<Session>> Add(CreateSessionModel newSession);

        Task<ServiceResponse> Invite(InviteGameModel model);
    }
}