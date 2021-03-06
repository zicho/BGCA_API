﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities.Users
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required]
        public string Role { get; set; }

        public string Email { get; set; }
        public List<UserFriendship> Friends { get; set; }
        public UserInfo Info { get; set; } = new UserInfo();
    }
}