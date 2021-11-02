using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PlanningPoker.Common.Enums;

namespace PlanningPoker.Repository.Class
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDBContext _context;
        public GameRepository(AppDBContext context)
        {
            _context = context;
        }
        public Game Create(Game createGame)
        {
            _context.Add(createGame);
            _context.SaveChanges();
            return createGame;
        }

        public Game GetById(long id)
        {
            return _context.Game.Find(id);
        }

        public PageResult<Game> GetGames(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var uquery = _context.Game.ToList();
            var skipAmount = pageSize * pageIndex;
            var totalRecords = uquery.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<Game>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;
            var PagedQuery = uquery.OrderBy(x => x.GameId).Skip(skipAmount).Take(pageSize).ToList();
            result.Results = from x in PagedQuery
                             select (new Game
                             {
                                 GameId = x.GameId,
                                 GameName = x.GameName,
                                 Description = x.Description,
                                 IsChangeVote = x.IsChangeVote,
                                 IsDefinitionOfEstimation = x.IsDefinitionOfEstimation,
                                 IsStoryTimer = x.IsStoryTimer,
                                 IsBot = x.IsBot,
                                 IsActive = x.IsActive,
                                 CreatedBy = x.CreatedBy,
                                 CreatedOn = x.CreatedOn,
                                 UpdatedBy = x.UpdatedBy,
                                 UpdatedOn = x.UpdatedOn
                             });
            return result;
        }
        public PageResult<GameDetailsResponse> GetGameDetails(long ScrumMasterId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var uquery = _context.Game.Where(x => x.CreatedBy == ScrumMasterId).ToList();

            var skipAmount = pageSize * pageIndex;
            var totalRecords = uquery.Count;
            var totalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                totalPages = totalPages + 1;

            var result = new PageResult<GameDetailsResponse>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalRecords = totalRecords;
            result.TotalPages = totalPages;

            int startIndex = (pageIndex * pageSize) + 1;
            var PagedQuery = uquery.OrderByDescending(x => x.UpdatedOn).Skip(skipAmount).Take(pageSize).ToList();
            result.Results = (from p in PagedQuery
                              join c in _context.UserStory on p.GameId equals c.GameId into g
                              join s in _context.GameSession on p.GameId equals s.GameId
                              select new GameDetailsResponse
                              {
                                  RowNum = startIndex++,
                                  GameId = p.GameId,
                                  GameName = p.GameName,
                                  UserStories = g.Count(),
                                  LastAccess = p.UpdatedOn,
                                  Status = GameStatusList(s.SessionTime)//GameStatusList(p.GameId)
                              }).ToList();
            return result;
        }
        public string GameStatusList(DateTime sessionTime)
        {
            //var query = _context.GameSession.FirstOrDefault(x => x.GameId == gameid);
            if (sessionTime == DateTime.Now)
                return GameStatus.Start.ToString();
            else if (sessionTime < DateTime.Now)
                return GameStatus.Played.ToString();
            else if (sessionTime > DateTime.Now)
                return GameStatus.Scheduled.ToString();
            else
                return GameStatus.PlayingNow.ToString();
        }
    }
}
