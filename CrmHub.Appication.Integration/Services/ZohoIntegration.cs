using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Interfaces.Base;
using System.Collections.Generic;
using CrmHub.Application.Integration.Models;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Infra.Messages.Models;
using CrmHub.Application.Integration.Models.Response;
using static CrmHub.Application.Integration.Models.Zoho.FieldsResponse;

namespace CrmHub.Application.Integration.Services
{
    public class ZohoIntegration : BaseIntegration
    {
        public static eCrmName CRM_NAME = eCrmName.ZOHOCRM;

        #region Attributes

        private IMessageController _messageController;

        #endregion

        public ZohoIntegration(IMessageController messageController)
        {
            _messageController = messageController;
        }

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
            LeadRoot lead = new LeadRoot { Lead = value.Lead, Authentication = value.Authentication };
            return OnExecuteLead(lead, list);
        }

        protected override bool OnExecuteLead(LeadRoot value, List<MappingFields> list)
        {
            string entityName = "Leads";
            return OnSendRequestSave(value, entityName, LoadXml(value, entityName, list));
        }

        protected override bool OnDeleteLead(LeadRoot value)
        {
            return SendRequestDelete(value, "Leads", value.GetId());
        }

        protected override bool OnGetFieldsLead(BaseRoot value)
        {
            return SendRequestGet(value, "Leads");
        }

        protected override bool OnExecuteContact(ScheduleRoot value, Contact contact, List<MappingFields> list)
        {
            ContactRoot contactRoot = new ContactRoot { Contact = contact, Authentication = value.Authentication };
            return OnExecuteContact(contactRoot, list);
        }

        protected override bool OnExecuteContact(ContactRoot value, List<MappingFields> list)
        {
            string entityName = "Contacts";
            return OnSendRequestSave(value, entityName, LoadXml(value, entityName, list));
        }

        protected override bool OnDeleteContact(ContactRoot value)
        {
            return SendRequestDelete(value, "Contacts", value.GetId());
        }

        protected override bool OnGetFieldsContact(BaseRoot value)
        {
            return SendRequestGet(value, "Contacts");
        }

        protected override bool OnExecuteEvent(ScheduleRoot value, List<MappingFields> list)
        {
            list.Add(new MappingFields { ClientEntity = "Reuniao", ClientField = "Assunto", CrmEntity = "Events", CrmField = "Subject" });
            EventRoot reuniao = new EventRoot { Schedule = value.Schedule, Authentication = value.Authentication };
            return OnExecuteEvent(reuniao, list);
        }

        protected override bool OnExecuteEvent(EventRoot value, List<MappingFields> list)
        {
            string entityName = "Events";
            return OnSendRequestSave(value, entityName, LoadXml(value, entityName, list));
        }

        protected override bool OnDeleteEvent(EventRoot value)
        {
            return SendRequestDelete(value, "Events", value.GetId());
        }

        protected override bool OnGetFieldsEvent(BaseRoot value)
        {
            return SendRequestGet(value, "Events");
        }

        protected override bool OnExecuteCompany(CompanyRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
            return OnSendRequestSave(value, entityName, LoadXml(value, entityName, list));
        }

        protected override bool OnDeleteCompany(CompanyRoot value)
        {
            return SendRequestDelete(value, "Accounts", value.GetId());
        }

        protected override bool OnGetFieldsCompany(BaseRoot value)
        {
            return SendRequestGet(value, "Accounts");
        }

        #endregion

        #region Private Methods

        private bool OnSendRequestSave(BaseRoot value, string entityName, string xml)
        {
            if (value.GetId().Equals(string.Empty))
                return SendRequestInsert(value, entityName, xml);
            else
                return SendRequestUpdate(value, entityName, value.GetId(), xml);
        }

        private bool SendRequestGet(BaseRoot value, string entityName)
        {
            string url = "";
            string urlFormat = string.Format("{0}/json/{1}/{2}?authtoken={3}&scope={4}", url, entityName, "getFields", value.Authentication.Token, value.Authentication.User);
            return SendRequestGetAsync(this, urlFormat).Result;
        }

        private bool SendRequestInsert(BaseRoot value, string entityName, string xml)
        {
            string url = "";
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&xmlData={5}", url, entityName, "insertRecords", value.Authentication.Token, value.Authentication.User, xml);
            return SendRequestPostAsync(this, urlFormat, xml).Result;
        }

        private bool SendRequestUpdate(BaseRoot value, string entityName, string id, string xml)
        {
            string url = "";
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&id={5}&xmlData={6}", url, entityName, "updateRecords", value.Authentication.Token, value.Authentication.User, id, xml);
            return SendRequestPostAsync(this, urlFormat, xml).Result;
        }

        private bool SendRequestDelete(BaseRoot value, string entityName, string id)
        {
            string url = "";
            string urlFormat = string.Format("{0}/xml/{1}/{2}?authtoken={3}&scope={4}&id={4}", url, entityName, "deleteRecords", value.Authentication.Token, value.Authentication.User, id);
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
            value.FL.ForEach(v => result.Fields.Add(ConvertFieldCrmToResponse(v)));
            return result;
        }

        private FieldCrm ConvertFieldCrmToResponse(FL value)
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

        private string LoadXml(BaseRoot value, string entityName, List<MappingFields> list)
        {
            string result = string.Format("<{0}><row no=\"1\">", entityName);
            foreach (MappingFields mapping in list)
            {
                result += string.Format("<FL val=\"{0}\">{1}</FL>", mapping.CrmField, GetFieldValue(mapping, value));
            }
            result += string.Format("</row></{0}>", entityName);
            return result;
        }

        #endregion
    }
}
