﻿using CrmHub.Application.Integration.Models;
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
            if (base.Execute(schedule))
            {
                AccountRoot account = GetAccount(schedule);
                if (!string.IsNullOrEmpty(schedule.Lead.Id))
                    SendRequestGetRecord(account, ZohoPotential.ENTITY_NAME, schedule.Lead.Id, GetIdByRecord);

                if (Execute(account, account.MappingFields.Where(w => FilterEntity(w.Entity)).ToList()))
                {
                    schedule.Account = new Account { Id = account.Id };
                    account.MappingFields.Where(w => ZohoEvent.Filter(w.Entity)).ToList().ForEach(f => { schedule.MappingFields.Add(f); });
                    account.MappingFields.Where(w => ZohoPotential.Filter(w.Entity)).ToList().ForEach(f => { schedule.MappingFields.Add(f); });
                    return true;
                }
            }
            return false;
        }

        public bool Execute(AccountRoot account, List<MappingFields> mapping)
        {
            if (base.Execute(account))
                return SendRequestSave(account, mapping.Where(w => FilterEntity(w.Entity)).ToList(), GetResponse);
            return false;
        }

        public static bool Filter(string entity) => entity.Equals(ENTITY);

        #endregion

        #region Protected Methods

        protected override string GetEntity() => ENTITY;
        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => Filter(entity);

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Accounts, message);
        }

        protected override bool OnLoadReponseUser(User user, object value)
        {
            BaseRoot baseRoot = (BaseRoot)value;
            if (base.OnLoadReponseUser(user, value))
            {
                Func<string, int, MappingFields> mapping = (e, id) => new MappingFields { Entity = e, Field = "SMOWNERID", Value = user.id, Id = id };
                baseRoot.MappingFields.Add(mapping(ZohoPotential.ENTITY, 0));
                baseRoot.MappingFields.Add(mapping(ZohoEvent.ENTITY, 0));
                int index = 0;
                if (value.GetType() == typeof(ScheduleRoot))
                    ((ScheduleRoot)baseRoot).Contacts.ForEach(f => { baseRoot.MappingFields.Add(mapping(ZohoContact.ENTITY, index++)); });
                return true;
            }
            return false;
        }

        protected override void SetId(string id, BaseRoot value)
        {
            if (!value.MappingFields.Exists(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("ACCOUNTID")))
                value.MappingFields.Add(new MappingFields { Entity = ZohoEvent.ENTITY, Field = "ACCOUNTID", Value = id });
            if (!value.MappingFields.Exists(e => ZohoPotential.Filter(e.Entity) && e.Field.Equals("ACCOUNTID")))
                value.MappingFields.Add(new MappingFields { Entity = ZohoPotential.ENTITY, Field = "ACCOUNTID", Value = id });
        }

        #endregion

        #region Private Methods

        private AccountRoot GetAccount(ScheduleRoot value)
        {
            AccountRoot company = new AccountRoot { Authentication = value.Authentication, MappingFields = value.MappingFields.Where(w => FilterEntity(w.Entity)).ToList() };
            string accountSite =
                GetFieldValue(value, "City", ZohoLead.Filter) + " / " +
                GetFieldValue(value, "State", ZohoLead.Filter) + " - " +
                GetFieldValue(value, "Country", ZohoLead.Filter);
            company.MappingFields.Add(new MappingFields { Entity = ENTITY, Field = "Account Site", Value = accountSite });
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