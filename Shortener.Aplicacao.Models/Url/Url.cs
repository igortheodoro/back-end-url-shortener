using Shortener.Aplicacao.Models.Usuarios;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Aplicacao.Models.Link
{
    public class Url
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32)]
        public string UrlEncurtada { get; set; }

        [Required, MaxLength(1024)]
        public string UrlReal { get; set; }

        [Required, MaxLength(32)]
        public int Acessos { get; set; }

        public string UsuarioId { get; set; }
    }
}
