using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Entities.Locations
{
    public class City : BaseEntity
    {
        [JsonIgnore]
        public State State { get; set; }

        [ForeignKey(nameof(State))]
        public int StateId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
}