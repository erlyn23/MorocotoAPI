using Microsoft.Extensions.Configuration;
using Morocoto.Infraestructure.Dtos.Responses;
using Morocoto.Infraestructure.Tools.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Tools
{
    public class EmailTools: IEmailTools
    {
        private readonly IConfiguration _configuration;
        public EmailTools(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailWithInfoAsync(string userEmail, string subject, string body)
        {
            string appEmail = _configuration["EmailAccount:AppEmail"];
            string appEmailPassword = _configuration["EmailAccount:AppEmailPassword"];

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(appEmail, appEmailPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(appEmail, "Morocoto App");
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
