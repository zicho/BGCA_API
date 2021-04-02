using API.Data;
using API.Data.Entities;
using API.Data.Entities.Locations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Country>> GetAllCountriesWithStates()
        {
            return await Context.Countries.Include(x => x.States).ToListAsync();
        }
        public async Task<List<Country>> GetAllCountriesWithStatesAndCities()
        {
            return await Context.Countries.Include(x => x.States).ThenInclude(x => x.Cities).ToListAsync();
        }
        public async Task<List<State>> GetCountryStates(string countryCode)
        {
            return await Context.States.Where(x => x.Country.CountryCode == countryCode).ToListAsync();
        }

        public async Task<List<City>> GetStateCities(int stateId)
        {
            return await Context.Cities.Where(x => x.StateId == stateId).ToListAsync();
        }

        public async Task<List<City>> GetAllCities()
        {
            return await Context.Cities.ToListAsync();
        }

        public async Task<List<Country>> GetFullCountryInfo(string countryCode)
        {
            return await Context.Countries.Where(x => x.CountryCode == countryCode).Include(x => x.States).ThenInclude(x => x.Cities).ToListAsync();
        }
    }
}