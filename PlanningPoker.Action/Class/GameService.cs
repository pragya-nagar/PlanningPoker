using Microsoft.Extensions.Logging;
using PlanningPoker.Action.Interface;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Class
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger _logger;
        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }
        public Game Create(Game createGame)
        {
            var result = _gameRepository.Create(createGame);
            this._logger.LogInformation("Game has been Created");
            return result;
        }

        public Game GetById(long id)
        {
            var result = _gameRepository.GetById(id);
            return result;
        }

        public PageResult<GameDetailsResponse> GetGameDetails(long ScrumMasterId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _gameRepository.GetGameDetails(ScrumMasterId, pageIndex, pageSize);
            return result;
        }

        public PageResult<Game> GetGames(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _gameRepository.GetGames(pageIndex, pageSize);
            return result;
        }
    }
}
