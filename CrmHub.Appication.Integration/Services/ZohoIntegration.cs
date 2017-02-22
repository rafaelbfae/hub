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
using System.Text.RegularExpressions;
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
            CompanyRoot company = GetAccount(value);
            if (OnExecuteCompany(company, company.MappingFields))
            {
                company.MappingFields.Where(w => w.Entity.Equals("Event")).ToList().ForEach(f => { value.MappingFields.Add(f); });
                return OnExecutePotential(value, value.MappingFields.Where(v => filterPotential(v.Entity)).ToList());
            }
                
            return false;
        }

        protected override bool OnExecuteLead(LeadRoot value, List<MappingFields> list)
        {
            string entityName = "Leads";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.LEAD, s => { });
        }

        protected override bool OnDeleteLead(string id, Authentication value)
        {
            return SendRequestDelete(value, "Leads", id, MessageType.ENTITY.LEAD, s => { });
        }

        protected override bool OnGetFieldsLead(Authentication value)
        {
            if (SendRequestGet(value, "Leads", LoadResponseFields))
            {
                MessageController.GetMessageSuccess().ForEach(e =>
                {
                    e.Entity = MessageType.ENTITY.LEAD;
                    FieldsResponseCrm data = ((FieldsResponseCrm)(e.Data));
                    if (data.Leads != null)
                        LoadResponse(data.Leads, e);
                });
                return true;
            }
            return false;
        }

        protected override bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list, int index = 0)
        {
            ContactRoot contactRoot = new ContactRoot { Contact = contact, Authentication = value.Authentication };

            var emailField = "Email";
            Predicate<MappingFields> filterEmail = m => m.Field.Equals(emailField) && m.Id == index;

            Action<string> setParticipants = id =>
            {
                if (value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value.IndexOf(id) <= 0)
                    value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value =
                        value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value.Replace("{0}", id + ",{0}");
            };

            Action<string> setId = id =>
            {
                if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("CONTACTID")))
                    value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "CONTACTID", Value = id });

                if (!value.MappingFields.Exists(e => e.Entity.Equals("Account") && e.Field.Equals("CONTACTID")))
                    value.MappingFields.Add(new MappingFields { Entity = "Account", Field = "CONTACTID", Value = id });

                if (!value.MappingFields.Exists(e => e.Entity.Equals("Potential") && e.Field.Equals("CONTACTID")))
                    value.MappingFields.Add(new MappingFields { Entity = "Potential", Field = "CONTACTID", Value = id });

                if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("Participants")))
                    value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "Participants", Value = "<Participant><FL val=\"CONTACTID\">{0}</FL></Participant>" });
                setParticipants(id);
            };

            if (list.ToList().Exists(e => filterEmail(e)))
            {
                var email = list.Where(w => filterEmail(w)).First().Value;

                Predicate<String> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Contacts.row.FL.content.Equals(string.Empty))
                        {
                            contact.Id = ((RootObject)response).response.result.Contacts.row.FL.content;
                            setId(contact.Id);
                            return true;
                        }
                    }
                    catch
                    {  }
                    return false;
                };

                if (SendRequestSearch(value.Authentication, "Contacts", "CONTACTID", emailField, email, loadId))
                    return OnExecuteContact(contactRoot, list, setId, index);
            }
                
            return OnExecuteContact(contactRoot, list, setId, index);
        }

        protected override bool OnExecuteContact(ContactRoot value, List<MappingFields> list, Action<string> setId, int index = 0)
        {
            string entityName = "Contacts";
            var contacList = list.Where(x => x.Entity == "Contact" && x.Id == index).ToList();
            return OnSendRequestSave(value, entityName, LoadXml(entityName, contacList), MessageType.ENTITY.CONTATO, setId);
        }

        protected override bool OnDeleteContact(string id, Authentication value)
        {
            string contactId = string.Empty;

            Predicate<String> loadId = s =>
            {
                try
                {
                    var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                    if (!((RootObject)response).response.result.Contacts.row.FL.content.Equals(string.Empty))
                    {
                        contactId = ((RootObject)response).response.result.Contacts.row.FL.content;
                        return true;
                    }
                }
                catch
                { }
                return false;
            };

            if (SendRequestSearch(value, "Contacts", "CONTACTID", "email", id, loadId))
                return SendRequestDelete(value, "Contacts", contactId, MessageType.ENTITY.CONTATO, s => { });
            return false;
        }

        protected override bool OnGetFieldsContact(Authentication value)
        {
            if (SendRequestGet(value, "Contacts", LoadResponseFields))
            {
                MessageController.GetMessageSuccess().ForEach(e =>
                {
                    e.Entity = MessageType.ENTITY.CONTATO;
                    FieldsResponseCrm data = ((FieldsResponseCrm)(e.Data));
                    if (data.Contacts != null)
                        LoadResponse(data.Contacts, e);
                });
                return true;
            }
            return false;
        }

        protected override bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list)
        {
            EventRoot reuniao = new EventRoot { Schedule = value.Schedule, Authentication = value.Authentication };
            return OnExecuteEvent(reuniao, list);
        }

        protected override bool OnExecuteEvent(EventRoot value, List<MappingFields> list)
        {
            string entityName = "Events";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.REUNIAO, s => { });
        }

        protected override bool OnDeleteEvent(string id, Authentication value)
        {
            return SendRequestDelete(value, "Events", id, MessageType.ENTITY.REUNIAO, s => { });
        }

        protected override bool OnGetFieldsEvent(Authentication value)
        {
            if (SendRequestGet(value, "Events", LoadResponseFields))
            {
                MessageController.GetMessageSuccess().ForEach(e =>
                {
                    e.Entity = MessageType.ENTITY.REUNIAO;
                    FieldsResponseCrm data = ((FieldsResponseCrm)(e.Data));
                    if (data.Events != null)
                        LoadResponse(data.Events, e);
                });
                return true;
            }
            return false;
        }

        protected override bool OnExecuteCompany(ScheduleRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.EMPRESA, s => { });
        }

        protected override bool OnExecuteCompany(CompanyRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
            var accountField = "Account Name";

            Action<string> setId = id =>
            {
                if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("ACCOUNTID")))
                    value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "ACCOUNTID", Value = id });
            };

            Predicate<MappingFields> filterAccount = m => m.Field.Equals(accountField);
            if (list.Exists(e => filterAccount(e)))
            {
                var accountName = list.Where(w => filterAccount(w)).First().Value;

                Predicate<String> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Accounts.row.FL.content.Equals(string.Empty))
                        {
                            value.Id = ((RootObject)response).response.result.Accounts.row.FL.content;
                            setId(value.GetId());
                            return true;
                        }
                    }
                    catch
                    { }
                    return false;
                };

                if (!SendRequestSearch(value.Authentication, entityName, "ACCOUNTSID", "accountname", accountName, loadId))
                    return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.EMPRESA, setId);
            }
            
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.EMPRESA, setId);
        }

        protected override bool OnDeleteCompany(string id, Authentication value)
        {
            return SendRequestDelete(value, "Accounts", id, MessageType.ENTITY.EMPRESA, s => { });
        }

        protected override bool OnGetFieldsCompany(Authentication value)
        {
            if (SendRequestGet(value, "Accounts", LoadResponseFields))
            {
                MessageController.GetMessageSuccess().ForEach(e =>
                {
                    e.Entity = MessageType.ENTITY.EMPRESA;
                    FieldsResponseCrm data = ((FieldsResponseCrm)(e.Data));
                    if (data.Accounts != null)
                        LoadResponse(data.Accounts, e);
                });
                return true;
            }
            return false;
        }

        protected bool OnExecutePotential(ScheduleRoot value, List<MappingFields> list)
        {
            string entityName = "Potentials";
            var potentialField = "Potential Name";

            Action<string> setId = id =>
            {
                if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("SEMODULE")))
                {
                    value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "SEID", Value = id });
                    value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "SEMODULE", Value = "Potentials" });
                }
            };

            list.Add(new MappingFields { Entity = "Potential", Field = "Stage", Value = "Qualificação" });
            list.Add(new MappingFields { Entity = "Potential", Field = "Closing Date", Value = DateTime.Now.AddMonths(1).ToString("yyy-MM-dd hh:mm:ss") });

            Predicate<MappingFields> filterPotential = m => m.Field.Equals(potentialField);
            if (list.Exists(e => filterPotential(e)))
            {
                var potentialName = list.Where(w => filterPotential(w)).First().Value;
                LeadRoot lead = new LeadRoot { Authentication = value.Authentication, EntityName = entityName, MappingFields = list };
                Predicate<String> loadId = s =>
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                        if (!((RootObject)response).response.result.Potentials.row.FL.content.Equals(string.Empty))
                        {
                            lead.Lead = new Lead();
                            lead.Lead.Id = ((RootObject)response).response.result.Potentials.row.FL.content;
                            setId(lead.Lead.Id);
                            return true;
                        }
                    }
                    catch
                    { }
                    return false;
                };

                if (SendRequestSearch(value.Authentication, entityName, "POTENTIALSID", "potentialname", potentialName, loadId))
                    return OnSendRequestSave(lead, entityName, LoadXml(entityName, list), MessageType.ENTITY.LEAD, setId);
            }

            return OnSendRequestSave(value, entityName, LoadXml(entityName, list), MessageType.ENTITY.LEAD, setId);
        }

        protected override bool GetResponse(string responseBody, MessageType.ENTITY entity, Action<string> setId)
        {
            string idRecord = LoadId(responseBody, setId);

            MessageType message = new MessageType(MessageType.TYPE.ERROR, entity);

            if (IsSuccess(responseBody, idRecord))
            {
                message.Type = MessageType.TYPE.SUCCESS;
                message.Data = new { id = idRecord };
            }

            string msg = string.Empty;
            Match match = Regex.Match(responseBody, "(xml/)(\\S*)(\">)|(json/)(\\S*)(\">)");
            if (match.Success)
            {
                msg = match.Groups[2].Value + ": ";
            }

            match = Regex.Match(responseBody, @"(<message>)(...*)(</message>)");
            if (match.Success)
            {
                msg += match.Groups[2].Value;
            }

            message.Message = msg;
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

        private bool OnSendRequestSave(BaseRoot value, string entityName, string xml, MessageType.ENTITY entity, Action<string> setId)
        {
            if (string.IsNullOrEmpty(value.GetId()))
                return SendRequestInsert(value.Authentication, entityName, xml, entity, setId);

            return SendRequestUpdate(value.Authentication, entityName, value.GetId(), xml, entity, setId);
        }

        private bool SendRequestGet(Authentication value, string entityName, Predicate<string> loadResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}json/{1}/{2}?authtoken={3}&scope={4}", url, entityName, "getFields", value.Token, value.User);
            return SendRequestGetAsync(this, urlFormat, loadResponse).Result;
        }

        private bool SendRequestSearch(Authentication value, string entityName, string selectColumn, string searchColumn, string searchValue, Predicate<string> loadResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}json/{1}/{2}?authtoken={3}&scope={4}&selectColumns={5}({6})&searchColumn={7}&searchValue={8}",
                url, entityName, "getSearchRecordsByPDC", value.Token, value.User, entityName, selectColumn, searchColumn.ToLower() , searchValue);
            return SendRequestGetAsync(this, urlFormat, loadResponse).Result;
        }

        private bool SendRequestInsert(Authentication value, string entityName, string xml, MessageType.ENTITY entity, Action<string> setId)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&xmlData={5}", url, entityName, "insertRecords", value.Token, value.User, xml);
            return SendRequestPostAsync(urlFormat, xml, entity, setId).Result;
        }

        private bool SendRequestUpdate(Authentication value, string entityName, string id, string xml, MessageType.ENTITY entity, Action<string> setId)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&id={5}&xmlData={6}", url, entityName, "updateRecords", value.Token, value.User, id, xml);
            return SendRequestPostAsync(urlFormat, xml, entity, setId).Result;
        }

        private bool SendRequestDelete(Authentication value, string entityName, string id, MessageType.ENTITY entity, Action<string> setId)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&id={5}", url, entityName, "deleteRecords", value.Token, value.User, id);
            return SendRequestPostAsync(urlFormat, string.Empty, entity, setId).Result;
        }

        private void LoadResponse(EntityResponse value, MessageType message)
        {
            message.Type = MessageType.TYPE.SUCCESS;
            message.Message = string.Empty;
            message.Data = GetResponseFields(value);
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
            result.EntityName = value.name.Replace("Information", string.Empty).Trim();
            value.FL.ForEach(v => result.Fields.Add(ConvertFieldCrmToResponse(v)));
            return result;
        }

        private FieldCrm ConvertFieldCrmToResponse(Models.Zoho.FieldsResponse.FL value)
        {
            return new FieldCrm()
            {
                Customfield = value.customfield,
                Maxlength = value.maxlength,
                Label = value.label,
                Type = value.type,
                Required = value.req
            };
        }

        private string LoadXml(string entityName, List<MappingFields> list)
        {
            string result = string.Format("<{0}><row no=\"1\">", entityName);

            foreach (MappingFields mapping in list)
                result += string.Format("<FL val=\"{0}\">{1}</FL>", mapping.Field, mapping.Value.Replace(",{0}", string.Empty));

            result += string.Format("</row></{0}>", entityName);
            return result;
        }

        private CompanyRoot GetAccount(ScheduleRoot value)
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

        private static string LoadId(string value, Action<string> setId)
        {
            if (value.IndexOf("<FL val=\"Id\">") > 0)
            {
                string id = value.Substring(value.IndexOf("<FL val=\"Id\">") + 13, 19);
                setId(id);
                return id;
            }
            return string.Empty;
        }

        private bool LoadResponseFields(string response)
        {
            response = RemoveDescription(response);
            try
            {
                var objectResponse = JsonConvert.DeserializeObject(response, typeof(FieldsResponseCrm));
                MessageController.Clear();
                MessageController.AddMessage(
                    new MessageType(MessageType.TYPE.SUCCESS)
                    {
                        Data = (FieldsResponseCrm)objectResponse
                    });
                return true;
            }
            catch (JsonSerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private static string RemoveDescription(string value)
        {
            Regex rgx = new Regex("(,{\"dv\":\")(Descrição | Description)(...*)(Information\"})");
            return rgx.Replace(value, string.Empty);
        }

        #endregion
    }
}
