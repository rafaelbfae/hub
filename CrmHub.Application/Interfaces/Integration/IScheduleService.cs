using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IScheduleService
    {
        bool Schedule(ScheduleHub value);
        bool OnSchedule(ScheduleHub value);
        bool ReSchedule(ScheduleHub value);
        bool CancelSchedule(ScheduleHub value);
        bool FeedBackSchedule(ScheduleHub value);
    }
}
