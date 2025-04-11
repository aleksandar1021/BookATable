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
                Credentials = new NetworkCredential("bookatable12345@gmail.com", "xbem iutk rgzu pkie")
            };
            var message = new MailMessage("bookatable12345@gmail.com", email.SendTo);
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }

        public static void SendUserEmail(UserEmailDTO email)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("bookatable12345@gmail.com", "xbem iutk rgzu pkie")
            };

            var message = new MailMessage
            {
                From = new MailAddress("bookatable12345@gmail.com"),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            message.ReplyToList.Add(new MailAddress(email.Email)); 

            message.To.Add(new MailAddress(email.SendTo));

            smtp.Send(message);
        }
    }

    public class SMTPUserEmailSender : IUserEmailSender
    {

        public  void SendUserEmail(UserEmailDTO email)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("bookatable12345@gmail.com", "xbem iutk rgzu pkie")
            };

            var message = new MailMessage
            {
                From = new MailAddress(email.Email),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true
            };

            message.ReplyToList.Add(new MailAddress(email.Email));

            message.To.Add(new MailAddress(email.SendTo));

            smtp.Send(message);
        }
    }
}
