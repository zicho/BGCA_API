using API.Data;
using API.Data.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class MessageRepository : BaseRepository<PrivateMessage>
    {
        public MessageRepository(DataContext context) : base(context)
        {
        }

        //private async Task<List<PrivateMessage>> GetUnread(string username)
        //{
        //    return await Context.Messages.Where(x => !x.IsRead && x.Recipient.Username == username).Include(x => x.Recipient).Include(x => x.Sender).ToListAsync();
        //}

        //private async Task<List<PrivateMessage>> GetAll(string username)
        //{
        //    return await Context.Messages.Where(x => !x.IsRead && x.Recipient.Username == username).Include(x => x.Recipient).Include(x => x.Sender).ToListAsync();
        //}

        public async Task<List<PrivateMessage>> GetMessagesForUser(string username, int limit = 0, bool unreadOnly = false)
        {
            var query = Context.Messages.Where(x => x.Recipient.Username == username);

            if (unreadOnly)
            {
                query = query.Where(x => !x.IsRead);
            }

            if (limit != 0)
            {
                query = query.Take(limit);
            }

            return query.Include(x => x.Recipient).Include(x => x.Sender).OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}