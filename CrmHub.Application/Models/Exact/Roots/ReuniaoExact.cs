using CrmHub.Application.Models.Exact.Roots.Base;
using System.Collections.Generic;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class ReuniaoExact : BaseExact<ReuniaoExact>
    {
        public Lead Lead { get; set; }
        public Empresa Empresa { get; set; }
        public Reuniao Reuniao { get; set; }
        public Endereco Endereco { get; set; }
        public List<Contato> Contatos { get; set; }
    }
}
