using API.Core.Enums;
using System;

namespace API.Data.Models
{
    public class NotificationModel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime Received { get; set; } = DateTime.UtcNow;
        public NotificationType Type { get; set; }

        public static NotificationModel CreateGameInvite(InviteGameModel model)
        {
            return new NotificationModel
            {
                Sender = model.Sender,
                Recipient = model.Recipient,
                Content = $"Player {model.Sender} invited you to play {model.NameOfGame}",
                Type = NotificationType.GameInvite
            };
        }
    }
}