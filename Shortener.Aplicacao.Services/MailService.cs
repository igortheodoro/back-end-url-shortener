using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Shortener.Aplicacao.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Aplicacao.Services
{
    public class MailService : IMailService
    {
        private IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string emailDestino, string subject, string conteudo, string user)
        {
            var apiKey = configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("igortheodoro12@gmail.com", "Url Shortener");
            var to = new EmailAddress(emailDestino, user);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, conteudo, conteudo);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
