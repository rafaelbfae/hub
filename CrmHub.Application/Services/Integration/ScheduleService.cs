using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Services.Integration
{
    public class ScheduleService : IScheduleService
    {
        private readonly IHubService _service;

        public ScheduleService(IHubService service)
        {
            this._service = service;
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
