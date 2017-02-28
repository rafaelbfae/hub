﻿using CrmHub.Application.Integration.Models;
using CrmHub.Application.Integration.Models.Response;
using CrmHub.Application.Integration.Models.Roots.Base;
using CrmHub.Infra.Helpers.Interfaces;
using CrmHub.Infra.Messages.Interfaces;
using CrmHub.Infra.Messages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static CrmHub.Application.Integration.Models.Zoho.FieldsResponse;

namespace CrmHub.Application.Integration.Services.Zoho.Base
{
    public abstract class ZohoBase
    {
        #region Attributes

        private IHttpMessageSender _httpMessageSender;
        private IMessageController _messageController;

        #endregion

        #region Constructor

        public ZohoBase(IHttpMessageSender httpMessageSender, IMessageController messageController)
        {
            _httpMessageSender = httpMessageSender;
            _messageController = messageController;
        }

        #endregion

        #region Public Methods

        public bool Delete(string id, Authentication value) => SendRequestDelete(value, id, GetResponse);

        public bool GetFields(Authentication value)
        {
            if (SendRequestGetFields(value, LoadResponseFields))
            {
                MessageController.GetMessageSuccess().ForEach(e =>
                {
                    e.Entity = MessageType.ENTITY.EMPRESA;
                    OnLoadResponseGetFields(((FieldsResponseCrm)(e.Data)), e);
                });
                return true;
            }
            return false;
        }

        #endregion

        #region Protected Methods

        protected IHttpMessageSender HttpMessageSender => _httpMessageSender;
        protected IMessageController MessageController => _messageController;

        protected abstract string GetEntityName();
        protected abstract MessageType.ENTITY GetEntityType();
        protected abstract bool FilterEntity(string entity);
        protected abstract void SetId(string id, BaseRoot value);
        protected abstract void OnLoadResponseGetFields(FieldsResponseCrm fieldResponse, MessageType message);
        
        protected bool SendRequestGetRecord(BaseRoot value, string id, Func<string, object, bool> loadResponse)
        {
            return SendRequestGetRecord(value, GetEntityName(), id, loadResponse);
        }

        protected bool SendRequestGetRecord(BaseRoot value, string entityName, string id, Func<string, object, bool> loadResponse)
        {
            string url = value.Authentication.UrlService;
            string urlFormat = string.Format("{0}json/{1}/{2}?authtoken={3}&scope={4}&id={5}",
                url, entityName, "getRecordById", value.Authentication.Token, value.Authentication.User, id);
            return HttpMessageSender.SendRequestGet(urlFormat, value, loadResponse);
        }

        protected bool SendRequestGetFields(Authentication value, Func<string, object, bool> getResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}json/{1}/{2}?authtoken={3}&scope={4}", url, GetEntityName(), "getFields", value.Token, value.User);
            return HttpMessageSender.SendRequestGet(urlFormat, null,  getResponse);
        }

        protected bool SendRequestSave(BaseRoot value, List<MappingFields> mapping, Func<string, object, bool> getReponse)
        {
            string content = LoadXml(GetEntityName(), mapping);
            if (string.IsNullOrEmpty(value.GetId()))
                return SendRequestInsert(value.Authentication, content, value, getReponse);
            return SendRequestUpdate(value.Authentication, content, value, getReponse);
        }

        protected bool SendRequestDelete(Authentication value, string id, Func<string, object, bool> getResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&id={5}", url, GetEntityName(), "deleteRecords", value.Token, value.User, id);
            return HttpMessageSender.SendRequestPost(urlFormat, string.Empty, null, getResponse);
        }

        protected virtual bool GetResponse(string response, object value)
        {
            string idRecord = LoadId(response, (BaseRoot)value, SetId);

            MessageType message = new MessageType(MessageType.TYPE.ERROR, GetEntityType());

            if (IsSuccess(response, idRecord))
            {
                message.Type = MessageType.TYPE.SUCCESS;
                message.Data = new { id = idRecord };
            }

            string msg = string.Empty;
            Match match = Regex.Match(response, "(xml/)(\\S*)(\">)|(json/)(\\S*)(\">)");
            if (match.Success)
                msg = match.Groups[2].Value + ": ";

            match = Regex.Match(response, @"(<message>)(...*)(</message>)");
            if (match.Success)
                msg += match.Groups[2].Value;

            message.Message = msg;
            MessageController.AddMessage(message);
            return message.Type == MessageType.TYPE.SUCCESS;
        }

        protected void LoadResponse(EntityResponse value, MessageType message)
        {
            message.Type = MessageType.TYPE.SUCCESS;
            message.Message = string.Empty;
            message.Data = GetResponseFields(value);
        }

        protected string GetFieldValue(BaseRoot value, string field, Func<string, bool> filter)
        {
            var mapping = value.MappingFields.Where(v => filter(v.Entity)).ToList();
            var fieldValue = mapping.Where(x => x.Field == field).FirstOrDefault();
            return fieldValue.Value;
        }

        #endregion

        #region Private Methods

        private bool SendRequestInsert(Authentication value, string content, BaseRoot baseRoot, Func<string, object, bool> getResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&xmlData={5}", url, GetEntityName(), "insertRecords", value.Token, value.User, content);
            return _httpMessageSender.SendRequestPost(urlFormat, content, baseRoot, getResponse);
        }

        private bool SendRequestUpdate(Authentication value, string content, BaseRoot baseRoot, Func<string, object, bool> getResponse)
        {
            string url = value.UrlService;
            string urlFormat = string.Format("{0}xml/{1}/{2}?authtoken={3}&scope={4}&newFormat=1&id={5}&xmlData={6}", url, GetEntityName(), "updateRecords", value.Token, value.User, baseRoot.GetId(), content);
            return _httpMessageSender.SendRequestPost(urlFormat, content, baseRoot, getResponse);
        }

        private string LoadXml(string entityName, List<MappingFields> list)
        {
            string result = string.Format("<{0}><row no=\"1\">", entityName);

            foreach (MappingFields mapping in list)
                result += string.Format("<FL val=\"{0}\">{1}</FL>", mapping.Field, mapping.Value.Replace(",{0}", string.Empty));

            result += string.Format("</row></{0}>", entityName);
            return result;
        }

        private string LoadId(string value, BaseRoot baseRoot, Action<string, BaseRoot> setId)
        {
            if (value.IndexOf("<FL val=\"Id\">") > 0)
            {
                string id = value.Substring(value.IndexOf("<FL val=\"Id\">") + 13, 19);
                setId(id, baseRoot);
                return id;
            }
            return string.Empty;
        }

        private bool LoadResponseFields(string response, object value)
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

        private bool IsSuccess(string responseBody, string id) =>  (responseBody.IndexOf("<code>5000</code>") > 0) || !id.Equals(string.Empty);

        private static string RemoveDescription(string value)
        {
            Regex rgx = new Regex("(,{\"dv\":\")(Descrição | Description)(...*)(Information\"})");
            return rgx.Replace(value, string.Empty);
        }

        #endregion
    }
}