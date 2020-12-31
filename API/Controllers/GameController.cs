using API.Data.Entities;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class GameController : BaseController<Game>
    {
        private readonly IBaseService<Game> _service;
        public GameController(IBaseService<Game> service, IMapper mapper) : base(service, mapper)
        {
            _service = service;
        }
    }
}
