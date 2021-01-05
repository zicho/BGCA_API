using API.Data;
using API.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> UserExists(string username)
        {
            return await Context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<User> GetByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task AddFriendRequest(User sender, User recipient)
        {
            var relation = new UserFriendship
            {
                User = sender,
                UserId = sender.Id,
                User2 = recipient,
                User2Id = recipient.Id,
                Status = Core.Enums.RelationshipStatus.Pending,
            };

            await Context.UserFriendships.AddAsync(relation);
            await Context.SaveChangesAsync();
        }
    }
}