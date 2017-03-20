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
        [Crm(eCrmName.ZOHOCRM, "Account", new string[] { "Account Name" })]
        [Crm(eCrmName.ZOHOCRM, "Potential", new string[] { "Potential Name", "Account Name" })]
        public string Nome { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Account", new string[] { "Website" })]
        public string Site { get; set; }

        public string Email { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Lead Source")]
        public string Origem { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Account", new string[] { "Description" })]
        [Crm(eCrmName.ZOHOCRM, "Potential", new string[] { "Description" })]
        public string Diagnostico { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Account", new string[] { "Phone" })]
        [Crm(eCrmName.ZOHOCRM, new string[] { "Phone", "Mobile", "Other Phone" })]
        public List<string> Telefone { get; set; }

        public string Mercado { get; set; }
        public string Produto { get; set; }
        public string SubOrigem { get; set; }
        public string LinkExact { get; set; }
        public string Observacao { get; set; }
        public string PreVendedor { get; set; }
        public string DataCadastro { get; set; }
        public List<Filtro> Filtros { get; set; }
    }
}
