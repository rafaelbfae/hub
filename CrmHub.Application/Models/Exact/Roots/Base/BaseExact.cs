using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact.Roots.Base
{
    public class BaseExact
    {
        public Autenticacao Autenticacao { get; set; }
        public List<MapeamentoCampos> MapeamentoCampos { get; set; }
        public List<CamposPersonalizados> CamposPersonalizados { get; set; }
    }
}
