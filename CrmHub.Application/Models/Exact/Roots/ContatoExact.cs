using CrmHub.Application.Models.Exact.Roots.Base;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class ContatoExact : BaseExact<ContatoExact>
    {
        [Required]
        public Contato Contato { get; set; }

        public override string GetId() { return Contato.Id; }
    }
}
