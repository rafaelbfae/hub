using CrmHub.Appication.Integration.Interfaces.Base;
using CrmHub.Application.Interfaces;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using AutoMapper;
using CrmHub.Appication.Integration.Models;

namespace CrmHub.Application.Services.Integration
{
    public class HubService : IHubService
    {
        private readonly ILogger _logger;
        private readonly ICrmService _crmService;
        private readonly ICrmIntegration _crmIntegration;

        public HubService(ICrmService crmService, ICrmIntegration crmIntegration, ILogger<HubService> logger)
        {
            _logger = logger;
            _crmService = crmService;
            _crmIntegration = crmIntegration;
        }

        public bool Schedule(ScheduleHub value)
        {
            var _value = Mapper.Map<Schedule>(value);
            return _crmIntegration.Schedule(_value);
        }

        public bool OnSchedule(ScheduleHub value)
        {
            var _value = Mapper.Map<Schedule>(value);
            return _crmIntegration.OnSchedule(_value);
        }

        public bool ReSchedule(ScheduleHub value)
        {
            var _value = Mapper.Map<Schedule>(value);
            return _crmIntegration.ReSchedule(_value);
        }

        public bool CancelSchedule(ScheduleHub value)
        {
            var _value = Mapper.Map<Schedule>(value);
            return _crmIntegration.CancelSchedule(_value);
        }

        public bool FeedBackSchedule(ScheduleHub value)
        {
            var _value = Mapper.Map<Schedule>(value);
            return _crmIntegration.FeedBackSchedule(_value);
        }

        public bool LeadRegister(LeadHub value)
        {
            var _value = Mapper.Map<Lead>(value);
            return _crmIntegration.LeadRegister(_value);
        }

        public bool LeadUpdate(LeadHub value)
        {
            var _value = Mapper.Map<Lead>(value);
            return _crmIntegration.LeadUpdate(_value);
        }

        public bool LeadDelete(LeadHub value)
        {
            var _value = Mapper.Map<Lead>(value);
            return _crmIntegration.LeadDelete(_value);
        }

        public bool ContactRegister(ContactHub value)
        {
            var _value = Mapper.Map<Contact>(value);
            return _crmIntegration.ContactRegister(_value);
        }

        public bool ContactUpdate(ContactHub value)
        {
            var _value = Mapper.Map<Contact>(value);
            return _crmIntegration.ContactUpdate(_value);
        }

        public bool ContactDelete(ContactHub value)
        {
            var _value = Mapper.Map<Contact>(value);
            return _crmIntegration.ContactDelete(_value);
        }
    }
}
