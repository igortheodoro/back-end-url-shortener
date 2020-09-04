using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shortener.Aplicacao.Models.ViewModels
{
    public class CadastrarUsuarioViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50), MinLength(5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50), MinLength(5)]
        public string ConfirmPassword { get; set; }
    }
}
