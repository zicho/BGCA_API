using API.Data.Entities;
using API.Repositories.Interfaces;
using AutoMapper.Configuration;

namespace API.Services
{
    public class GameService : BaseService<Game>
    {
        private readonly IBaseRepository<Game> _repository;
        private readonly IConfiguration _configuration;

        public GameService(IBaseRepository<Game> repository, IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
            _repository = repository;
        }
    }
}
