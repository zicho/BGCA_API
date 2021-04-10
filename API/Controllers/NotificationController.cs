using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _service;

        public NotificationController(IMapper mapper, INotificationService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("{username}/unread/count")]
        public virtual async Task<ServiceResponse<int>> GetUnreadNotificationsAsync(string username)
        {
            return await _service.GetUnreadNotificationsCount(username);
        }

        [HttpGet("{username}/{limit?}/{unreadOnly?}")]
        public async Task<ServiceResponse<List<NotificationModel>>> GetNotifications(string username, int limit = 0, bool unreadOnly = false)
        {
            return _mapper.Map<ServiceResponse<List<NotificationModel>>>(await _service.GetNotifications(username, limit, unreadOnly));
        }
    }
}