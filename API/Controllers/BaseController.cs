using API.Core;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public abstract class BaseController<T> : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<T> _service;

        protected BaseController(IBaseService<T> service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        protected string GetRoleFromHttpContext() => HttpContext.User.FindFirstValue(ClaimTypes.Role);

        [HttpGet]
        public virtual async Task<ServiceResponse<List<T>>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet("id/{id}")]
        public virtual async Task<ServiceResponse<T>> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public virtual async Task<ServiceResponse<T>> Add(T entity)
        {
            return await _service.Add(entity);
        }

        [HttpPut]
        public virtual async Task Edit(T entity)
        {
            await _service.Update(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task Delete(int id)
        {
            await _service.Remove(id);
        }
    }
}