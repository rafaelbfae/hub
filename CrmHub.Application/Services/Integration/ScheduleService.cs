using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Infra.Messages.Interfaces;

namespace CrmHub.Application.Services.Integration
{
    public class ScheduleService : IScheduleService
    {
        private readonly IHubService _service;

        public ScheduleService(IHubService service)
        {
            this._service = service;
        }

        public IMessageController MessageController()
        {
            return _service.MessageController();
        }

        public bool Update(ReuniaoExact value)
        {
            return _service.ScheduleUpdate(value);
        }

        public bool Register(ReuniaoExact value)
        {
            return _service.ScheduleRegister(value);
        }

        public bool Fields(Autenticacao value)
        {
            return _service.ScheduleGetFields(value);
        }
    }
}
