using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPoker.Repository.Interface
{
    public interface IEmailSenderRepository
    {
        string SendEmail(string to, string from, string subject, string content);
        Task<string> SendEmailAsync(string to, string from, string subject, string content);
    }
}
