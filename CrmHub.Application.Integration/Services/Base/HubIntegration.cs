using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Integration.Services.Zoho;
using CrmHub.Infra.Helpers.Interfaces;
using CrmHub.Infra.Messages.Interfaces;
using System;
using System.Collections.Generic;

namespace CrmHub.Application.Integration.Services.Base
{
    public class HubIntegration : IHubIntegration
    {
        #region Attributes

        private IHttpMessageSender _httpMessageSender;
        private IMessageController _messageController;
        private Dictionary<eCrmName, Type> _crmHub = new Dictionary<eCrmName, Type>();

        #endregion

        #region Constructor

        public HubIntegration(IHttpMessageSender httpMessageSender, IMessageController messageController)
        {
            _httpMessageSender = httpMessageSender;
            _messageController = messageController;
            LoadCRM();
        }

        #endregion

        #region Public Methods

        public IHttpMessageSender HttpMessageSender
        {
            get
            {
                return _httpMessageSender;
            }
        }

        public IMessageController MessageController
        {
            get
            {
                return _messageController;
            }
        }

        public bool Schedule(ScheduleRoot value) => Execute(value, (c, v) => c.Schedule((ScheduleRoot)v));
        public bool ReSchedule(ScheduleRoot value) => Execute(value, (c, v) => c.ReSchedule((ScheduleRoot)v));
        public bool CancelSchedule(string id, Authentication value) => ExecuteById(id, value, (c, i, v) => c.CancelSchedule(i, v));

        public bool LeadRegister(LeadRoot value) => Execute(value, (c, v) => c.LeadRegister((LeadRoot)v));
        public bool LeadUpdate(LeadRoot value) => Execute(value, (c, v) => c.LeadUpdate((LeadRoot)v));
        public bool LeadGetFields(BaseRoot value) => Execute(value, (c, v) => c.LeadGetFields(v));
        public bool LeadDelete(string id, Authentication value) => ExecuteById(id, value, (c, i, v) => c.LeadDelete(i, v));

        public bool ContactRegister(ContactRoot value) => Execute(value, (c, v) => c.ContactRegister((ContactRoot)v));
        public bool ContactUpdate(ContactRoot value) => Execute(value, (c, v) => c.ContactUpdate((ContactRoot)v));
        public bool ContactGetFields(BaseRoot value) => Execute(value, (c, v) => c.ContactGetFields(v));
        public bool ContactDelete(string email, Authentication value) => ExecuteById(email, value, (c, i, v) => c.ContactDelete(i, v));

        public bool EventRegister(EventRoot value) => Execute(value, (c, v) => c.EventRegister((EventRoot)v));
        public bool EventUpdate(EventRoot value) => Execute(value, (c, v) => c.EventUpdate((EventRoot)v));
        public bool EventGetFields(BaseRoot value) => Execute(value, (c, v) => c.EventGetFields(v));
        public bool EventDelete(string id, Authentication value) => ExecuteById(id, value, (c, i, v) => c.EventDelete(i, v));

        public bool AccountRegister(AccountRoot value) => Execute(value, (c, v) => c.AccountRegister((AccountRoot)v));
        public bool AccountUpdate(AccountRoot value) => Execute(value, (c, v) => c.AccountUpdate((AccountRoot)v));
        public bool AccountGetFields(BaseRoot value) => Execute(value, (c, v) => c.AccountGetFields(v));
        public bool AccountDelete(string id, Authentication value) => ExecuteById(id, value, (c, i, v) => c.AccountDelete(i, v));

        private void LoadCRM()
        {
            _crmHub.Add(ZohoIntegration.CRM_NAME, typeof(ZohoIntegration));
        }

        private BaseIntegration CrmController(eCrmName value) => (BaseIntegration)Activator.CreateInstance(_crmHub[value], HttpMessageSender, MessageController);

        private bool Execute(BaseRoot value, Func<BaseIntegration, BaseRoot, bool> function)
        {
            return Exec(value.Authentication.Crm, (c) => function(c, value));
        }

        private bool ExecuteById(string id, Authentication value, Func<BaseIntegration, string, Authentication, bool> function)
        {
            return Exec(value.Crm, (c) => function(c, id, value));
        }

        private bool Exec(eCrmName crm, Func<BaseIntegration, bool> function)
        {
            try
            {
                return function(CrmController(crm));
            }
            catch (KeyNotFoundException e)
            {
                _messageController.AddErrorMessage(e.Message);
            }
            return false;
        }

        #endregion
    }
}