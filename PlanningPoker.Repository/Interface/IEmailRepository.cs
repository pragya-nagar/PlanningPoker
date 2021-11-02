using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Repository.Interface
{
    public interface IEmailRepository
    {
        Email CreateEmail(Email email);
        Email UpdateEmail(Email email);
        Email GetEmail(long emailId);
        List<Email> GetEmailByStatus();
    }
}
