using System;
using System.Collections.Generic;

using CrmHub.Model.Crm;
using CrmHub.Model.User;
using CrmHub.Model.Schedule;
using CrmHub.Infra.Message;

using CrmHub.Zoho;
using CrmHub.HubSpot;

namespace CrmHub.Model 
{
    public class HubController
    {
        #region Attributes

        private Dictionary<CRM, CrmController> _crmHub = new Dictionary<CRM, CrmController>();
        private MessageController _messageController = new MessageController();

        #endregion

        #region Constructor

        public HubController() 
        {
            LoadCRM();
        }

        #endregion

        #region Properties

        public MessageController messageController 
        {
            get { return this._messageController; }
        }

        protected Dictionary<CRM, CrmController> crmHub 
        {
            get { return this._crmHub; }
        }

        #endregion

        #region Public Methods

        public bool Schedule(ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.Schedule(v));
        }

        public bool ReSchedule(ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.ReSchedule(v));
        }

        public bool CancelSchedule(ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.CancelSchedule(v));
        }

        public bool FeedBackSchedule(ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.FeedBackSchedule(v));
        }

        #endregion

        #region Private Methods
        
        private void LoadCRM() 
        {
            crmHub.Add(ZohoController.crm, new ZohoController(this));
            crmHub.Add(HubSpotController.crm, new HubSpotController(this));
        }

        private bool Execute(ScheduleValue value, Func<CrmController, ScheduleValue, bool> function)
        {
            CRM crm = GetCRMUser(value.user);

            if (!crm.Equals(CRM.NONE)) 
            {
                try 
                {
                    return function(GetCRMController(crm), value); 
                }
                catch (KeyNotFoundException e) {}
            }

            return false;
        }

        private CrmController GetCRMController(CRM value) 
        {
            return crmHub[value];
        }

        private CRM GetCRMUser(UserValue user)
        {
            return user.crm;
        }

        #endregion

    }
}