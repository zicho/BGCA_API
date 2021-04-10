using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Data.Entities.Locations
{
    public class Country : BaseEntity
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("states")]
        public List<State> States { get; set; }
    }
}