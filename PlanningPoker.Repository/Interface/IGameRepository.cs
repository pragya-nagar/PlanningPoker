using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Repository.Interface
{
    public interface IGameRepository
    {
        Game Create(Game createGame);
        PageResult<Game> GetGames(int pageIndex = 0, int pageSize = int.MaxValue);
        Game GetById(long id);
        PageResult<GameDetailsResponse> GetGameDetails(long ScrumMasterId, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
