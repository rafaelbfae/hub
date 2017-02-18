using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using Microsoft.Extensions.Logging;

namespace CrmHub.Web.Areas.Api.Base
{
    public class HubController<T> : Controller where T : IMessageService
    {
        protected readonly T _service;
        protected readonly ILogger _logger;

 
        protected HubController(T service, ILogger<HubController<T>> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        protected IActionResult Execute<E>(E value, Func<T, E, bool> function, bool json = true)
        {
            if (ModelState.IsValid)
            {
                function(_service, value);
                return json ? Ok(_service.MessageController().GetAllMessageToJson()) : Ok(_service.MessageController().GetAllMessage());
            }

            return ErrorValidation();
        }

        protected IActionResult ErrorValidation()
        {
            _logger.LogDebug("Model Error Validation");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(errors);
        }
    }
}
