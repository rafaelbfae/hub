using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Json;
using CrmHub.Application.Integration.Models.Response;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static CrmHub.Application.Integration.Models.Zoho.FieldsResponse;

namespace CrmHub.Application.Integration.Services
{
    public class ZohoIntegration : BaseIntegration
    {
        public static eCrmName CRM_NAME = eCrmName.ZOHOCRM;

        #region Attributes

        private IMessageController _messageController;

        #endregion

        #region Constructor

        public ZohoIntegration(IMessageController messageController)
        {
            _messageController = messageController;
        }

        #endregion

        #region Protect Methods

        protected override IMessageController MessageController
        {
            get
            {
                return _messageController;
            }
        }

        protected override bool OnExecuteLead(ScheduleRoot value, List<MappingFields> list)
        {
            CompanyRoot company = GetCompany(value);
            if (OnExecuteCompany(company, company.MappingFields))
                return OnExecutePotential(value, value.MappingFields.Where(v => filterPotential(v.Entity)).ToList());
            return false;
        }

        protected override bool OnExecuteLead(LeadRoot value, List<MappingFields> list)
        {
            string entityName = "Leads";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
        }

        protected override bool OnDeleteLead(string id, Authentication value)
        {
            return SendRequestDelete(value, "Leads", id);
        }

        protected override bool OnGetFieldsLead(Authentication value)
        {
            return SendRequestGet(value, "Leads");
        }

        protected override bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list, int index = 0)
        {
            ContactRoot contactRoot = new ContactRoot { Contact = contact, Authentication = value.Authentication };

            var emailField = "Email";
            Predicate<MappingFields> filterEmail = m => m.Field.Equals(emailField) && m.Id == index;

            if (list.ToList().Exists(e => filterEmail(e)))
            {
                var email = list.Where(w => filterEmail(w)).First().Value;

                Func<String, bool> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Contacts.row.FL.content.Equals(string.Empty))
                        {
                            contact.Id = ((RootObject)response).response.result.Contacts.row.FL.content;
                            return true;
                        }
                    }
                    catch
                    {  }
                    return false;
                };

                if (SendRequestSearch(value.Authentication, "Contacts", "CONTACTID", emailField, email, loadId))
                    return OnExecuteContact(contactRoot, list, index);
            }
                
            return OnExecuteContact(contactRoot, list, index);
        }

        protected override bool OnExecuteContact(ContactRoot value, List<MappingFields> list, int index = 0)
        {
            string entityName = "Contacts";
            var contacList = list.Where(x => x.Entity == "Contact" && x.Id == index).ToList();
            return OnSendRequestSave(value, entityName, LoadXml(entityName, contacList));
        }

        protected override bool OnDeleteContact(string id, Authentication value)
        {
            return SendRequestDelete(value, "Contacts", id);
        }

        protected override bool OnGetFieldsContact(Authentication value)
        {
            return SendRequestGet(value, "Contacts");
        }

        protected override bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list)
        {
            EventRoot reuniao = new EventRoot { Schedule = value.Schedule, Authentication = value.Authentication };
            return OnExecuteEvent(reuniao, list);
        }

        protected override bool OnExecuteEvent(EventRoot value, List<MappingFields> list)
        {
            string entityName = "Events";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
        }

        protected override bool OnDeleteEvent(string id, Authentication value)
        {
            return SendRequestDelete(value, "Events", id);
        }

        protected override bool OnGetFieldsEvent(Authentication value)
        {
            return SendRequestGet(value, "Events");
        }

        protected override bool OnExecuteCompany(ScheduleRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
        }

        protected override bool OnExecuteCompany(CompanyRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
            var accountField = "Account Name";

            Predicate<MappingFields> filterAccount = m => m.Field.Equals(accountField);
            if (list.Exists(e => filterAccount(e)))
            {
                var accountName = list.Where(w => filterAccount(w)).First().Value;

                Func<String, bool> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Accounts.row.FL.content.Equals(string.Empty))
                        {
                            value.Id = ((RootObject)response).response.result.Accounts.row.FL.content;
                            return true;
                        }
                    }
                    catch
                    { }
                    return false;
                };

                if (SendRequestSearch(value.Authentication, entityName, "ACCOUNTSID", "accountname", accountName, loadId))
                    return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
            }
            
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
        }

        protected override bool OnDeleteCompany(string id, Authentication value)
        {
            return SendRequestDelete(value, "Accounts", id);
        }

        protected override bool OnGetFieldsCompany(Authentication value)
        {
            return SendRequestGet(value, "Accounts");
        }

        protected bool OnExecutePotential(ScheduleRoot value, List<MappingFields> list)
        {
            string entityName = "Potentials";
            var potentialField = "Potential Name";
            list.Add(new MappingFields { Entity = "Potential", Field = "Stage", Value = "Qualificação" });
            list.Add(new MappingFields { Entity = "Potential", Field = "Closing Date", Value = DateTime.Now.ToString("yyy-MM-dd hh:mm:ss") });

            Predicate<MappingFields> filterPotential = m => m.Field.Equals(potentialField);
            if (list.Exists(e => filterPotential(e)))
            {
                var potentialName = list.Where(w => filterPotential(w)).First().Value;
                LeadRoot lead = new LeadRoot { Authentication = value.Authentication, EntityName = entityName, MappingFields = list };
                Func<String, bool> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Potentials.row.FL.content.Equals(string.Empty))
                        {
                            lead.Lead = new Lead();
                            lead.Lead.Id = ((RootObject)response).response.result.Potentials.row.FL.content;
                            return true;
                        }
                    }
                    catch
                    { }
                    return false;
                };

                if (SendRequestSearch(value.Authentication, entityName, "POTENTIALSID", "potentialname", potentialName, loadId))
                    return OnSendRequestSave(lead, entityName, LoadXml(entityName, list));
            }

            return OnSendRequestSave(value, entityName, LoadXml(entityName, list));
        }

        protected override bool GetResponse(string responseBody)
        {
            string idRecord = LoadId(responseBody);

            MessageType message = new MessageType(MessageType.TYPE.ERROR);

            if (IsSuccess(responseBody, idRecord))
            {
                message.Type = MessageType.TYPE.SUCCESS;
                message.Data = new { id = idRecord };
            }

            MessageController.AddMessage(message);
            return message.Type == MessageType.TYPE.SUCCESS;
        }

        protected override string GetSubjectEvent(ScheduleRoot value)
        {
            string leadName = GetFieldValue(value, "Last Name", filterLead);
            string dtIni = GetFieldValue(value, "Start DateTime", filterEvent);
            string dtFim = GetFieldValue(value, "End DateTime", filterEvent);
                 
            return string.Format(labelEvent, leadName, dtIni, dtFim);
        }

        #endregion

        #region Private Methods

        private bool OnSendRequestSave(BaseRoot value, string entityName, string xml)
        {
            if (string.IsNullOrEmpty(value.GetId()))
                return SendRequestInsert(value.Authentication, entityName, xml);

            return SendRequestUpdate(value.Authentication, entityName, value.GetId(), xml);
        }

        private bool SendRequestGet(Authentication value, string entityName)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/json/{1}/{2}?authtoken={3}&scope={4}", url, entityName, "getFields", value.Token, value.User);
            return SendRequestGetAsync(this, urlFormat, s => { return true; }).Result;
        }

        private bool SendRequestSearch(Authentication value, string entityName, string selectColumn, string searchColumn, string searchValue, Func<string, bool> loadResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/json/{1}/{2}?authtoken={3}&scope={4}&selectColumns={5}({6})&searchColumn={7}&searchValue={8}",
                url, entityName, "getSearchRecordsByPDC", value.Token, value.User, entityName, selectColumn, searchColumn.ToLower() , searchValue);
            return SendRequestGetAsync(this, urlFormat, loadResponse).Result;
        }

        private bool SendRequestInsert(Authentication value, string entityName, string xml)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&xmlData={5}", url, entityName, "insertRecords", value.Token, value.User, xml);
            return SendRequestPostAsync(this, urlFormat, xml).Result;
        }

        private bool SendRequestUpdate(Authentication value, string entityName, string id, string xml)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&id={5}&xmlData={6}", url, entityName, "updateRecords", value.Token, value.User, id, xml);
            return SendRequestPostAsync(this, urlFormat, xml).Result;
        }

        private bool SendRequestDelete(Authentication value, string entityName, string id)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&id={4}", url, entityName, "deleteRecords", value.Token, value.User, id);
            return SendRequestDeleteAsync(this, urlFormat).Result;
        }

        private void LoadResponse(EntityResponse value, MessageType message)
        {
            message.Type = MessageType.TYPE.SUCCESS;
            message.Message = string.Empty;
            //message.Data = GetResponseFields(value);
            
        }

        private ResponseFields GetResponseFields(EntityResponse value)
        {
            ResponseFields result = new ResponseFields();
            value.section.ForEach(s => result.Entities.Add(GetEntity(s)));
            return result;
        }

        private ResponseEntity GetEntity(Section value)
        {
            ResponseEntity result = new ResponseEntity();
            result.EntityName = value.name;
            //value.FL.ForEach(v => result.Fields.Add(ConvertFieldCrmToResponse(v)));
            return result;
        }

        private string LoadXml(string entityName, List<MappingFields> list)
        {
            string result = string.Format("<{0}><row no=\"1\">", entityName);

            foreach (MappingFields mapping in list)
                result += string.Format("<FL val=\"{0}\">{1}</FL>", mapping.Field, mapping.Value);

            result += string.Format("</row></{0}>", entityName);
            return result;
        }

        private CompanyRoot GetCompany(ScheduleRoot value)
        {
            CompanyRoot company = new CompanyRoot { Authentication = value.Authentication };
            string leadName = GetFieldValue(value, "Last Name", filterLead);
            company.MappingFields = new List<MappingFields>();
            company.MappingFields.Add(new MappingFields { Entity = "Account", Field = "Account Name", Value = leadName });
            return company;
        }

        private Func<string, bool> filterPotential = v => v.Equals("Potential");

        private static bool IsSuccess(string responseBody, string id)
        {
            return (responseBody.IndexOf("<code>5000</code>") > 0) || !id.Equals(string.Empty);
        }

        private static string LoadId(string value)
        {
            if (value.IndexOf("<FL val=\"Id\">") > 0)
                return value.Substring(value.IndexOf("<FL val=\"Id\">") + 13, 19);
            return string.Empty; ;
        }

        #endregion
    }
}
