using API.Core;
using API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.Interfaces
{
    public interface IUserController
    {
        Task<ServiceResponse<List<AuthUserModel>>> GetAll();
    }
}