using AutoMapper;
using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace CrmHub.Application.Services.Integration
{
    public class HubService : IHubService
    {
        #region Attributes

        private readonly ILogger _logger;
        private readonly ICrmService _crmService;
        private readonly ICrmIntegration _crmIntegration;
        private readonly IHostingEnvironment _env;

        #endregion

        #region Constructor

        public HubService(ICrmService crmService, ICrmIntegration crmIntegration, ILogger<HubService> logger, IHostingEnvironment env)
        {
            _env = env;
            _logger = logger;
            _crmService = crmService;
            _crmIntegration = crmIntegration;
        }

        #endregion

        #region Public Methods

        public bool ScheduleRegister(ReuniaoExact value)
        {
            
            var _value = Mapper.Map<ScheduleRoot>(value);
            return Execute(_value, (c, v) => c.Schedule(_value));
        }

        public bool ScheduleUpdate(ReuniaoExact value)
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

        public bool CompanyRegister(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactRegister(_value);
        }

        public bool CompanyUpdate(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactUpdate(_value);
        }

        public bool CompanyDelete(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return _crmIntegration.ContactDelete(_value);
        }

        public IMessageController MessageController()
        {
            return _crmIntegration.MessageController;
        }

        private void AddUrl(Authentication authentication)
        {
            var crm = _crmService.GetByName(authentication.Crm, _env.EnvironmentName);
            authentication.UrlService = crm.UrlService;
            authentication.UrlAccount = crm.UrlAccount;
        }

        private bool Execute(BaseRoot value, Func<ICrmIntegration, BaseRoot, bool> function)
        {
            AddUrl(value.Authentication);
            return function(_crmIntegration, value);
        }

        #endregion

    }
}
