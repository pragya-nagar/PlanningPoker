using Microsoft.AspNetCore.Http;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Repository.Interface
{
    public interface IUserStoryRepository
    {
        List<UserStory> Import(IFormFile formFile, int gameId, int userId);
        PageResult<UserStoryInsertRequest> GetByGameId(long gameId, int pageIndex = 0, int pageSize = int.MaxValue);
        PageResult<UserStoryInsertRequest> SearchStory(long gameId, string story, int pageIndex = 0, int pageSize = int.MaxValue);
        UserStory GetByUserStoryId(long userstoryId);
    }
}
