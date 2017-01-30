using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IScheduleService
    {
        bool Schedule(ReuniaoExact value);
        bool ReSchedule(ReuniaoExact value);
    }
}
