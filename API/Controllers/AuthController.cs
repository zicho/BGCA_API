﻿using API.Core;
using API.Data.Models;
using API.Data.Models.User;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/user")] // this controller is part of the "user" namespace but is not inheriting a generic controller. It also does not require authentication to use.
    // consider maybe moving the contents of this controller to the yser controller? 
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ServiceResponse<AuthUserModel>> Register(CreateUserModel dto)
        {
            return await _userService.Register(dto);
        }

        [HttpPost("login")]
        public async Task<ServiceResponse<AuthUserModel>> Login(LoginUserModel dto)
        {
            return await _userService.Login(dto);
        }
    }
}