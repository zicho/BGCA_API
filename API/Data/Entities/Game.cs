using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int MinPlayers { get; set; }

        [Required]
        public int MaxPlayers { get; set; }
    }
}