using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Interface
{
    public interface IUserSessionService
    {
        IEnumerable<UserSession> Create(long userId, string token);
        bool Update(long userId, string token);
    }
}
