using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace PlanningPoker.Common
{
    public static class Healper
    {
        private const string EmailAddressSeparator = ",";

        /// <summary>
        /// Send Email to the given recipients
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachmentPath"></param>
        /// <param name="cc"></param>
        public static void SendEmail(string from, string to, string cc = null, string bcc = null, string subject = null, string body = null, string attachmentPath = null)
        {
            MimeMessage message = new MimeMessage()
            {
                Subject = subject,
                Body = new TextPart(TextFormat.Html) { Text = body },
            };
            message.From.Add(new MailboxAddress(@from));

            message.To.Add(new MailboxAddress(to.ToString()));
            if (!string.IsNullOrEmpty(cc))
                message.Cc.Add(new MailboxAddress(cc.ToString()));
            if (!string.IsNullOrEmpty(bcc))
                message.Cc.Add(new MailboxAddress(cc.ToString()));

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
                smtp.Authenticate("botcardadmin@compunnel.net", "Bot Card@5862");
                smtp.Send(message);
                smtp.Disconnect(true);

            }
            return;
        }
    }
}
