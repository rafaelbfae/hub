using CrmHub.Application.Models.Exact.Roots.Base;

namespace CrmHub.Application.Models.Exact.Roots
{
    public class EventoExact : BaseExact<EventoExact>
    {
        public Reuniao Reuniao { get; set; }
    }
}
