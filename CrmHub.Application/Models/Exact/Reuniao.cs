using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Reuniao
    {
        public string DataIni { get; set; }
        public string DataFim { get; set; }
        public string Endereco { get; set; }
        public string TimeZone { get; set; }
        public string TipoReuniao { get; set; }
        public string Referencia { get; set; }
    }
}
