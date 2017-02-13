using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Interfaces.Base;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;
using System;
using System.Collections.Generic;

namespace CrmHub.Application.Integration.Services.Base
{
    public class HubIntegration : IHubIntegration
    {
        #region Attributes

        private IMessageController _messageController;
        private Dictionary<eCrmName, Type> _crmHub = new Dictionary<eCrmName, Type>();

        #endregion

        #region Constructor

        public HubIntegration(IMessageController messageController)
        {
            _messageController = messageController;
            LoadCRM();
        }

        #endregion

        #region Public Methods

        public IMessageController MessageController
        {
            get
            {
                return _messageController;
            }
        }

        public bool Schedule(ScheduleRoot value) => Execute(value, (c, v) => c.Schedule((ScheduleRoot)v));
        public bool ReSchedule(ScheduleRoot value) => Execute(value, (c, v) => c.ReSchedule((ScheduleRoot)v));

        public bool LeadRegister(LeadRoot value) => Execute(value, (c, v) => c.LeadRegister((LeadRoot)v));
        public bool LeadUpdate(LeadRoot value) => Execute(value, (c, v) => c.LeadUpdate((LeadRoot)v));
        public bool LeadDelete(LeadRoot value) => Execute(value, (c, v) => c.LeadDelete((LeadRoot)v));
        public bool LeadGetFields(BaseRoot value) => Execute(value, (c, v) => c.LeadGetFields(v));

        public bool ContactRegister(ContactRoot value) => Execute(value, (c, v) => c.ContactRegister((ContactRoot)v));
        public bool ContactUpdate(ContactRoot value) => Execute(value, (c, v) => c.ContactUpdate((ContactRoot)v));
        public bool ContactDelete(ContactRoot value) => Execute(value, (c, v) => c.ContactDelete((ContactRoot)v));
        public bool ContactGetFields(BaseRoot value) => Execute(value, (c, v) => c.ContactGetFields(v));

        public bool EventRegister(EventRoot value) => Execute(value, (c, v) => c.EventRegister((EventRoot)v));
        public bool EventUpdate(EventRoot value) => Execute(value, (c, v) => c.EventUpdate((EventRoot)v));
        public bool EventDelete(EventRoot value) => Execute(value, (c, v) => c.EventDelete((EventRoot)v));
        public bool EventGetFields(BaseRoot value) => Execute(value, (c, v) => c.EventGetFields(v));

        public bool CompanyRegister(CompanyRoot value) => Execute(value, (c, v) => c.CompanyRegister((CompanyRoot)v));
        public bool CompanyUpdate(CompanyRoot value) => Execute(value, (c, v) => c.CompanyUpdate((CompanyRoot)v));
        public bool CompanyDelete(CompanyRoot value) => Execute(value, (c, v) => c.CompanyDelete((CompanyRoot)v));
        public bool CompanyGetFields(BaseRoot value) => Execute(value, (c, v) => c.CompanyGetFields(v));

        private void LoadCRM()
        {
            _crmHub.Add(ZohoIntegration.CRM_NAME, typeof(ZohoIntegration));
        }

        private BaseIntegration CrmController(eCrmName value) => (BaseIntegration)Activator.CreateInstance(_crmHub[value], this._messageController);

        private bool Execute(BaseRoot value, Func<BaseIntegration, BaseRoot, bool> function)
        {
            eCrmName crm = value.Authentication.Crm;
            try
            {
                return function(CrmController(crm), value);
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