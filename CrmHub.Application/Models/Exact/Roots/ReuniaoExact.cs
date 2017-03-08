using CrmHub.Application.Models.Exact.Roots.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class ReuniaoExact : BaseExact<ReuniaoExact>
    {
        [Required]
        public Lead Lead { get; set; }

        public Empresa Empresa { get; set; }

        [Required]
        public Reuniao Reuniao { get; set; }

        public Endereco Endereco { get; set; }

        [Required]
        public List<Contato> Contatos { get; set; }

        public override string GetId() { return Reuniao.Id; }
    }
}
