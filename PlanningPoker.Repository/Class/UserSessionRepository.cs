using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningPoker.Repository.Class
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly AppDBContext _dataContext;
        public UserSessionRepository(AppDBContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<UserSession> Create(long userId, string token)
        {
            var userSessionList = new List<UserSession>();
            var query = _dataContext.UserSession.FirstOrDefault(x => x.UserId == userId);

            if (query is null)
            {
                var userSession = new UserSession();
                userSession.UserId = userId;
                userSession.SessionToken = token;
                userSession.CreatedOn = DateTime.Now;
                userSession.UpdatedOn = DateTime.Now;
                userSession.CreatedBy = userId;
                userSession.UpdatedBy = userId;
                userSession.RowGuid = Guid.NewGuid();
                userSession.IsActive = true;
                userSessionList.Add(userSession);

                foreach (var list in userSessionList)
                {
                    _dataContext.UserSession.Add(list);
                }
                _dataContext.SaveChanges();

            }

            else
            {
                query.SessionToken = token;
                query.IsActive = true;
                query.UpdatedOn = DateTime.Now;
                _dataContext.UserSession.Update(query).Property(x => x.SessionId).IsModified = false; 
                _dataContext.SaveChanges();
            }
            return userSessionList;
        }

        public bool Update(long userId, string token)
        {
            var isLogout = false;
            var userSession = _dataContext.UserSession.FirstOrDefault(x => x.UserId == userId && x.SessionToken == token);
            if (!(userSession is null))
            {
                userSession.IsActive = false;
                userSession.UpdatedOn = DateTime.Now;
                _dataContext.UserSession.Update(userSession).Property(x => x.SessionId).IsModified = false;
                _dataContext.SaveChanges();
                if (userSession.IsActive == false)
                    isLogout = true;
            }
            return isLogout;
        }
    }
}
