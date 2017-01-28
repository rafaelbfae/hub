using CrmHub.Application.Models.Exact.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmHub.Application.Interfaces.Integration
{
    public interface IHubService
    {
        bool Schedule(ScheduleHub value);
        bool OnSchedule(ScheduleHub value);
        bool ReSchedule(ScheduleHub value);
        bool CancelSchedule(ScheduleHub value);
        bool FeedBackSchedule(ScheduleHub value);

        bool LeadRegister(LeadHub value);
        bool LeadUpdate(LeadHub value);
        bool LeadDelete(LeadHub value);

        bool ContactRegister(ContactHub value);
        bool ContactUpdate(ContactHub value);
        bool ContactDelete(ContactHub value);
    }
}
