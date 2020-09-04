using Microsoft.EntityFrameworkCore;
using Shortener.Aplicacao.Data.Repositories.Interfaces;
using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Shortener.Aplicacao.Data.Repositories
{
    public class UrlRepositorie:IUrlRepositorie
    {
        private readonly AplicacaoContext context;
        public UrlRepositorie(AplicacaoContext context)
        {
            this.context = context;
        }
        public Url Cadastrar(Url url)
        {
            context.Urls.Add(url);
            context.SaveChanges();

            return url;
        }

        public void Deletar(EditarUrlViewModel url, string usuarioId)
        {
            var urlParaDeletar = context.Urls.FirstOrDefault(u => u.Id == url.Id);

            if (urlParaDeletar.UsuarioId == usuarioId)
            {
                context.Urls.Remove(urlParaDeletar);
                context.SaveChanges();
            }
        }

        public Url Editar(EditarUrlViewModel url, string usuarioId)
        {
            var urlParaEditar = context.Urls.FirstOrDefault(u => u.Id == url.Id);

            if (urlParaEditar == null || urlParaEditar.UsuarioId != usuarioId)
            {
                return null;
            }

            urlParaEditar.UrlReal = url.UrlReal;

            context.Entry(urlParaEditar).State = EntityState.Modified;
            context.SaveChanges();

            return urlParaEditar;
        }

        public Url Editar(Url url, string usuarioId)
        {
            var urlParaEditar = context.Urls.FirstOrDefault(u => u.Id == url.Id);

            if (urlParaEditar == null || urlParaEditar.UsuarioId != usuarioId)
            {
                return null;
            }

            context.Entry(url).State = EntityState.Modified;
            context.SaveChanges();

            return urlParaEditar;
        }

        public Url Editar(Url url)
        {
            context.Entry(url).State = EntityState.Modified;
            context.SaveChanges();

            return url;
        }

        public Url Obter(string link)
        {
            return context.Urls
                 .Where(u => u.UrlEncurtada == link)
                 .FirstOrDefault();
        }

        public IEnumerable<Url> ObterUrlsDoUsuario(string id)
        {
            return context.Urls
                .Where(u => u.UsuarioId == id)
                .AsNoTracking()
                .ToList();
        }
    }
}
