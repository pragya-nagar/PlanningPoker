using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPoker.Action.Interface
{
    public interface IEmailSenderService
    {
        string SendEmail(string to, string from, string subject, string content);
        Task<string> SendEmailAsync(string to, string from, string subject, string content);
    }
}
