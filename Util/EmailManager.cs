using Google.Apis.Auth.OAuth2;
using HangfireEmailSchedule.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HangfireEmailSchedule.Util
{
    public class EmailManager{

        public void SendEmail(string name, string to, string from, string text) {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = name;
            message.Body = new TextPart("html") {
                Text = $"From: {name} <br>" +
                $"Contact Information: {from} <br>" +
                $"Messge: {text}"
            };

            using (var client = new SmtpClient()) {
                client.Connect("smtp.mailtrap.io", 2525);
                client.Authenticate("56c23255eaa2fe", "d0c5b22b63775b");
                client.Send(message);
                client.Disconnect(false);
            }

        }
    }
}
