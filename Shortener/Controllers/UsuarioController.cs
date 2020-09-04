using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using Shortener.Aplicacao.Services.Interfaces;

namespace Shortener.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly IUrlService urlService;
        public UsuarioController(IUsuarioService usuarioService, IUrlService urlService)
        {
            this.usuarioService = usuarioService;
            this.urlService = urlService;
        }

        [HttpPost]
        [Route("/registrar")]
        public async Task<IActionResult> Registrar([FromBody] CadastrarUsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.RegistrarUsuarioAsync(usuario);
                if (result.Success)
                {
                   

                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest();
            
        }

        [HttpPost]
        [Route("/entrar")]
        public async Task<IActionResult> Entrar([FromBody] LoginUsuarioViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.LoginUsuarioAsync(user);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        [Route("/perfil")]
        public IEnumerable<Url> Teste()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var result = urlService.ObterUrlsDoUsuario(id);

            return result;
        }

        [HttpGet]
        [Route("/confirmaremail")]
        public async Task<IActionResult> Confirmar(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await usuarioService.ConfirmarEmailAsync(userId, token);

            if (result.Success)
            {
                // Redirecionar para o perfil
                // return Redirect("/perfil");
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("/esquecisenha")]
        public async Task<IActionResult> Redefinir(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return NotFound();
            }

            var result = await usuarioService.EsqueceuSenhaAsync(email);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("/redefinirsenha")]
        public async Task<IActionResult> Alterar([FromBody] RedefinirSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.RedefinirSenhaAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest();
            }

            return BadRequest();
        }
    }
}
