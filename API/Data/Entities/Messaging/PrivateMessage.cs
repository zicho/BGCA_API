using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Data.Entities.Messaging
{
    public class PrivateMessage : BaseEntity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
