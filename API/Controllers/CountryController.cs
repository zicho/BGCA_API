using API.Core;
using API.Data.Entities.Locations;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;
        private readonly IMapper _mapper;

        public CountryController(ICountryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ServiceResponse<List<Country>>> GetAll()
        {
            return await _service.GetCountries();
        }

        [HttpGet("{countrycode}")]
        public async Task<ServiceResponse<List<Country>>> GetFullInfo(string countrycode)
        {
            return await _service.GetFullCountryInfo(countrycode);
        }

        [HttpGet("states/{countryCode}")]
        public async Task<ServiceResponse<List<State>>> GetStates(string countrycode)
        {
            return await _service.GetCountryStates(countrycode);
        }

        [HttpGet("cities/{stateId}")]
        public async Task<ServiceResponse<List<City>>> GetStateCities(int stateId)
        {
            return await _service.GetStateCitites(stateId);
        }
    }
}