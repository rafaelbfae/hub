using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Interfaces;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;

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

        public bool Schedule(ReuniaoExact value)
        {
            var _value = Mapper.Map<ScheduleRoot>(value);
            return _crmIntegration.Schedule(_value);
        }

        public bool ReSchedule(ReuniaoExact value)
        {
            var _value = Mapper.Map<ScheduleRoot>(value);
            return _crmIntegration.ReSchedule(_value);
        }

        public bool LeadRegister(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return _crmIntegration.LeadRegister(_value);
        }

        public bool LeadUpdate(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return _crmIntegration.LeadUpdate(_value);
        }

        public bool LeadDelete(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return _crmIntegration.LeadDelete(_value);
        }

        public bool ContactRegister(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactRegister(_value);
        }

        public bool ContactUpdate(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactUpdate(_value);
        }

        public bool ContactDelete(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactDelete(_value);
        }
    }
}
