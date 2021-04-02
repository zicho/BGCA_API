using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities.Locations
{
    public class Country  : BaseEntity
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("states")]
        public List<State> States { get; set; }
    }
}
