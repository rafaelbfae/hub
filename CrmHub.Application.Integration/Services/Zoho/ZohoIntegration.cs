﻿using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Json;
using CrmHub.Application.Integration.Models.Response;
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
using static CrmHub.Application.Integration.Models.Zoho.FieldsResponse;

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoIntegration : BaseIntegration
    {
        public static eCrmName CRM_NAME = eCrmName.ZOHOCRM;

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
            string accountId = string.Empty;
            string potentialId = string.Empty;
            Predicate<String> loadPotentialId = s =>
            {
                try
                {
                    var response = JsonConvert.DeserializeObject(s, typeof(Models.Zoho.GetRecord.RootObject));
                    if (((Models.Zoho.GetRecord.RootObject)response).response.result.Events.row.FL.Exists(e => e.val.Equals("RELATEDTOID")))
                    {
                        potentialId = ((Models.Zoho.GetRecord.RootObject)response).response.result.Events.row.FL
                            .Where(e => e.val.Equals("RELATEDTOID")).First().content;
                        return true;
                    }
                }
                catch
                { }
                return false;
            };

            Predicate<String> loadAccountId = s =>
            {
                try
                {
                    var response = JsonConvert.DeserializeObject(s, typeof(Models.Zoho.GetRecord.RootObject));
                    if (((Models.Zoho.GetRecord.RootObject)response).response.result.Potentials.row.FL.Exists(e => e.val.Equals("ACCOUNTID")))
                    {
                        accountId = ((Models.Zoho.GetRecord.RootObject)response).response.result.Potentials.row.FL
                            .Where(e => e.val.Equals("ACCOUNTID")).First().content;
                        return true;
                    }
                }
                catch
                { }
                return false;
            };

            if (SendRequestGetRecord(value, "Events", id, loadPotentialId))
            {
                if (SendRequestGetRecord(value, "Potentials", potentialId, loadAccountId))
                    return OnDeleteAccount(accountId, value);
            }
                
            return false;
        }

        protected override bool OnExecuteLead(ScheduleRoot value, List<MappingFields> list)
        {
            return OnExecutePotential(value, value.MappingFields.Where(v => filterPotential(v.Entity)).ToList());
        }

        protected override bool OnExecuteLead(LeadRoot value, List<MappingFields> list)
        {
            string entityName = "Potentials";
            return OnSendRequestSave(value, entityName, LoadXml(entityName, list.Where(w => filterPotential(w.Entity)).ToList()), MessageType.ENTITY.LEAD, s => { });
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

            if (value.MappingFields.Exists(e => filterPotential(e.Entity) && e.Field.Equals("ACCOUNTID")))
            {
                string accountId = value.MappingFields.Where(w => filterPotential(w.Entity) && w.Field.Equals("ACCOUNTID")).First().Value;
                list.Add(new MappingFields { Entity = "Contact", Field = "ACCOUNTID", Id = index, Value = accountId });
            }

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

        protected override bool OnGetIdContact(ContactRoot value)
        {
            var emailField = "Email";
            Predicate<String> loadId = s =>
            {
                try
                {
                    var response = JsonConvert.DeserializeObject(s, typeof(RootObject));
                    if (!((RootObject)response).response.result.Contacts.row.FL.content.Equals(string.Empty))
                    {
                        value.Contact.Id = ((RootObject)response).response.result.Contacts.row.FL.content;
                        return true;
                    }
                }
                catch 
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject(s, typeof(Models.Json.Test.RootObject));
                        if (!((Models.Json.Test.RootObject)response).response.result.Contacts.row[0].FL.content.Equals(string.Empty))
                        {
                            value.Contact.Id = ((Models.Json.Test.RootObject)response).response.result.Contacts.row[0].FL.content;
                            return true;
                        }
                    }
                    catch { return false; }
                }
                return false;
            };

            if (SendRequestSearch(value.Authentication, "Contacts", "CONTACTID", emailField, value.Contact.Id, loadId))
                return true;
            return false;
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

            list.Add(new MappingFields { Entity = "Potential", Field = "Closing Date", Value = DateTime.Now.AddMonths(1).ToString("yyy-MM-dd hh:mm:ss") });

            if (!string.IsNullOrEmpty(value.Lead.Id))
            {
                LeadRoot lead = new LeadRoot { Lead = value.Lead, Authentication = value.Authentication };
                return OnSendRequestSave(lead, entityName, LoadXml(entityName, list), MessageType.ENTITY.LEAD, setId);
            }
            else
                list.Add(new MappingFields { Entity = "Potential", Field = "Stage", Value = "Qualificação" });

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

        private ZohoAccount AccountController => new ZohoAccount(HttpMessageSender, MessageController);
        private ZohoEvent EventController => new ZohoEvent(HttpMessageSender, MessageController);

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

        private bool SendRequestGetRecord(Authentication value, string entityName, string id, Predicate<string> loadResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}json/{1}/{2}?authtoken={3}&scope={4}&id={5}",
                url, entityName, "getRecordById", value.Token, value.User, id);
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
            CompanyRoot company = new CompanyRoot { Authentication = value.Authentication, MappingFields = value.MappingFields.Where(w => filterAccount(w.Entity)).ToList() };
            string accountSite = 
                GetFieldValue(value, "City", filterLead) + " / " +
                GetFieldValue(value, "State", filterLead) + " - " +
                GetFieldValue(value, "Country", filterLead);
            company.MappingFields.Add(new MappingFields { Entity = "Account", Field = "Account Site", Value = accountSite });
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