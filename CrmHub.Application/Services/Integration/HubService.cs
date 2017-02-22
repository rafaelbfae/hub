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

        #region Scheduler

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

        public bool ScheduleDelete(string id, Autenticacao value)
        {
            var _value = Mapper.Map<Authentication>(value);
            return ExecuteById(id, _value, (c, i, v) => c.EventDelete(id, _value));
        }

        #endregion

        #region Lead

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

        public bool LeadDelete(string id, Autenticacao value)
        {
            var _value = Mapper.Map<Authentication>(value);
            return ExecuteById(id, _value, (c, i, v) => c.LeadDelete(id, _value));
        }

        public bool LeadGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.LeadGetFields(_value));
        }

        #endregion

        #region Contact

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

        public bool ContactDelete(string email, Autenticacao value)
        {
            var _value = Mapper.Map<Authentication>(value);
            return ExecuteById(email, _value, (c, i, v) => c.ContactDelete(email, _value));
        }

        public bool ContactGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.CompanyGetFields(_value));
        }

        #endregion

        #region Company

        public bool CompanyRegister(EmpresaExact value)
        {
            var _value = Mapper.Map<CompanyRoot>(value);
            return Execute(_value, (c, v) => c.CompanyRegister(_value));
        }

        public bool CompanyUpdate(EmpresaExact value)
        {
            var _value = Mapper.Map<CompanyRoot>(value);
            return Execute(_value, (c, v) => c.CompanyUpdate(_value));
        }

        public bool CompanyDelete(string id, Autenticacao value)
        {
            var _value = Mapper.Map<Authentication>(value);
            return ExecuteById(id, _value, (c, i, v) => c.CompanyDelete(id, _value));
        }

        public bool CompanyGetFields(Autenticacao value)
        {
            var _value = new BaseRoot() { Authentication = Mapper.Map<Authentication>(value) };
            return Execute(_value, (c, v) => c.CompanyGetFields(_value));
        }

        #endregion

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

        private bool ExecuteById(string id, Authentication value, Func<IHubIntegration, string, Authentication, bool> function)
        {
            string msg = string.Format("Execute By Id CRM:{0} ", value.Crm);
            _logger.LogDebug(msg);
            AddUrl(value);
            return function(_crmIntegration, id, value);
        }

        #endregion

    }
}
