using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PlanningPoker.WebApi.Domain.Models;
using PlanningPoker.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public string SendEmail(string to, string from, string subject, string content)
        {
            try
            {
                MimeMessage message = new MimeMessage()
                {
                    Subject = subject,
                    Body = new TextPart("Plain") { Text = content }
                };
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to.ToString()));

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(
                        "smtp.gmail.com",
                        587,
                        SecureSocketOptions.StartTls
                    );

                    smtp.Authenticate("botcardadmin@compunnel.net", "Bot Card@5862");
                    smtp.Send(message);
                    smtp.Disconnect(true);

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed. Error: {ex.Message}";
            }
        }

        public async Task<string> SendEmailAsync(string to, string from, string subject, string content)
        {
            try
            {
                MimeMessage message = new MimeMessage()
                {
                    Subject = subject,
                    Body = new TextPart("Plain") { Text = content }
                };
                message.From.Add(new MailboxAddress(from));
                message.To.Add(new MailboxAddress(to));

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(
                        "smtp.gmail.com",
                        587,
                        SecureSocketOptions.StartTls
                    );
                    await smtp.AuthenticateAsync("xxxxxx@imoogoo.com", "xxxxxx");
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed. Error: {ex.Message}";
            }
        }
    }

}

