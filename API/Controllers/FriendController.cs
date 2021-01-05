using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]s")]
    public class FriendController : ControllerBase
    {
        private readonly IUserService _userService;

        public FriendController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public async Task<ServiceResponse<bool>> GetByUsername(FriendRequestModel model)
        {
            return await _userService.SendFriendRequest(model);
        }
    }
}