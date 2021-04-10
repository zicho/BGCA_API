using API.Core.Enums;
using API.Data.Entities.Users;
using System;

namespace API.Data.Entities.Messaging
{
    public class Notification : BaseEntity
    {
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public DateTime SendDate { get; set; } = DateTime.UtcNow;
    }
}