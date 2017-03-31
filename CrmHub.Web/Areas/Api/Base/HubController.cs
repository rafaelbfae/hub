using CrmHub.Application.Interfaces.Integration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces;
using CrmHub.Domain.Models;
using CrmHub.Application.Models.Exact.Roots.Base;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Web.Areas.Api.Base
{
    public class HubController<T> : Controller where T : IMessageService
    {
        protected readonly T _service;
        protected readonly ILogger _logger;
        protected readonly ILoggerService _loggerApiService;
        protected enum Method { Post, Put, Delete, Get, Fields }

        protected HubController(T service, ILogger<HubController<T>> logger, ILoggerService loggerApiService)
        {
            this._service = service;
            this._logger = logger;
            this._loggerApiService = loggerApiService;
        }

        protected IActionResult Execute<E>(E value, Method method, Func<T, E, bool> function, bool addLog = true) where E : BaseExact<E>
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var log = AddLoggerApi(value, method, value.Autenticacao, value.GetId(), addLog);
                    bool success = function(_service, value);
                    UpdateLoggerApi(log, addLog, success);
                    return success ? Ok(_service.MessageController().GetAllMessage()) : StatusCode(400, _service.MessageController().GetAllMessage());
                }

                return ErrorValidation();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { type = "ERROR", Message = ex.Message });
            }
        }

        protected IActionResult Execute(string entityName, string parameter, Method method, Autenticacao autenticacao, Func<T, string, Autenticacao, bool> function, bool addLog = true)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var log = AddLoggerApi(parameter, method, autenticacao, parameter, addLog, entityName);
                    bool success = function(_service, parameter, autenticacao);
                    UpdateLoggerApi(log, addLog, success);
                    return success ? Ok(_service.MessageController().GetAllMessage()) : StatusCode(400, _service.MessageController().GetAllMessage());
                }

                return ErrorValidation();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { type = "ERROR", message = ex.Message });
            }
        }

        private LogApi AddLoggerApi<E>(E value, Method method, Autenticacao autenticacao, string parameter, bool addLog, string entityName = null)
        {
            if (!addLog) return null;

            _logger.LogDebug("Add Log");
            var log = new LogApi()
            {
                Type = "Sending",
                Parameters = parameter,
                Method = method.ToString(),
                Crm = autenticacao.TipoCRM,
                Send = value.Equals(parameter) ? autenticacao.ToJson() : value.ToJson(),
                Entity = string.IsNullOrEmpty(entityName) ? value.GetType().Name : entityName,
                User = string.IsNullOrEmpty(HttpContext.User.Identity.Name) ? "Anonymous" : HttpContext.User.Identity.Name,
                Empresa = string.IsNullOrEmpty(autenticacao.EmpresaCliente) ? "" : autenticacao.EmpresaCliente
            };

            _loggerApiService.Add(log);
            return log;
        }

        private void UpdateLoggerApi(LogApi log, bool addLog, bool success)
        {
            if (!addLog) return;

            log.Response = _service.MessageController().GetAllMessage().ToJson();
            log.Type = success ? "Success" : "Error";
            _loggerApiService.Update(log);
        }

        protected IActionResult ErrorValidation()
        {
            _logger.LogDebug("Model Error Validation");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(errors);
        }
    }
}
