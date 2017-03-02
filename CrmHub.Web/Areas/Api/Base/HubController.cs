using CrmHub.Application.Interfaces.Integration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces;
using CrmHub.Domain.Models;
using CrmHub.Application.Models.Exact.Roots.Base;
using System.Reflection;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Web.Areas.Api.Base
{
    public class HubController<T> : Controller where T : IMessageService
    {
        protected readonly T _service;
        protected readonly ILogger _logger;
        protected readonly ILoggerApiService _loggerApiService;
        protected enum Method { Post, Put, Delete, Get, Fields }

        protected HubController(T service, ILogger<HubController<T>> logger, ILoggerApiService loggerApiService)
        {
            this._service = service;
            this._logger = logger;
            this._loggerApiService = loggerApiService;
        }

        protected IActionResult Execute<E>(E value, Method method, Func<T, E, bool> function) where E : BaseExact<E>
        {
            if (ModelState.IsValid)
            {
                var log = AddLoggerApi(value, method, value.Autenticacao, value.GetId());
                function(_service, value);
                log.Response = _service.MessageController().GetAllMessage().ToJson();
                log.Type = "Ready";
                _loggerApiService.Update(log);
                return Ok(_service.MessageController().GetAllMessage());
            }

            return ErrorValidation();
        }

        protected IActionResult Execute(string entityName, string parameter, Method method, Autenticacao autenticacao, Func<T, string, Autenticacao, bool> function)
        {
            if (ModelState.IsValid)
            {
                var log = AddLoggerApi(parameter, method, autenticacao, parameter, entityName);
                function(_service, parameter, autenticacao);
                log.Response = _service.MessageController().GetAllMessage().ToJson();
                log.Type = "Ready";
                _loggerApiService.Update(log);
                return Ok(_service.MessageController().GetAllMessage());
            }

            return ErrorValidation();
        }

        protected LogApi AddLoggerApi<E>(E value, Method method, Autenticacao autenticacao, string parameter, string entityName = null)
        {
            _logger.LogDebug("Add Log");
            var log = new LogApi()
            {
                Type = "Sending...",
                Parameters = parameter,
                Method = method.ToString(),
                Crm = autenticacao.TipoCRM,
                Send = value.Equals(parameter) ? autenticacao.ToJson() : value.ToJson(),
                Entity = string.IsNullOrEmpty(entityName) ? value.GetType().Name : entityName,
                User = string.IsNullOrEmpty(HttpContext.User.Identity.Name) ? "Anonymous" : HttpContext.User.Identity.Name
            };

            _loggerApiService.Add(log);
            return log;
        }

        protected IActionResult ErrorValidation()
        {
            _logger.LogDebug("Model Error Validation");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(errors);
        }
    }
}
