using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities.Locations
{
    public class State : BaseEntity
    {
        [JsonProperty("state_code")]
        public string StateCode { get; set; }


        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cities")]
        public List<City> Cities { get; set; }
    }
}
