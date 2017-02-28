using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Services.Integration
{
    public class EventService : IEventService
    {
        private readonly IHubService _service;

        public EventService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(ReuniaoExact value)
        {
            return _service.EventUpdate(value);
        }

        public bool Register(ReuniaoExact value)
        {
            return _service.EventRegister(value);
        }

        public bool Delete(string id, Autenticacao value)
        {
            return _service.EventDelete(id, value);
        }

        public bool Fields(Autenticacao value)
        {
            return _service.EventGetFields(value);
        }
    }
}
