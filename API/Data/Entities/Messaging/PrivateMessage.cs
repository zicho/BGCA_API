using API.Data.Entities.Users;
using System;

namespace API.Data.Entities.Messaging
{
    public class PrivateMessage : BaseEntity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}