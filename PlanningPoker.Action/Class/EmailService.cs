using PlanningPoker.Action.Interface;
using PlanningPoker.Domain.Entities;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Class
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        public EmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public Email SaveEmail(Email email)
        {
            var result = new Email();
            if (email is null)
            {
                return null;
            }
            var emailExist = _emailRepository.GetEmail(email.EmailId);
            if (email.EmailId == 0)
            {
                if (emailExist is null)
                {
                    email.CreatedOn = DateTime.Now;
                    email.RowGuid = Guid.NewGuid();
                    result = _emailRepository.CreateEmail(email);
                }
            }
            else if (email.EmailId > 0)
            {
                emailExist.IsActive = email.IsActive;
                emailExist.UpdatedBy = email.UpdatedBy;
                emailExist.UpdatedOn = email.UpdatedOn;
                result = _emailRepository.UpdateEmail(emailExist);
            }
            return result;
        }

        public Email GetEmail(long emailId)
        {
            var result = _emailRepository.GetEmail(emailId);
            return result;
        }

        public List<Email> GetEmailByStatus()
        {
            var result = _emailRepository.GetEmailByStatus();
            return result;
        }
      
    }
}
