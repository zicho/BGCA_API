using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _service;

        private string GetRoleFromHttpContext() => HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public MessageController(IMapper mapper, IMessageService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public virtual async Task<ServiceResponse> SendPrivateMessage(PrivateMessageModel model)
        {
            return await _service.SendPrivateMessage(model);
        }

        [HttpGet("{username}/{limit?}")]
        public virtual async Task<ServiceResponse<List<PrivateMessageModel>>> GetMessages(string username, int limit = 0)
        {
            return _mapper.Map<ServiceResponse<List<PrivateMessageModel>>>(await _service.GetPrivateMessages(username, limit));
        }

        [HttpGet("{username}/unread/{limit?}")]
        public virtual async Task<ServiceResponse<List<PrivateMessageModel>>> GetUnreadMessages(string username, int limit = 0)
        {
            //var user = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //if (user != username && GetRoleFromHttpContext() != UserRoles.Admin)
            //    return new ServiceResponse<int>(false) { Message = "You do not have access to this user" };
            //return await _service.GetUnreadMessages(username);
            return _mapper.Map<ServiceResponse<List<PrivateMessageModel>>>(await _service.GetPrivateMessages(username, limit, true));
        }

        [HttpGet("{username}/unread/count")]
        public virtual async Task<ServiceResponse<int>> GetUnreadMessagesCount(string username)
        {
            //var user = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //if (user != username && GetRoleFromHttpContext() != UserRoles.Admin)
            //    return new ServiceResponse<int>(false) { Message = "You do not have access to this user" };

            return await _service.GetUnreadMessagesCount(username);
        }

        [HttpGet("{username}/unread/clear")]
        public virtual async Task<ServiceResponse> MarkAllAsRead(string username)
        {
            return await _service.MarkAllAsRead(username);
        }
    }
}