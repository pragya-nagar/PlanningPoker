using PlanningPoker.DataContract.Request;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Interface
{
    public interface IUserStoryEstimationService
    {
        UserStoryEstimate UserStoryEstimationDetails(UserEstimationRequest model, long UserId, long GameId, long UserStoryId);
    }
}
