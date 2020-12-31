using API.Controllers;
using API.Core;
using API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ISessionService
    {
        Task<ServiceResponse<Session>> Add(CreateSessionModel newSession);
        Task<ServiceResponse<bool>> Invite(InviteGameModel model);
    }
}
