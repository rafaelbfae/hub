using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.Collections.Generic;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Contact")]
    public class Contato : Base<Contato>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Last Name")]
        public string Nome { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Title")]
        public string Cargo { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Email")]
        public string Email { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Phone", "Mobile", "Other Phone")]
        public List<string> Telefone { get; set; }

        public string IdMensageiro { get; set; }

        public string TipoMensageiro { get; set; }
    }
}
