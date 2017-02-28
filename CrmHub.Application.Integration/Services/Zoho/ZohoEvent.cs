using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Roots;
using CrmHub.Application.Integration.Services.Zoho.Base;
using CrmHub.Infra.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CrmHub.Infra.Messages.Models;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Application.Integration.Models.Zoho;
using Newtonsoft.Json;

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoEvent : ZohoBase
    {
        #region Constantes
        
        private const string ENTITY_NAME = "Events";
        private const MessageType.ENTITY ENTITY_TYPE = MessageType.ENTITY.REUNIAO;

        public const string ENTITY = "Event";

        #endregion

        #region Constructor

        public ZohoEvent(IHttpMessageSender httpMessageSender, IMessageController messageController) : base(httpMessageSender, messageController) { }

        #endregion

        #region Public Methods

        public bool Execute(ScheduleRoot schedule, List<MappingFields> mapping)
        {
            EventRoot eventRoot = new EventRoot { Schedule = schedule.Schedule, Authentication = schedule.Authentication };
            return Execute(eventRoot, mapping.Where(w => FilterEntity(w.Entity)).ToList());
        }

        public bool Execute(EventRoot eventRoot, List<MappingFields> mapping)
        {
            return SendRequestSave(eventRoot, mapping, GetResponse);
        }

        public static bool Filter(string entity) => entity.Equals(ENTITY);

        public string GetSubject(ScheduleRoot schedule, string label)
        {
            string leadName = GetFieldValue(schedule, "Last Name", ZohoLead.Filter);
            string dtIni = GetFieldValue(schedule, "Start DateTime", FilterEntity);
            string dtFim = GetFieldValue(schedule, "End DateTime", FilterEntity);

            return string.Format(label, leadName, dtIni, dtFim);
        }

        public string GetIdPotential(Authentication value, string id)
        {
            LeadRoot lead = new LeadRoot { Authentication = value, Lead = new Lead() };
            if (SendRequestGetRecord(lead, id, LoadResponsePotential))
                return lead.GetId();
            return string.Empty;
        }

        #endregion

        #region Protected Methods

        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => Filter(entity);
        protected override void SetId(string id, BaseRoot value) { }

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Events, message);
        }

        #endregion

        #region Private Methods

        private bool LoadResponsePotential(string response, object value)
        {
            LeadRoot lead = (LeadRoot)value;
            try
            {
                var responseObject = JsonConvert.DeserializeObject(response, typeof(Models.Zoho.GetRecord.RootObject));
                if (((Models.Zoho.GetRecord.RootObject)responseObject).response.result.Events.row.FL.Exists(e => e.val.Equals("RELATEDTOID")))
                {
                    lead.Lead.Id = ((Models.Zoho.GetRecord.RootObject)responseObject).response.result.Events.row.FL
                        .Where(e => e.val.Equals("RELATEDTOID")).First().content;
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