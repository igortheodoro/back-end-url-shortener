using System;
using Shortener.Aplicacao.Services.Interfaces;
using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using Shortener.Aplicacao.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Shortener.Aplicacao.Services
{
    public class UrlService:IUrlService
    {
        private readonly IUrlRepositorie repositorie;

        public UrlService(IUrlRepositorie repositorie)
        {
            this.repositorie = repositorie;
        }

        private string GerarUrlEncurtada(Url url)
        {
            Random rnd = new Random();

            string consoantes = "bcdfghjklmnpqrstvwxyz" +
                "BCDFGHJKLMNPQRSTVWXYZ";
            string vogais = "aeiouAEIOU";

            string urlEncurtada;

            urlEncurtada = ""
                + consoantes[rnd.Next(0, 41)]
                + url.Id
                + vogais[rnd.Next(0, 10)]
                + consoantes[rnd.Next(0, 41)];

            return urlEncurtada;
        }

        public Url Cadastrar(CadastrarUrlViewModel url, string usuarioId)
        {
            if (url == null)
            {
                return null;
            }

            Url urlValida = new Url()
            {
                Acessos = 0,
                UrlEncurtada = "",
                UsuarioId = usuarioId,
                UrlReal = url.UrlReal
            };

            var urlCadastrada = repositorie.Cadastrar(urlValida);

            urlCadastrada.UrlEncurtada = GerarUrlEncurtada(urlCadastrada);

            repositorie.Editar(urlCadastrada, usuarioId);
                
            return urlCadastrada;
        }

        public void Deletar(EditarUrlViewModel url, string id)
        {
            repositorie.Deletar(url, id);
        }

        public Url Editar(EditarUrlViewModel url, string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                return null;
            }
            return repositorie.Editar(url, usuarioId);
        }

        public Url Obter(string link)
        {
            if (link == null || link == string.Empty)
            {
                return null;
            }

            Url url = repositorie.Obter(link);

            if (url == null)
            {
                return new Url()
                {
                    UrlReal = "https://localhost:44373/v1"
                };
            }

            url.Acessos++;
            repositorie.Editar(url);

            return url;
        }

        public IEnumerable<Url> ObterUrlsDoUsuario(string id)
        {
            if (id == null || id == string.Empty)
            {
                return null;
            }

            var result = repositorie.ObterUrlsDoUsuario(id);

            return result;
        }
    }
}
