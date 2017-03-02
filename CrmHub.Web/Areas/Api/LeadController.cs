using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;
using CrmHub.Application.Interfaces;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class LeadController : HubController<ILeadService>
    {
        public LeadController(ILeadService service, ILogger<LeadController> logger, ILoggerApiService loggerApi) : base(service, logger, loggerApi)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeadExact value)
        {
            _logger.LogDebug("Lead Register Call");
            return Execute(value, Method.Post, (s, v) => s.Register(v));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] LeadExact value)
        {
            _logger.LogDebug("Lead Update Call");
            value.Lead.Id = id;
            return Execute(value, Method.Put, (s, v) => s.Update(v));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            _logger.LogDebug("Lead Delete Call");
            return Execute("Lead", id, Method.Delete, value, (s, v, a) => s.Delete(v, a));
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Lead Fields Call");
            return Execute("Lead", string.Empty, Method.Fields, value, (s, v, a) => s.Fields(a));
        }
    }
}
