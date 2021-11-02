using Microsoft.AspNetCore.Http;
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
    public class UserStoryService : IUserStoryservice
    {
        private readonly IUserStoryRepository _userStoryRepository;
        public UserStoryService(IUserStoryRepository userStoryRepository)
        {
            _userStoryRepository = userStoryRepository;
        }
        public PageResult<UserStoryInsertRequest> GetByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _userStoryRepository.GetByGameId(gameId, pageIndex, pageSize);
            return result;
        }

        public List<UserStory> Import(IFormFile formFile, int gameId, int userId)
        {
            var result = _userStoryRepository.Import(formFile, gameId, userId);
            return result;
        }

        public PageResult<UserStoryInsertRequest> SearchStory(long gameId, string story, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var result = _userStoryRepository.SearchStory(gameId, story, pageIndex, pageSize);
            return result;
        }

        public UserStory GetByUserStoryId(long userstoryId)
        {
            var result = _userStoryRepository.GetByUserStoryId(userstoryId);
            return result;
        }
    }
}
