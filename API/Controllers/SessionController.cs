﻿using API.Core;
using API.Data.Entities;
using API.Data.Models;
using API.Services;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service, IMapper mapper)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ServiceResponse<Session>> Add(CreateSessionModel newSession)
        {
            return await _service.Add(newSession);
        }

        [HttpPost("invite")]
        public async Task<ServiceResponse> Invite(InviteGameModel model)
        {
            return await _service.Invite(model);
        }
    }
}