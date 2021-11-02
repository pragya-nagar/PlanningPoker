using PlanningPoker.Action.Interface;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Class
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IUserSessionRepository _userSessionRepository;
        public UserSessionService(IUserSessionRepository userSessionRepository)
        {
            _userSessionRepository = userSessionRepository;
        }

        public IEnumerable<UserSession> Create(long userId, string token)
        {
            var result = _userSessionRepository.Create(userId, token);
            return result;
        }

        public bool Update(long userId, string token)
        {
            var result = _userSessionRepository.Update(userId, token);
            return result;
        }
    }
}
