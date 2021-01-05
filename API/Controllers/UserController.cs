using API.Controllers.Interfaces;
using API.Core;
using API.Data.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase, IUserController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("{username}")]
        public async Task<ServiceResponse<AuthUserModel>> GetByUsername(string username)
        {
            return _mapper.Map<ServiceResponse<AuthUserModel>>(await _userService.GetByUsername(username));
        }

        public async Task<ServiceResponse<List<AuthUserModel>>> GetAll()
        {
            return _mapper.Map<ServiceResponse<List<AuthUserModel>>>(await _userService.GetAll());
        }
    }
}