using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.Collections.Generic;


namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Lead")]
    public class Lead : Base<Lead>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Fist Name")]
        [Crm(eCrmName.ZOHOCRM, "Last Name")]
        public string Nome { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Lead Source")]
        public string Origem { get; set; }

        public string Mercado { get; set; }
        public string Produto { get; set; }
        public string SubOrigem { get; set; }
        public string LinkExact { get; set; }
        public string Observacao { get; set; }
        public string Diagnostico { get; set; }
        public string PreVendedor { get; set; }
        public string DataCadastro { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Phone", "Mobile", "Other Phone")]
        public List<string> Telefone { get; set; }

        public List<Filtro> Filtros { get; set; }
    }
}
