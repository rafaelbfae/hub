using CrmHub.Application.Models.Exact.Roots.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EmpresaExact : BaseExact<EmpresaExact>
    {
        [Required]
        public Empresa Empresa { get; set; }
    }
}
