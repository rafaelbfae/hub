using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string IdMensageiro { get; set; }
        public string TipoMensageiro { get; set; }
        public List<string> Telefone { get; set; }
    }
}
