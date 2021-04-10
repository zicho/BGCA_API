using API.Core;
using API.Data.Entities.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface ICountryService
    {
        Task<ServiceResponse<List<Country>>> GetCountries();

        Task<ServiceResponse<List<Country>>> GetFullCountryInfo(string countrycode);

        Task<ServiceResponse<List<State>>> GetCountryStates(string countrycode);

        Task<ServiceResponse<List<City>>> GetStateCitites(int stateId);
    }
}