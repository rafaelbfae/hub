using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;

namespace CrmHub.Web.Areas.Api
{
    [Route("api/v1/[controller]")]
    public class LeadController : HubController<ILeadService>
    {
        public LeadController(ILeadService service, ILogger<LeadController> logger) : base(service, logger)
        {
        }
    
        [HttpPost]
        public IActionResult Post([FromBody] LeadExact value)
        {
            _logger.LogDebug("Lead Register Call");
            return Execute(value, (v, c) => v.Register(c));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] LeadExact value)
        {
            _logger.LogDebug("Lead Update Call");
            value.Lead.Id = id;
            return Execute(value, (v, c) => v.Update(c));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Lead Delete Call");
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
                _logger.LogDebug("Lead Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }
    }
}
