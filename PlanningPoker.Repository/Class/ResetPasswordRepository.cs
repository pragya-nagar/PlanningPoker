using System;
using System.Collections.Generic;
using System.Text;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Domain.Entities;
using System.Linq;

namespace PlanningPoker.Repository.Class
{
    public class ResetPasswordRepository : IResetPasswordRepository
    {
        private readonly AppDBContext _appDbContext;
        public ResetPasswordRepository(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public long AddForUser(long id, string email, string jwttoken)
        {
            var uquery = _appDbContext.ResetPassword.ToList();
            var tokenQuery = uquery.FirstOrDefault(x => x.UserId == id);

            if (tokenQuery != null)
            {
                tokenQuery.ResetToken = jwttoken;
                this._appDbContext.Update(tokenQuery);
                this._appDbContext.SaveChanges();
                return tokenQuery.ResetPasswordId;
            }

            else
            {
                var token = new ResetPassword();
                token.UserId = id;
                token.ResetToken = jwttoken;
                token.CreatedOn = DateTime.Now;
                token.UpdatedOn = DateTime.Now;
                this._appDbContext.Add(token);
                this._appDbContext.SaveChanges();
                return token.ResetPasswordId;
            }

        }
        public string GetToken(string jwttoken)
        {
            var query = _appDbContext.ResetPassword.ToList();
            var tokenquery = _appDbContext.ResetPassword
                .Where(p => p.ResetToken == jwttoken)
                .Select(p => p.ResetToken).First();
            if (tokenquery != null)
            {
                return tokenquery;
            }
            else
            {
                return null;
            }
        }

    }
}
