using PlanningPoker.Action.Interface;
using PlanningPoker.DataContract.Request;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Class
{
    class UserStoryEstimationService : IUserStoryEstimationService
    {
        private readonly IUserStoryEstimationRepository _userStoryEstimationRepository;
        public UserStoryEstimationService(IUserStoryEstimationRepository userStoryEstimationRepository)
        {
            _userStoryEstimationRepository = userStoryEstimationRepository;
        }
        public UserStoryEstimate UserStoryEstimationDetails(UserEstimationRequest model, long UserId, long GameId, long UserStoryId)
        {
            var result = _userStoryEstimationRepository.UserStoryEstimationDetails(model, UserId, GameId, UserStoryId);
            return result;
        }
    }
}
