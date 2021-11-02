using PlanningPoker.DataContract.Request;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Repository.Class
{
    public class UserStoryEstimationRepository : IUserStoryEstimationRepository
    {
        private readonly AppDBContext _dataContext;
        public UserStoryEstimationRepository(AppDBContext dataContext)
        {
            _dataContext = dataContext;
        }
        public UserStoryEstimate UserStoryEstimationDetails(UserEstimationRequest model, long UserId, long GameId, long UserStoryId)
        {
            var query = from i in _dataContext.InviteUser
                        join u in _dataContext.UserStory on i.GameId equals u.GameId
                        select new { i.UserId, u.UserStoryId, i.GameId };
            var uquery = query.FirstOrDefault(x => x.UserId == UserId && x.GameId == GameId && x.UserStoryId == UserStoryId);
            if (!(uquery is null))
            {
                var estimate = new UserStoryEstimate();
                estimate.Userid = uquery.UserId;
                estimate.UserStoryId = uquery.UserStoryId;
                estimate.KeyWord = model.KeyWord;
                estimate.KeyWordTypeId = (Int32)model.KeywordType;
                estimate.BotPoints = model.BotPoints;
                estimate.UserPoints = model.UserPoints;
                estimate.Reason = model.Reason;
                _dataContext.Add(estimate);
                _dataContext.SaveChanges();
                return estimate;
            }
            else
            {
                return new UserStoryEstimate();
            }
        }
    }
}

