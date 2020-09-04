using Shortener.Aplicacao.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Aplicacao.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<RespostaViewModel> RegistrarUsuarioAsync(CadastrarUsuarioViewModel user);
        Task<RespostaViewModel> LoginUsuarioAsync(LoginUsuarioViewModel user);
        Task<RespostaViewModel> ConfirmarEmailAsync(string userId, string token);
        Task<RespostaViewModel> EsqueceuSenhaAsync(string email);
        Task<RespostaViewModel> RedefinirSenhaAsync(RedefinirSenhaViewModel model);

    }
}
