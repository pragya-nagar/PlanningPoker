using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PlanningPoker.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPoker.Repository.Class
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
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

