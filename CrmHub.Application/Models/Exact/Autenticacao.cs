using CrmHub.Application.Integration.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class Autenticacao
    {
        public string TipoCRM { get; set; }
        public string TokenCRM { get; set; }
        public string UsuarioCRM { get; set; }
        public string UsuarioToken { get; set; }
        public string EmailVendedor { get; set; }
        public eCrmName Crm() { return (eCrmName)Enum.Parse(typeof(eCrmName), TipoCRM); }
    }
}
