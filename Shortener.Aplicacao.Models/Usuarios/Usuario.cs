using System.ComponentModel.DataAnnotations;

namespace Shortener.Aplicacao.Models.Usuarios
{
    public class Usuario
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

    }   
}
