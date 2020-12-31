using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Models
{
    public class FriendRequestModel
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
    }
}
