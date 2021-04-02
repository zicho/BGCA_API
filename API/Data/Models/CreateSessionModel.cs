using System.Collections.Generic;

namespace API.Services
{
    public class CreateSessionModel
    {
        public List<string> Players { get; set; } = new List<string>();
        public int GameId { get; set; }
    }
}