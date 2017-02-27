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

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoAccount : ZohoBase
    {
        #region Constantes

        private const string ENTITY = "Account";
        private const string ENTITY_NAME = "Accounts";
        private const MessageType.ENTITY ENTITY_TYPE = MessageType.ENTITY.EMPRESA;

        #endregion

        #region Constructor

        public ZohoAccount(IHttpMessageSender httpMessageSender, IMessageController messageController) : base(httpMessageSender, messageController) { }

        #endregion

        #region Public Methods

        public bool Execute(ScheduleRoot schedule, List<MappingFields> mapping)
        {
            AccountRoot account = GetAccount(schedule);
            if (!string.IsNullOrEmpty(schedule.Lead.Id))
                SendRequestGetRecord(schedule, "Potentials", schedule.Lead.Id, GetIdByRecord);

            if (Execute(account, mapping.Where(w => FilterEntity(w.Entity)).ToList()))
            {
                account.MappingFields.Where(w => w.Entity.Equals("Event")).ToList().ForEach(f => { schedule.MappingFields.Add(f); });
                account.MappingFields.Where(w => w.Entity.Equals("Potential")).ToList().ForEach(f => { schedule.MappingFields.Add(f); });
                return true;
            }
            return true;
        }

        public bool Execute(AccountRoot account, List<MappingFields> mapping)
        {
            return SendRequestSave(account, mapping.Where(w => FilterEntity(w.Entity)).ToList(), GetResponse);
        }

        #endregion

        #region Protected Methods

        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => entity.Equals(ENTITY);

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Accounts, message);
        }

        protected override void SetId(string id, BaseRoot value)
        {
            if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("ACCOUNTID")))
                value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "ACCOUNTID", Value = id });
            if (!value.MappingFields.Exists(e => e.Entity.Equals("Potential") && e.Field.Equals("ACCOUNTID")))
                value.MappingFields.Add(new MappingFields { Entity = "Potential", Field = "ACCOUNTID", Value = id });
        }

        #endregion

        #region Private Methods

        private AccountRoot GetAccount(ScheduleRoot value)
        {
            AccountRoot company = new AccountRoot { Authentication = value.Authentication, MappingFields = value.MappingFields.Where(w => FilterEntity(w.Entity)).ToList() };
            Func<string, bool> filterLead = v => v.Equals("Lead") || v.Equals("Address");
            string accountSite =
                GetFieldValue(value, "City", filterLead) + " / " +
                GetFieldValue(value, "State", filterLead) + " - " +
                GetFieldValue(value, "Country", filterLead);
            company.MappingFields.Add(new MappingFields { Entity = "Account", Field = "Account Site", Value = accountSite });
            return company;
        }

        private bool GetIdByRecord(string response, object value)
        {
            try
            {
                AccountRoot account = (AccountRoot)value;
                var responseObject = JsonConvert.DeserializeObject(response, typeof(Models.Zoho.GetRecord.RootObject));
                if (((Models.Zoho.GetRecord.RootObject)responseObject).response.result.Potentials.row.FL.Exists(e => e.val.Equals("ACCOUNTID")))
                {
                    account.Id = ((Models.Zoho.GetRecord.RootObject)responseObject).response.result.Potentials.row.FL
                        .Where(e => e.val.Equals("ACCOUNTID")).First().content;
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        #endregion
    }
}