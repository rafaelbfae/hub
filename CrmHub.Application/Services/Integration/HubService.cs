using AutoMapper;
using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Models.Exact.Roots;
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
        private readonly IHubIntegration _crmIntegration;
        private readonly IHostingEnvironment _env;

        #endregion

        #region Constructor

        public HubService(ICrmService crmService, IHubIntegration crmIntegration, ILogger<HubService> logger, IHostingEnvironment env)
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
            return Execute(_value, (c, v) => c.ReSchedule(_value));
        }

        public bool ScheduleGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.EventGetFields(_value));
        }

        public bool LeadRegister(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return Execute(_value, (c, v) => c.LeadRegister(_value));
        }

        public bool LeadUpdate(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return Execute(_value, (c, v) => c.LeadUpdate(_value));
        }

        public bool LeadDelete(LeadExact value)
        {
            var _value = Mapper.Map<LeadRoot>(value);
            return Execute(_value, (c, v) => c.LeadDelete(_value));
        }

        public bool LeadGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.LeadGetFields(_value));
        }

        public bool ContactRegister(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return Execute(_value, (c, v) => c.ContactRegister(_value));
        }

        public bool ContactUpdate(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return Execute(_value, (c, v) => c.ContactUpdate(_value));
        }

        public bool ContactDelete(ContatoExact value)
        {
            var _value = Mapper.Map<ContactRoot>(value);
            return Execute(_value, (c, v) => c.ContactDelete(_value));
        }

        public bool ContactGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.CompanyGetFields(_value));
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

        private bool Execute(BaseRoot value, Func<IHubIntegration, BaseRoot, bool> function)
        {
            string msg = string.Format("Execute CRM:{0} ", value.Authentication.Crm);
            _logger.LogDebug(msg);
            AddUrl(value.Authentication);
            return function(_crmIntegration, value);
        }

        #endregion

    }
}
