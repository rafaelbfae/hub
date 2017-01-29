using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Lead
    {
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Origem { get; set; }
        public string Mercado { get; set; }
        public string Produto { get; set; }
        public string SubOrigem { get; set; }
        public string LinkExact { get; set; }
        public string Observacao { get; set; }
        public string Diagnostico { get; set; }
        public string PreVendedor { get; set; }
        public string DataCadastro { get; set; }
        public List<string> Telefone { get; set; }
        public List<Filtro> Filtros { get; set; }
    }
}
