using API.Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities
{
    public class Session : BaseEntity
    {
        [Required]
        public Game Game { get; set; }
        [Required]
        public List<User> Players { get; set; } = new List<User>();
        public DateTime Date { get; set; }
    }
}
