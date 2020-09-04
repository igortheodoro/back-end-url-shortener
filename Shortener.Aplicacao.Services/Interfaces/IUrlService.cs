using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using System.Collections.Generic;

namespace Shortener.Aplicacao.Services.Interfaces
{
    public interface IUrlService
    {
        Url Obter(string link);
        Url Cadastrar(CadastrarUrlViewModel url, string usuarioId);
        Url Editar(EditarUrlViewModel url, string usuarioId);
        void Deletar(EditarUrlViewModel url, string usuarioId);
        IEnumerable<Url> ObterUrlsDoUsuario(string id);
    }
}
