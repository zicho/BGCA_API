using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        private string GetRoleFromHttpContext() => HttpContext.User.FindFirstValue(ClaimTypes.Role);
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual async Task<ServiceResponse> SendPrivateMessage(PrivateMessageModel model)
        {
            return await _service.SendPrivateMessage(model);
        }

        [HttpGet("{username}")]
        public virtual async Task<ServiceResponse<int>> GetUnreadMessages(string username)
        {
            //var user = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //if (user != username && GetRoleFromHttpContext() != UserRoles.Admin)
            //    return new ServiceResponse<int>(false) { Message = "You do not have access to this user" };

            return await _service.GetUnreadMessagesCount(username);
        }

        [HttpGet("markAllRead/{username}")]
        public virtual async Task<ServiceResponse> MarkAllAsRead(string username)
        {
            return await _service.MarkAllAsRead(username);
        }
    }
}
