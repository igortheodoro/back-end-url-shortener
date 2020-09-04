using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shortener.Aplicacao.Models.ViewModels
{
    public class LoginUsuarioViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
