using CrmHub.Application.Models.Exact.Roots.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EmpresaExact : BaseExact<EmpresaExact>
    {
        public Empresa Empresa { get; set; }
    }
}
