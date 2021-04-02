using API.Core;
using API.Data.Entities.Locations;
using API.Data.Entities.Messaging;
using API.Data.Models;
using API.Repositories;
using API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalRChat.Hubs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountryRepository _countryRepository;

        public CountryService(
            CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResponse<List<Country>>> GetCountries()
        {
            if (await _countryRepository.CountAll() == 0)
            {
                await SeedCountries();
            }

            var countries = await _countryRepository.GetAll();

            return new ServiceResponse<List<Country>> { Data = countries };
        }

        public async Task<ServiceResponse<List<State>>> GetCountryStates(string countrycode)
        {
            return new ServiceResponse<List<State>> { Data = await _countryRepository.GetCountryStates(countrycode) };
        }

        public async Task<ServiceResponse<List<Country>>> GetFullCountryInfo(string countrycode)
        {
            return new ServiceResponse<List<Country>> { Data = await _countryRepository.GetFullCountryInfo(countrycode) };
        }

        public async Task<ServiceResponse<List<City>>> GetStateCitites(int stateId)
        {
            return new ServiceResponse<List<City>> { Data = await _countryRepository.GetStateCities(stateId) };
        }

        private async Task SeedCountries()
        {
            using (StreamReader r = new StreamReader(@"C:\Projects\BGCA_API\API\Assets\countries_states_cities.json"))
            {
                var json = await r.ReadToEndAsync();
                var list = JsonConvert.DeserializeObject<List<Country>>(json);
                await _countryRepository.AddRange(list);
            }
            //using (var client = new System.Net.WebClient())
            //{
            //    var json = client.DownloadString("https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/countries%2Bstates%2Bcities.json");
            //    var list = JsonConvert.DeserializeObject<List<Country>>(json);

                //    await _countryRepository.AddRange(list);
                //}
        }
    }
}