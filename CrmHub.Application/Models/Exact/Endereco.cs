using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Endereco
    {
        public string Rua { get; set; }
        public string CEP { get; set; }
        public string Pais { get; set; }
        public string Maps { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
    }
}
