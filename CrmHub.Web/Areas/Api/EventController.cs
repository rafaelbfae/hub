using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class EventController : HubController<IEventService>
    {
        public EventController(IEventService service, ILogger<EventController> logger) : base(service, logger)
        {
        }
    
        [HttpPost]
        public IActionResult Post([FromBody] EventoExact value)
        {
            _logger.LogDebug("Event Register Call");
            return Execute(value, (v, c) => v.Register(c));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EventoExact value)
        {
            _logger.LogDebug("Event Update Call");
            value.Reuniao.Id = id;
            return Execute(value, (v, c) => v.Update(c));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Event Delete Call");
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
                _logger.LogDebug("Event Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }
    }
}
