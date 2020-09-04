using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Aplicacao.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string emailDestino, string subject, string conteudo, string user);
    }
}
