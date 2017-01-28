using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public class MapeamentoCampos
    {
        public string CampoCRM { get; set; }
        public string CampoExact { get; set; }
        public string TipoEntidadeCRM { get; set; }
        public string TipoEntidadeExact { get; set; }
    }
}
