using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Services.Interfaces
{
    public interface IEmailSender
    {
        string SendEmail(string to, string from, string subject, string content);
        Task<string> SendEmailAsync(string to, string from, string subject, string content);
    }
}
