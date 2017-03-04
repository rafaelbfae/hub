using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Services.Zoho.Base;
using CrmHub.Infra.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using CrmHub.Infra.Messages.Models;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;
using Newtonsoft.Json;
using CrmHub.Application.Integration.Models.Zoho;
using CrmHub.Application.Integration.Models.Json;

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoContact : ZohoBase
    {
        #region Constantes

        private const MessageType.ENTITY ENTITY_TYPE = MessageType.ENTITY.CONTATO;
        private const string FIELD_SEARCH = "email";
        private const string FIELD_SELECT = "CONTACTID";
        private const string FIELD_EMAIL = "Email";
        private const string ACCOUNT_ID = "ACCOUNTID";

        public const string ENTITY = "Contact";
        public const string ENTITY_NAME = "Contacts";

        #endregion

        #region Constructor

        public ZohoContact(IHttpMessageSender httpMessageSender, IMessageController messageController) : base(httpMessageSender, messageController) { }

        #endregion

        #region Public Methods

        public override bool Delete(string email, Authentication value)
        {
            ContactRoot contact = new ContactRoot { Authentication = value, Contact = new Contact() };
            if (SendRequestSearch(contact, GetEntityName(), FIELD_SELECT, FIELD_SEARCH, email, GetResponseSearch))
                return base.Delete(contact.GetId(), value);
            return false;
        }

        public bool Execute(ScheduleRoot schedule, Contact contact, List<MappingFields> mapping, int index = 0)
        {
            Predicate<MappingFields> filterEmail = m => m.Field.Equals(FIELD_EMAIL) && m.Id == index;
            var email = mapping.Where(w => filterEmail(w)).First().Value;

            if (schedule.MappingFields.Exists(e => ZohoPotential.Filter(e.Entity) && e.Field.Equals(ACCOUNT_ID)))
            {
                string accountId = schedule.MappingFields.Where(w => ZohoPotential.Filter(w.Entity) && w.Field.Equals(ACCOUNT_ID)).First().Value;
                mapping.Add(new MappingFields { Entity = ENTITY, Field = ACCOUNT_ID, Id = index, Value = accountId });
            }

            ContactRoot contactRoot = new ContactRoot { Authentication = schedule.Authentication, Contact = contact, MappingFields = schedule.MappingFields };
            SendRequestSearch(contactRoot, GetEntityName(), FIELD_SELECT, FIELD_SEARCH, email, GetResponseSearch);

            if (Execute(contactRoot, mapping, index))
            {
                Predicate<string> filter = s => ZohoEvent.Filter(s) || ZohoPotential.Filter(s) || ZohoAccount.Filter(s);

                if (!schedule.MappingFields.Exists(e => filter(e.Entity) && e.Field.Equals("CONTACTID")))
                    contactRoot.MappingFields.Where(e => filter(e.Entity) && e.Field.Equals("CONTACTID")).ToList().ForEach(f => schedule.MappingFields.Add(f));

                if (!schedule.MappingFields.Exists(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("Participants")))
                    contactRoot.MappingFields.Where(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("Participants")).ToList().ForEach(f => schedule.MappingFields.Add(f));

                return true;
            }
            return false;
        }

        public bool Execute(ContactRoot value, List<MappingFields> mapping, int index = 0)
        {
            return SendRequestSave(value, mapping.Where(w => FilterEntity(w.Entity) && w.Id == index).ToList(), GetResponse);
        }

        public bool GetId(ContactRoot value)
        {
            return SendRequestSearch(value, GetEntityName(), FIELD_SELECT, FIELD_SEARCH, value.GetId(), GetResponseSearch);
        }

        #endregion

        #region Protected Methods

        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => entity.Equals(ENTITY);

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Contacts, message);
        }

        protected override void SetId(string id, BaseRoot value)
        {
            if (!value.MappingFields.Exists(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("CONTACTID")))
                value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "CONTACTID", Value = id });

            if (!value.MappingFields.Exists(e => ZohoAccount.Filter(e.Entity) && e.Field.Equals("CONTACTID")))
                value.MappingFields.Add(new MappingFields { Entity = "Account", Field = "CONTACTID", Value = id });

            if (!value.MappingFields.Exists(e => ZohoPotential.Filter(e.Entity) && e.Field.Equals("CONTACTID")))
                value.MappingFields.Add(new MappingFields { Entity = "Potential", Field = "CONTACTID", Value = id });

            if (!value.MappingFields.Exists(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("Participants")))
                value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "Participants", Value = "<Participant><FL val=\"CONTACTID\">{0}</FL></Participant>" });

            SetParticipants(id, value);
        }

        #endregion

        #region Private Methods

        private bool GetResponseSearch(string response, object value)
        {
            ContactRoot contact = (ContactRoot)value;
            try
            {
                var responseObject = JsonConvert.DeserializeObject(response, typeof(RootObject));
                if (!((RootObject)responseObject).response.result.Contacts.row.FL.content.Equals(string.Empty))
                {
                    contact.Contact.Id = ((RootObject)responseObject).response.result.Contacts.row.FL.content;
                    return true;
                }
            }
            catch
            {
                try
                {
                    var responseObject = JsonConvert.DeserializeObject(response, typeof(Models.Json.Test.RootObject));
                    if (!((Models.Json.Test.RootObject)responseObject).response.result.Contacts.row[0].FL.content.Equals(string.Empty))
                    {
                        contact.Contact.Id = ((Models.Json.Test.RootObject)responseObject).response.result.Contacts.row[0].FL.content;
                        return true;
                    }
                }
                catch { return false; }
            }
            return false;
        }

        private void SetParticipants(string id, BaseRoot value)
        {
            if (value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value.IndexOf(id) <= 0)
                value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value =
                    value.MappingFields.Where(w => w.Entity.Equals("Event") && w.Field.Equals("Participants")).First().Value.Replace("{0}", id + ",{0}");
        }

        #endregion
    }
}