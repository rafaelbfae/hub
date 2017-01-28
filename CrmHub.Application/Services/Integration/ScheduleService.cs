using CrmHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public bool CancelSchedule(ScheduleHub value)
        {
            return _service.CancelSchedule(value);
        }

        public bool FeedBackSchedule(ScheduleHub value)
        {
            return _service.FeedBackSchedule(value);
        }

        public bool OnSchedule(ScheduleHub value)
        {
            return _service.OnSchedule(value);
        }

        public bool ReSchedule(ScheduleHub value)
        {
            return _service.ReSchedule(value);
        }

        public bool Schedule(ScheduleHub value)
        {
            return _service.Schedule(value);
        }
    }
}
