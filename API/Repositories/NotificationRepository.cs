﻿using API.Data;
using API.Data.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Notification>> GetNotificationsForUser(string username, int limit = 0, bool unreadOnly = false)
        {
            var query = Context.Notifications.Where(x => x.Recipient.Username == username);

            if (unreadOnly)
            {
                query = query.Where(x => !x.IsRead);
            }

            if (limit > 0)
            {
                query = query.Take(limit);
            }

            query = query.Include(x => x.Recipient).Include(x => x.Sender).OrderByDescending(x => x.CreatedDate);

            return await query.ToListAsync();
        }
    }
}