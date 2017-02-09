using CrmHub.Application.Interfaces.Integration;
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

        public bool ReSchedule(ReuniaoExact value)
        {
            return _service.ReSchedule(value);
        }

        public bool Schedule(ReuniaoExact value)
        {
            return _service.Schedule(value);
        }
    }
}
