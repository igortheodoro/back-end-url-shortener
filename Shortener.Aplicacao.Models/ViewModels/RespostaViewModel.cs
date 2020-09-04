using System;
using System.Collections.Generic;
using System.Text;

namespace Shortener.Aplicacao.Models.ViewModels
{
    public class RespostaViewModel
    {
        public string Mensagem { get; set; }
        public bool Success { get; set; }
        public Object Objeto { get; set; }
    }
}
