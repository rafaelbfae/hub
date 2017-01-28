using CrmHub.Appication.Integration.Enuns;
using CrmHub.Appication.Integration.Interfaces.Base;
using CrmHub.Appication.Integration.Models;
using CrmHub.Appication.Integration.Models.Base;
using CrmHub.Infra.Messages.Interfaces;
using System;
using System.Collections.Generic;

namespace CrmHub.Appication.Integration.Services.Base
{
    public class HubIntegration : ICrmIntegration
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

        public bool Schedule(Schedule value)
        {
            return Execute(value, (c, v) => c.Schedule((Schedule)v));
        }

        public bool OnSchedule(Schedule value)
        {
            return Execute(value, (c, v) => c.OnSchedule((Schedule)v));
        }

        public bool ReSchedule(Schedule value)
        {
            return Execute(value, (c, v) => c.ReSchedule((Schedule)v));
        }

        public bool CancelSchedule(Schedule value)
        {
            return Execute(value, (c, v) => c.CancelSchedule((Schedule)v));
        }

        public bool FeedBackSchedule(Schedule value)
        {
            return Execute(value, (c, v) => c.FeedBackSchedule((Schedule)v));
        }

        public bool LeadRegister(Lead value)
        {
            return Execute(value, (c, v) => c.LeadRegister((Lead)v));
        }

        public bool LeadUpdate(Lead value)
        {
            return Execute(value, (c, v) => c.LeadUpdate((Lead)v));
        }

        public bool LeadDelete(Lead value)
        {
            return Execute(value, (c, v) => c.LeadDelete((Lead)v));
        }

        public bool ContactRegister(Contact value)
        {
            return Execute(value, (c, v) => c.ContactRegister((Contact)v));
        }

        public bool ContactUpdate(Contact value)
        {
            return Execute(value, (c, v) => c.ContactUpdate((Contact)v));
        }

        public bool ContactDelete(Contact value)
        {
            return Execute(value, (c, v) => c.ContactDelete((Contact)v));
        }

        public bool GetFields(EntityBase value)
        {
            return Execute(value, (c, v) => c.GetFields(v));
        }

        #endregion

        #region Public Methods

        private void LoadCRM()
        {
            _crmHub.Add(ZohoIntegration.CRM_NAME, typeof(ZohoIntegration));
        }

        private ICrmIntegration CrmController(eCrmName value) => (ICrmIntegration)Activator.CreateInstance(_crmHub[value], this._messageController);

        private bool Execute(EntityBase value, Func<ICrmIntegration, EntityBase, bool> function)
        {
            Crm crm = value.User.Crm;
            try
            {
                return function(CrmController(crm.Name), value);
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