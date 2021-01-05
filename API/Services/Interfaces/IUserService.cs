using API.Core;
using API.Data.Entities.Users;
using API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAll();

        Task<ServiceResponse<User>> GetById(int id);

        Task<ServiceResponse<User>> Update(User entity);

        Task<ServiceResponse<User>> Remove(int id);

        Task<ServiceResponse<User>> GetByUsername(string username);

        // authentication and registration
        Task<ServiceResponse<CreateUserModel>> Register(CreateUserModel dto);

        Task<ServiceResponse<AuthUserModel>> Login(LoginUserModel dto);

        Task<ServiceResponse<bool>> SendFriendRequest(FriendRequestModel model);
    }
}