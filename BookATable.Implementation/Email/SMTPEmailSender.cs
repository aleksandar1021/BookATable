using BookATable.Application.DTO;
using BookATable.Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Email
{
    public class SMTPEmailSender : IEmailSender
    {
        public void SendEmail(EmailDTO email)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("bookatable12345@gmail.com", "telx aufz yqkc qhkt")
            };
            var message = new MailMessage("bookatable12345@gmail.com", email.SendTo);
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
