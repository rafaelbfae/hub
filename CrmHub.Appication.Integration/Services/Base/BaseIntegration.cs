using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;
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
            bool result = true;
            if (ExecuteLead(value))
            {
                int index = 0;
                value.Contacts.ForEach(c => result &= ExecuteContact(value, c, index++));
                result &= ExecuteCompany(value);
                result &= ExecuteEvent(value);
            }
            return result;
        }

        public bool LeadRegister(LeadRoot value) => ExecuteLead(value);
        public bool LeadUpdate(LeadRoot value) => ExecuteLead(value);
        public bool LeadDelete(LeadRoot value) => OnDeleteLead(value.GetId(), value.Authentication);
        public bool LeadGetFields(BaseRoot value) => OnGetFieldsLead(value.Authentication);

        public bool EventRegister(EventRoot value) => ExecuteEvent(value);
        public bool EventUpdate(EventRoot value) => ExecuteEvent(value);
        public bool EventDelete(EventRoot value) => OnDeleteEvent(value.GetId(), value.Authentication);
        public bool EventGetFields(BaseRoot value) => OnGetFieldsEvent(value.Authentication);

        public bool ContactRegister(ContactRoot value) => ExecuteContact(value);
        public bool ContactUpdate(ContactRoot value) => ExecuteContact(value);
        public bool ContactDelete(ContactRoot value) => OnDeleteContact(value.GetId(), value.Authentication);
        public bool ContactGetFields(BaseRoot value) => OnGetFieldsContact(value.Authentication);

        public bool CompanyRegister(CompanyRoot value) => ExecuteCompany(value);
        public bool CompanyUpdate(CompanyRoot value) => ExecuteCompany(value);
        public bool CompanyDelete(CompanyRoot value) => OnDeleteCompany(value.GetId(), value.Authentication);
        public bool CompanyGetFields(BaseRoot value) => OnGetFieldsCompany(value.Authentication);

        #endregion

        #region Protected Methods

        protected abstract IMessageController MessageController { get; }
        protected abstract bool OnExecuteLead(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteLead(LeadRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteLead(string id, Authentication value);
        protected abstract bool OnGetFieldsLead(Authentication value);
        protected abstract bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteEvent(EventRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteEvent(string id, Authentication value);
        protected abstract bool OnGetFieldsEvent(Authentication value);
        protected abstract bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list, int index = 0);
        protected abstract bool OnExecuteContact(ContactRoot value, List<MappingFields> list, int index = 0);
        protected abstract bool OnDeleteContact(string id, Authentication value);
        protected abstract bool OnGetFieldsContact(Authentication value);
        protected abstract bool OnExecuteCompany(ScheduleRoot value, List<MappingFields> list);
        protected abstract bool OnExecuteCompany(CompanyRoot value, List<MappingFields> list);
        protected abstract bool OnDeleteCompany(string id, Authentication value);
        protected abstract bool OnGetFieldsCompany(Authentication value);
        protected abstract string GetSubjectEvent(ScheduleRoot value);
        protected abstract bool GetResponse(string responseBody);

        protected Func<string, bool> filterLead = v => v.Equals("Lead") || v.Equals("Address");
        protected Func<string, bool> filterContact = v => v.Equals("Contact");
        protected Func<string, bool> filterEvent = v => v.Equals("Event");
        protected Func<string, bool> filterCompany = v => v.Equals("Company");

        protected async Task<bool> SendRequestGetAsync(BaseIntegration controller, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                //responseBody = RemovoDescription(responseBody);
                //try
                //{
                //    var objectResponse = JsonConvert.DeserializeObject(responseBody, typeof(FieldsResponseCrm));
                //    controller.MessageController.Clear();
                //    controller.MessageController.AddMessage(
                //        new Message()
                //        {
                //            typeMessage = Message.TYPE.SUCCESS,
                //            data = (FieldsResponseCrm)objectResponse
                //        });
                //}
                //catch (JsonSerializationException e)
                //{
                //    Console.WriteLine(e.Message);
                //}
                //catch (AggregateException e)
                //{
                //    Console.WriteLine(e.Message);
                //}

                return true;
            }
        }

        protected async Task<bool> SendRequestPostAsync(BaseIntegration controller, string url, string content)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent xmlQuery = new StringContent(content);
                var response = await httpClient.PostAsync(new Uri(url), xmlQuery);
                response.EnsureSuccessStatusCode();
                return GetResponse(await response.Content.ReadAsStringAsync());
            }
        }

        protected async Task<bool> SendRequestDeleteAsync(BaseIntegration controller, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                return GetResponse(await response.Content.ReadAsStringAsync());
            }
        }

        protected string GetFieldValue(ScheduleRoot value, string field, Func<string, bool> filter)
        {
            var mapping = value.MappingFields.Where(v => filter(v.Entity)).ToList();
            var fieldValue = mapping.Where(x => x.Field == field).FirstOrDefault();
            return fieldValue.Value;
        }

        #endregion

        #region Private Methods

        private bool ExecuteLead(ScheduleRoot value)
        {
            return OnExecuteLead(value, value.MappingFields.Where(v => filterLead(v.Entity)).ToList());
        }

        private bool ExecuteLead(LeadRoot value)
        {
            return OnExecuteLead(value, value.MappingFields);
        }

        private bool ExecuteEvent(ScheduleRoot value)
        {
            var mapping = value.MappingFields.Where(v => filterEvent(v.Entity)).ToList();
            var subject = mapping.Where(x => x.Field == "Subject").FirstOrDefault();

            if(subject == null)
            {
                value.MappingFields.Add(new MappingFields()
                {
                    Id = 0,
                    Entity = "Event",
                    Field = "Subject",
                    Value = "Reunião das" + DateTime.Now.ToString("dd/MM/yyyy hh:mm")
                });
            }
            else if (string.IsNullOrEmpty(subject.Value))
                subject.Value = GetSubjectEvent(value);

            return OnExecuteEvent(value, mapping);
        }

        private bool ExecuteEvent(EventRoot value)
        {
            return OnExecuteEvent(value, value.MappingFields);
        }

        private bool ExecuteContact(ScheduleRoot value, Contact contact, int index)
        {
            return OnExecuteContact(value, contact, value.MappingFields.Where(v => filterContact(v.Entity)).ToList(), index);
        }

        private bool ExecuteContact(ContactRoot value)
        {
            return OnExecuteContact(value, value.MappingFields);
        }

        private bool ExecuteCompany(ScheduleRoot value)
        {
            return OnExecuteCompany(value, value.MappingFields.Where(v => filterCompany(v.Entity)).ToList());
        }

        private bool ExecuteCompany(CompanyRoot value)
        {
            return OnExecuteCompany(value, value.MappingFields);
        }

        #endregion
    }
}
