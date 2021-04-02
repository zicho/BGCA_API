using API.Core.Enums;

namespace API.Data.Models
{
    public class NotificationModel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
    }
}