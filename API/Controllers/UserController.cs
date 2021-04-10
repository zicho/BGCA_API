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

        //[HttpGet("test")]
        //public async Task<List<City>> Test()
        //{
        //    using (var client = new System.Net.WebClient())
        //    {
        //        var json = client.DownloadString("https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/countries%2Bstates%2Bcities.json");
        //        var list = JsonConvert.DeserializeObject<List<Country>>(json);
        //    }

        //    return null;
        //}

        //public async Task<List<Country>> Test()
        //{
        //    var items = new List<Country>();

        //    using (var client = new System.Net.WebClient())
        //    {
        //        var json = client.DownloadString("https://raw.githubusercontent.com/David-Haim/CountriesToCitiesJSON/master/countriesToCities.json");
        //        var dict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);

        //        foreach (var item in dict)
        //        {
        //            var c = new Country
        //            {
        //                Name = item.Key,
        //                Cities = item.Value
        //            };
        //            items.Add(c);
        //        }
        //    }

        //    return items.OrderBy(x => x.Name).ToList();
        //}
    }
}