﻿using CrmHub.Application.Models.Exact.Roots.Base;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EmpresaExact : BaseExact<EmpresaExact>
    {
        [Required]
        public Empresa Empresa { get; set; }

        public override string GetId() { return Empresa.Id; }
    }
}
