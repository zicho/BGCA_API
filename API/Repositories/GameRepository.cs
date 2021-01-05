using API.Data;
using API.Data.Entities;

namespace API.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(DataContext context) : base(context)
        {
        }
    }
}