using CrmHub.Application.Models.Exact.Roots.Base;
using System.ComponentModel.DataAnnotations;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EventExact : BaseExact<EventExact>
    {
        [Required]
        public Reuniao Reuniao { get; set; }

        public override string GetId() { return Reuniao.Id; }
    }
}
