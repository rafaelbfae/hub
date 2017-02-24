using CrmHub.Application.Models.Exact.Roots.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class ContatoExact : BaseExact<ContatoExact>
    {
        [Required]
        public Contato Contato { get; set; }
    }
}
