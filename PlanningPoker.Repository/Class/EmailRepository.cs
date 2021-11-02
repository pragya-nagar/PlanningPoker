using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningPoker.Repository.Class
{
    public class EmailRepository:IEmailRepository
    {
        private readonly AppDBContext _appDBContext;
        public EmailRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public Email CreateEmail(Email email)
        {
            _appDBContext.Add(email);
            _appDBContext.SaveChanges();
            return email;
        }

        public Email UpdateEmail(Email email)
        {
            _appDBContext.Update(email);
            _appDBContext.SaveChanges();
            return email;
        }

        public Email GetEmail(long emailId)
        {
            return _appDBContext.Email.Find(emailId);   
        }

        public List<Email> GetEmailByStatus()
        {
            var query = _appDBContext.Email.Where(x => x.IsActive == false).ToList();
            return query;
        }
    }
}
