using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Helpers.Interfaces;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CrmHub.Application.Integration.Services.Base
{
    public abstract class BaseIntegration
    {
        #region Static

        protected static string labelEvent = "Reunião: {0} {1} - {2}";

        #endregion

        #region Public Methods

        public bool Schedule(ScheduleRoot value)
        {
            value.Schedule.Id = string.Empty;
            return ReSchedule(value);
        }

        public bool ReSchedule(ScheduleRoot value)
        {
            int index = 0;
            bool result = true;
            result &= ExecuteAccount(value);
            if (result)
            {
                value.Contacts.ForEach(c => result &= ExecuteContact(value, c, index++));
                result &= ExecuteLead(value);
                result &= ExecuteEvent(value);
            }
            return result;
        }

        public bool CancelSchedule(string id, Authentication value) => OnCancelSchedule(id, value);

        public bool LeadRegister(LeadRoot value) => ExecuteLead(value);
        public bool LeadUpdate(LeadRoot value) => ExecuteLead(value);
        public bool LeadGetFields(BaseRoot value) => OnGetFieldsLead(value.Authentication);
        public bool LeadDelete(string id, Authentication value) => OnDeleteLead(id, value);

        public bool EventRegister(EventRoot value) => ExecuteEvent(value);
        public bool EventUpdate(EventRoot value) => ExecuteEvent(value);
        public bool EventGetFields(BaseRoot value) => OnGetFieldsEvent(value.Authentication);
        public bool EventDelete(string id, Authentication value) => OnDeleteEvent(id, value);

        public bool ContactRegister(ContactRoot value) => ExecuteContact(value);
        public bool ContactUpdate(ContactRoot value) => ExecuteContact(value);
        public bool ContactGetFields(BaseRoot value) => OnGetFieldsContact(value.Authentication);
        public bool ContactDelete(string email, Authentication value) => OnDeleteContact(email, value);

        public bool AccountRegister(AccountRoot value) => ExecuteAccount(value);
        public bool AccountUpdate(AccountRoot value) => ExecuteAccount(value);
        public bool AccountGetFields(BaseRoot value) => OnGetFieldsAccount(value.Authentication);
        public bool AccountDelete(string id, Authentication value) => OnDeleteAccount(id, value);

        #endregion

        #region Protected Methods

        protected abstract IMessageController MessageController { get; }
        protected abstract IHttpMessageSender HttpMessageSender { get; }
        protected abstract bool OnCancelSchedule(string id, Authentication value);
        protected abstract bool OnExecuteLead(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteLead(LeadRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteLead(string id, Authentication value);
        protected abstract bool OnGetFieldsLead(Authentication value);
        protected abstract bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteEvent(EventRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteEvent(string id, Authentication value);
        protected abstract bool OnGetFieldsEvent(Authentication value);
        protected abstract bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list, int index = 0);
        protected abstract bool OnExecuteContact(ContactRoot value, List<MappingFields> list, Action<string> setId, int index = 0);
        protected abstract bool OnDeleteContact(string id, Authentication value);
        protected abstract bool OnGetIdContact(ContactRoot value);
        protected abstract bool OnGetFieldsContact(Authentication value);
        protected abstract bool OnExecuteAccount(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteAccount(AccountRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteAccount(string id, Authentication value);
        protected abstract bool OnGetFieldsAccount(Authentication value);
        protected abstract string GetSubjectEvent(ScheduleRoot value);

        protected Func<string, bool> filterLead = v => v.Equals("Lead") || v.Equals("Address");
        protected Func<string, bool> filterContact = v => v.Equals("Contact");
        protected Func<string, bool> filterEvent = v => v.Equals("Event");
        protected Func<string, bool> filterAccount = v => v.Equals("Account");

        #endregion

        #region Private Methods

        private bool ExecuteLead(ScheduleRoot value)
        {
            return OnExecuteLead(value, value.MappingFields.Where(v => filterLead(v.Entity)).ToList());
        }

        private bool ExecuteLead(LeadRoot value) => OnExecuteLead(value, value.MappingFields);

        private bool ExecuteEvent(ScheduleRoot value)
        {
            var mapping = value.MappingFields.Where(v => filterEvent(v.Entity)).ToList();
            var subject = mapping.Where(x => x.Field == "Subject").FirstOrDefault();

            if (subject == null)
            {
                value.MappingFields.Add(new MappingFields()
                {
                    Id = 0,
                    Entity = "Event",
                    Field = "Subject",
                    Value = GetSubjectEvent(value)
                });
            }
            else if (string.IsNullOrEmpty(subject.Value))
                subject.Value = GetSubjectEvent(value);

            return OnExecuteEvent(value, mapping);
        }

        private bool ExecuteEvent(EventRoot value) => OnExecuteEvent(value, value.MappingFields);

        private bool ExecuteContact(ScheduleRoot value, Contact contact, int index)
        {
            return OnExecuteContact(value, contact, value.MappingFields.Where(v => filterContact(v.Entity)).ToList(), index);
        }

        private bool ExecuteContact(ContactRoot value)
        {
            if (!value.Contact.Id.Equals(string.Empty))
                OnGetIdContact(value);
            return OnExecuteContact(value, value.MappingFields, s => { });
        }

        private bool ExecuteAccount(ScheduleRoot value)
        {
            return OnExecuteAccount(value, value.MappingFields.Where(v => filterAccount(v.Entity)).ToList());
        }

        private bool ExecuteAccount(AccountRoot value) => OnExecuteAccount(value, value.MappingFields);

        #endregion
    }
}