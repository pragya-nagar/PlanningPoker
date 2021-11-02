using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PlanningPoker.Action.Interface;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPoker.Action.Class
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailSenderRepository _emailSenderRepository;
        public EmailSenderService(IEmailSenderRepository emailSenderRepository)
        {
            _emailSenderRepository = emailSenderRepository;
        }
        public string SendEmail(string to, string from, string subject, string content)
        {
            var result = _emailSenderRepository.SendEmail(to, from, subject, content);
            return result;
        }

        public async Task<string> SendEmailAsync(string to, string from, string subject, string content)
        {
            var result = _emailSenderRepository.SendEmail(to, from, subject, content);
            return result;
        }
    }
}

