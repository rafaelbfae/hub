using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Web.Areas.Api
{
    [Route("api/v1/[controller]")]
    public class LeadController : Controller
    {
        private readonly ILeadService _service;
        private readonly ILogger _logger;

        public LeadController(ILeadService service, ILogger<LeadController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LeadExact schedule)
        {
            _logger.LogDebug("Lead Register Call");
            _service.Register(schedule);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LeadExact schedule)
        {
            _logger.LogDebug("Lead Update Call");
            _service.Update(schedule);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogDebug("Lead Delete Call");
            return new NoContentResult();
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Lead Fields Call");
            _service.Fields(value);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }
    }
}
