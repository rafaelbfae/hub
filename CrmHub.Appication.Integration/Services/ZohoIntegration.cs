using CrmHub.Application.Integration.Enuns;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Response;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Application.Integration.Services.Base;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;
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

        protected override bool OnExecuteCompany(CompanyRoot value, List<MappingFields> list)
        {
            string entityName = "Accounts";
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

        #endregion

        #region Private Methods

        private bool OnSendRequestSave(BaseRoot value, string entityName, string xml)
        {
            if (string.IsNullOrEmpty(value.GetId()))
            {
                return SendRequestInsert(value.Authentication, entityName, xml);
            }

            return SendRequestUpdate(value.Authentication, entityName, value.GetId(), xml);
        }

        private bool SendRequestGet(Authentication value, string entityName)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}/json/{1}/{2}?authtoken={3}&scope={4}", url, entityName, "getFields", value.Token, value.User);
            return SendRequestGetAsync(this, urlFormat).Result;
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

        private string LoadXml(string entityName, List<MappingFields> list)
        {
            string result = string.Format("<{0}><row no=\"1\">", entityName);
            foreach (MappingFields mapping in list)
            {
                result += string.Format("<FL val=\"{0}\">{1}</FL>", mapping.Field, mapping.Value);
            }
            result += string.Format("</row></{0}>", entityName);
            return result;
        }

        #endregion
    }
}
