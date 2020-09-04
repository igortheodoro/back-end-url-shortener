using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shortener.Aplicacao.Models.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NovaSenha { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmarSenha { get; set; }
    }
}
