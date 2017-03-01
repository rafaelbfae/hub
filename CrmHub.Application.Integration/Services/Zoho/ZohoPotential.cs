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
using CrmHub.Application.Integration.Models.Zoho;
using Newtonsoft.Json;

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoPotential : ZohoBase
    {
        #region Constantes
        
        private const MessageType.ENTITY ENTITY_TYPE = MessageType.ENTITY.LEAD;

        public const string ENTITY_NAME = "Potentials";
        public const string ENTITY = "Potential";

        #endregion

        #region Constructor

        public ZohoPotential(IHttpMessageSender httpMessageSender, IMessageController messageController) : base(httpMessageSender, messageController) { }

        #endregion

        #region Public Methods

        public bool Execute(ScheduleRoot schedule, List<MappingFields> mapping)
        {
            LeadRoot lead = new LeadRoot { Lead = schedule.Lead, Authentication = schedule.Authentication, MappingFields = mapping };

            if (Execute(lead, mapping.Where(w => FilterEntity(w.Entity)).ToList()))
            {
                if (!schedule.MappingFields.Exists(e => ZohoEvent.Filter(e.Entity) && e.Field.Equals("SEMODULE")))
                    lead.MappingFields.Where(w => ZohoEvent.Filter(w.Entity)).ToList().ForEach(f => { schedule.MappingFields.Add(f); });
                return true;
            }
            return false;
        }

        public bool Execute(LeadRoot lead, List<MappingFields> mapping)
        {
            mapping.Add(new MappingFields { Entity = "Potential", Field = "Closing Date", Value = DateTime.Now.AddMonths(1).ToString("yyy-MM-dd hh:mm:ss") });

            if (string.IsNullOrEmpty(lead.GetId()))
                mapping.Add(new MappingFields { Entity = "Potential", Field = "Stage", Value = "Qualificação" });

            return SendRequestSave(lead, mapping.Where(w => FilterEntity(w.Entity)).ToList(), GetResponse);
        }

        public static bool Filter(string entity) => entity.Equals(ENTITY);

        public string GetIdAccount(Authentication value, string id)
        {
            AccountRoot account = new AccountRoot { Authentication = value };
            if (SendRequestGetRecord(account, id, LoadResponseAccount))
                return account.GetId();
            return string.Empty;
        }

        #endregion

        #region Protected Methods

        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => Filter(entity);

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Potentials, message);
        }

        protected override void SetId(string id, BaseRoot value)
        {
            if (!value.MappingFields.Exists(e => e.Entity.Equals("Event") && e.Field.Equals("SEMODULE")))
            {
                value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "SEID", Value = id });
                value.MappingFields.Add(new MappingFields { Entity = "Event", Field = "SEMODULE", Value = "Potentials" });
            }
        }

        #endregion

        #region Private Methods

        private bool LoadResponseAccount(string response, object value)
        {
            AccountRoot account = (AccountRoot)value;
            try
            {
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