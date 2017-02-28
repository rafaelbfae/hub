using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Json;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Infra.Helpers.Interfaces;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoIntegration : BaseIntegration
    {
        #region Static

        public static eCrmName CRM_NAME = eCrmName.ZOHOCRM;

        #endregion

        #region Attributes

        private IHttpMessageSender _httpMessageSender;
        private IMessageController _messageController;

        #endregion

        #region Constructor

        public ZohoIntegration(IHttpMessageSender httpMessageSender, IMessageController messageController)
        {
            _httpMessageSender = httpMessageSender;
            _messageController = messageController;
        }

        #endregion

        #region Protect Methods

        protected override IHttpMessageSender HttpMessageSender
        {
            get
            {
                return _httpMessageSender;
            }
        }

        protected override IMessageController MessageController
        {
            get
            {
                return _messageController;
            }
        }

        protected override bool OnCancelSchedule(string id, Authentication value)
        {
            string idPotential = EventController.GetIdPotential(value, id);
            if (!idPotential.Equals(string.Empty))
            {
                string idAccount = PotentialController.GetIdAccount(value, idPotential);
                if (!idAccount.Equals(string.Empty))
                    return AccountController.Delete(idAccount, value);
                  
            }
            return false;
        }

        protected override bool OnExecuteLead(ScheduleRoot value, List<MappingFields> list)
        {
            return OnExecutePotential(value, value.MappingFields.Where(v => filterPotential(v.Entity)).ToList());
        }

        protected override bool OnExecuteLead(LeadRoot value, List<MappingFields> list)
        {
            return PotentialController.Execute(value, list);
        }

        protected override bool OnDeleteLead(string id, Authentication value)
        {
            return PotentialController.Delete(id, value);
        }

        protected override bool OnGetFieldsLead(Authentication value)
        {
            return PotentialController.GetFields(value);
        }

        protected override bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list, int index = 0)
        {
            return ContactController.Execute(value, contact, list, index);
        }

        protected override bool OnExecuteContact(ContactRoot value, List<MappingFields> list, Action<string> setId, int index = 0)
        {
            return ContactController.Execute(value, list, index);
        }

        protected override bool OnGetIdContact(ContactRoot value)
        {
            return ContactController.GetId(value);
        }

        protected override bool OnDeleteContact(string id, Authentication value)
        {
            return ContactController.Delete(id, value);
        }

        protected override bool OnGetFieldsContact(Authentication value)
        {
            return ContactController.GetFields(value);
        }

        protected override bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list)
        {
            return EventController.Execute(value, list);
        }

        protected override bool OnExecuteEvent(EventRoot value, List<MappingFields> list)
        {
            return EventController.Execute(value, list);
        }

        protected override bool OnDeleteEvent(string id, Authentication value)
        {
            return EventController.Delete(id, value);
        }

        protected override bool OnGetFieldsEvent(Authentication value)
        {
            return EventController.GetFields(value);
        }

        protected override bool OnExecuteAccount(ScheduleRoot value, List<MappingFields> list)
        {
            return AccountController.Execute(value, list.Where(w => filterAccount(w.Entity)).ToList());
        }

        protected override bool OnExecuteAccount(AccountRoot value, List<MappingFields> list)
        {
            return AccountController.Execute(value, list);
        }

        protected override bool OnDeleteAccount(string id, Authentication value)
        {
            return AccountController.Delete(id, value);
        }

        protected override bool OnGetFieldsAccount(Authentication value)
        {
            return AccountController.GetFields(value);
        }

        protected bool OnExecutePotential(ScheduleRoot value, List<MappingFields> list)
        {
            return PotentialController.Execute(value, list.Where(w => filterPotential(w.Entity)).ToList());
        }

        protected override string GetSubjectEvent(ScheduleRoot value)
        {
            return EventController.GetSubject(value, labelEvent);
        }

        #endregion

        #region Private Methods

        private ZohoAccount AccountController => new ZohoAccount(HttpMessageSender, MessageController);
        private ZohoEvent EventController => new ZohoEvent(HttpMessageSender, MessageController);
        private ZohoPotential PotentialController => new ZohoPotential(HttpMessageSender, MessageController);
        private ZohoContact ContactController => new ZohoContact(HttpMessageSender, MessageController);

        private Func<string, bool> filterPotential = v => v.Equals("Potential");

        #endregion
    }
}