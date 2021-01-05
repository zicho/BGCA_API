using API.Core;
using API.Data.Entities.Users;
using API.Data.Models;
using API.Repositories;
using API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext;

        public UserService(UserRepository repository, IConfiguration configuration, IHubContext<NotificationHub, INotificationHub> myHub)
        {
            _repository = repository;
            _configuration = configuration;
            _notificationHubContext = myHub;
        }

        public async Task<ServiceResponse<CreateUserModel>> Register(CreateUserModel dto)
        {
            if (await UserExists(dto.Username)) return new ServiceResponse<CreateUserModel>
            {
                Success = false,
                Message = "Username taken"
            };

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = dto.Username,
                Role = dto.Role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            try
            {
                await _repository.Add(user);
                return new ServiceResponse<CreateUserModel>
                {
                    Message = $"User '{user.Username}' was created successfully."
                };
            }
            catch
            {
                return new ServiceResponse<CreateUserModel>
                {
                    Success = false,
                    Message = "Error creating user"
                };
            }
        }

        public async Task<ServiceResponse<bool>> SendFriendRequest(FriendRequestModel model)
        {
            try
            {
                var response = new ServiceResponse<bool>();

                var sender = await _repository.GetById(model.SenderId);
                var recipient = await _repository.GetById(model.RecipientId);

                await _repository.AddFriendRequest(sender, recipient);

                response.Success = true;

                return response;
            }
            catch
            {
                return new ServiceResponse<bool> { Success = false, Message = "Friend request failed?" };
            }
        }

        public async Task<ServiceResponse<AuthUserModel>> Login(LoginUserModel dto)
        {
            var response = new ServiceResponse<AuthUserModel>();
            var user = await _repository.GetByUsername(dto.Username);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = new AuthUserModel
                {
                    Username = user.Username,
                    JWT = CreateToken(user)
                };

                response.Message = "Login success!";
            }

            return response;
        }

        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            try
            {
                return new ServiceResponse<List<User>>
                {
                    Data = await _repository.GetAll()
                };
            }
            catch
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = $"Method '{nameof(GetAll)}' in '{GetType().Name}' failed'"
                };
            }
        }

        public async Task<ServiceResponse<User>> GetByUsername(string username)
        {
            var response = new ServiceResponse<User>();
            var user = await _repository.GetByUsername(username);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else
            {
                response.Data = user;

                response.Message = "Login success!";
            }

            return response;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _repository.UserExists(username);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Task<ServiceResponse<User>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<User>> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<User>> Remove(int id)
        {
            try
            {
                await _repository.Remove(id);
                return new ServiceResponse<User> { Message = $"User (ID: {id}' was successfully removed." }; ;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}