using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using System.Collections.Generic;

namespace Shortener.Aplicacao.Data.Repositories.Interfaces
{
    public interface IUrlRepositorie
    {
        Url Obter(string link);
        Url Cadastrar(Url url);
        Url Editar(EditarUrlViewModel url, string usuarioId);
        Url Editar(Url url, string usuarioId);
        Url Editar(Url url);
        void Deletar(EditarUrlViewModel url, string usuarioId);
        IEnumerable<Url> ObterUrlsDoUsuario(string id);
    }
}
