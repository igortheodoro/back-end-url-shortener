using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortener.Aplicacao.Models.Link;
using Shortener.Aplicacao.Models.ViewModels;
using Shortener.Aplicacao.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Controllers
{
    public class UrlController : ControllerBase
    {
        private readonly IUrlService service;
        public UrlController(IUrlService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{link}")]
        public IActionResult Get(string link)
        {
            var url = service.Obter(link);

            return Ok(Redirect(url.UrlReal));
        }

        [HttpPost]
        [Authorize]
        [Route("/encurtar")]
        public IActionResult Post([FromBody] CadastrarUrlViewModel url)
        {
            //Pegar o ID do usuário e associar
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            return Created("/encurtar", service.Cadastrar(url, id));
        }


        [HttpPut]
        [Authorize]
        [Route("/encurtar")]
        public IActionResult Put([FromBody] EditarUrlViewModel url)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            var edit = service.Editar(url, id);

            return Accepted("v1", edit);
        }

        [HttpDelete]
        [Authorize]
        [Route("/encurtar")]
        public IActionResult Delete([FromBody] EditarUrlViewModel url)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            service.Deletar(url, id);

            return NoContent();
        }
    }
}
