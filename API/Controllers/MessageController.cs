using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual async Task<ServiceResponse<bool>> SendPrivateMessage(PrivateMessageModel model)
        {
            return await _service.SendPrivateMessage(model);
        }
    }
}
