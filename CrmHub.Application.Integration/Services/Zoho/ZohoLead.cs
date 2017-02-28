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

namespace CrmHub.Application.Integration.Services.Zoho
{
    public class ZohoLead : ZohoBase
    {
        #region Constantes

        private const string ENTITY_LEAD = "Lead";
        private const string ENTITY_ADDRESS = "Address";
        private const string ENTITY_NAME = "Leads";
        private const MessageType.ENTITY ENTITY_TYPE = MessageType.ENTITY.LEAD;

        #endregion

        #region Constructor

        public ZohoLead(IHttpMessageSender httpMessageSender, IMessageController messageController) : base(httpMessageSender, messageController) { }

        #endregion

        #region Public Methods

        public static bool Filter(string entity) => entity.Equals(ENTITY_LEAD) || entity.Equals(ENTITY_ADDRESS);

        #endregion

        #region Protected Methods

        protected override string GetEntityName() => ENTITY_NAME;
        protected override MessageType.ENTITY GetEntityType() => ENTITY_TYPE;
        protected override bool FilterEntity(string entity) => Filter(entity);

        protected override void OnLoadResponseGetFields(FieldsResponse.FieldsResponseCrm fieldResponse, MessageType message)
        {
            LoadResponse(fieldResponse.Leads, message);
        }

        protected override void SetId(string id, BaseRoot value) { }

        #endregion

        #region Private Methods
        #endregion
    }
}