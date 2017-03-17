using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact
{
    [Crm(eCrmName.ZOHOCRM, "Event")]
    public class Reuniao : Base<Reuniao>
    {
        public string Id { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Subject")]
        public string Assunto { get; set; }

        private DateTime dataIni; 

        [Required]
        [Crm(eCrmName.ZOHOCRM, "Start DateTime", "yyy-MM-dd hh:mm:ss")]
        public DateTime DataIni
        {
            get { return ConvertTimeZone(dataIni); }
            set { dataIni = value; }
        }

        private DateTime dataFim;

        [Required]
        [Crm(eCrmName.ZOHOCRM, "End DateTime", "yyy-MM-dd hh:mm:ss")]
        public DateTime DataFim
        {
            get { return ConvertTimeZone(dataFim); }
            set { dataFim = value; }
        }

        [Crm(eCrmName.ZOHOCRM, "Venue")]
        public string Endereco { get; set; }

        [Crm(eCrmName.ZOHOCRM, "Description")]
        public string Referencia { get; set; }

        public string TipoReuniao { get; set; }

        [Required]
        public string TimeZone { get; set; }

        private DateTime ConvertTimeZone(DateTime value)
        {
            return TimeZoneInfo.ConvertTime(value.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById(TimeZone));
        }
    }
}
