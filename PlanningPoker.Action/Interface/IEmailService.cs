using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanningPoker.Action.Interface
{
    public interface IEmailService
    {
        Email SaveEmail(Email email);
        Email GetEmail(long emailId);
        List<Email> GetEmailByStatus();
    }
}
