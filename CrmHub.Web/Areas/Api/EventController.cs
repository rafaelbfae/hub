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
    public class EventController : HubController<IEventService>
    {
        public EventController(IEventService service, ILogger<EventController> logger, ILoggerService loggerApi) : base(service, logger, loggerApi)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] EventoExact value)
        {
            _logger.LogDebug("Event Register Call");
            return Execute(value, Method.Post, (s, v) => s.Register(v));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EventoExact value)
        {
            _logger.LogDebug("Event Update Call");
            value.Reuniao.Id = id;
            return Execute(value, Method.Put, (s, v) => s.Update(v));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            _logger.LogDebug("Event Delete Call");
            return Execute("Event", id, Method.Delete, value, (s, v, a) => s.Delete(v, a));
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Event Fields Call");
            return Execute("Event", string.Empty, Method.Fields, value, (s, v, a) => s.Fields(a));
        }
    }
}
