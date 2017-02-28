﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class CompanyController : HubController<IAccountService>
    {
        public CompanyController(IAccountService service, ILogger<CompanyController> logger) : base(service, logger)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmpresaExact value)
        {
            _logger.LogDebug("Company Register Call");
            return Execute(value, (v, c) => v.Register(c));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EmpresaExact value)
        {
            _logger.LogDebug("Company Update Call");
            value.Empresa.Id = id;
            return Execute(value, (v, c) => v.Update(c));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Company Delete Call");
                _service.Delete(id, value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Company Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }
    }
}
