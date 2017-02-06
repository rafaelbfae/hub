using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Event")]
    public class Reuniao : Base<Reuniao>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Subject")]
        public string Assunto { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Start Date Time")]
        public string DataIni { get; set; }

        [Crm(eCrmName.ZOHOCRM, "End Date Time")]
        public string DataFim { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Venue")]
        public string Endereco { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Description")]
        public string Referencia { get; set; }

        public string TipoReuniao { get; set; }
        public string TimeZone { get; set; }
    }
}
