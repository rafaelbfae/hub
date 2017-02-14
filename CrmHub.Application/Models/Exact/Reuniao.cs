using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Event")]
    public class Reuniao : Base<Reuniao>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Subject")]
        public string Assunto { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Start DateTime")]
        public string DataIni { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "End DateTime")]
        public string DataFim { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Venue")]
        public string Endereco { get; set; }

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Description")]
        public string Referencia { get; set; }

        public string TipoReuniao { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}
