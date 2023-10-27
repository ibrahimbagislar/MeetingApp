using MeetingApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Persistence.Services
{
    public class MailService : IMailService
    {
        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;

            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("info@ibrahimbagislar.com", "Meeting APP", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential("info@ibrahimbagislar.com", "ibraHim123.");
            smtp.Port = 587;
            smtp.EnableSsl = false;
            smtp.Host = "mt-odin-win.guzelhosting.com";
            await smtp.SendMailAsync(mail);
        }
    }
}
