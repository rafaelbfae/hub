using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Contact")]
    public class Contato : Base<Contato>
    {
        public string Id { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "First Name")]
        [Crm(eCrmName.ZOHOCRM, "Last Name")]
        public string Nome { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Title")]
        public string Cargo { get; set; }

        [Required]
        [EmailAddress]
        [Crm(eCrmName.ZOHOCRM, "Email")]
        public string Email { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, new string[] { "Phone", "Mobile", "Other Phone" })]
        public List<string> Telefone { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Skype Id")]
        public string IdMensageiro { get; set; }

        public string TipoMensageiro { get; set; }
    }
}
