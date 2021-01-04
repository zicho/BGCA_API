using API.Controllers;
using API.Core;
using API.Data.Entities;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class SessionService : ISessionService
    {
        private readonly IBaseRepository<Session> _repository;
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly UserRepository _userRepository;
        private readonly IHubContext<NotificationHub, INotificationHub> _notificationHubContext;

        public SessionService(
            IBaseRepository<Session> repository,
            IBaseRepository<Game> gameRepository,
            UserRepository userRepository,
            IHubContext<NotificationHub, INotificationHub> notificationHubContext)
        {
            _repository = repository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _notificationHubContext = notificationHubContext;
        }

        public async Task<ServiceResponse<Session>> Add(CreateSessionModel newSession)
        {
            try
            {
                if (!newSession.Players.Any())
                {
                    return new ServiceResponse<Session> { Success = false, Message = $"You need to add players to your session." };
                }

                if (newSession.GameId == 0)
                {
                    return new ServiceResponse<Session> { Success = false, Message = $"You need to add a game to your session." };
                }

                var session = new Session();

                foreach (var username in newSession.Players)
                {
                    var user = await _userRepository.GetByUsername(username);

                    if (user != null) session.Players.Add(user);
                    else
                    {
                        return new ServiceResponse<Session> { Success = false, Message = $"Player {username} does not exist" };
                    }
                }

                var game = await _gameRepository.GetById(newSession.GameId);

                if (game != null) session.Game = game;
                else
                {
                    return new ServiceResponse<Session> { Success = false, Message = $"Game ID: {newSession.GameId} does not exist" };
                }

                await _repository.Add(session);
                return new ServiceResponse<Session> { Message = $"Entity of type '{session}' was successfully added." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Session>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<bool>> Invite(InviteGameModel model)
        {
            await _notificationHubContext.Clients.Group(model.Recipient).SendNotice($"Player {model.Sender} invited you to play {model.NameOfGame}");
            return new ServiceResponse<bool>();
        }
    }

    public class CreateSessionModel
    {
        public List<string> Players { get; set; } = new List<string>();
        public int GameId { get; set; }
    }
}
