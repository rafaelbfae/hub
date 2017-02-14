using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Lead")]
    public class Lead : Base<Lead>
    {
        public string Id { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Last Name")]
        [Crm(eCrmName.ZOHOCRM, "Potential", true, "Potential Name", "Account Name")]
        public string Nome { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Lead Source")]
        public string Origem { get; set; }

        public string Mercado { get; set; }
        public string Produto { get; set; }
        public string SubOrigem { get; set; }
        public string LinkExact { get; set; }
        public string Observacao { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Potential", true, "Description")]
        public string Diagnostico { get; set; }

        public string PreVendedor { get; set; }
        public string DataCadastro { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Phone", "Mobile", "Other Phone")]
        public List<string> Telefone { get; set; }

        public List<Filtro> Filtros { get; set; }
    }
}
