using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;
using CrmHub.Application.Interfaces;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ScheduleController : HubController<IScheduleService>
    {
        public ScheduleController(IScheduleService service, ILogger<ScheduleController> logger, ILoggerApiService loggerApi) : base(service, logger, loggerApi)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReuniaoExact value)
        {
            _logger.LogDebug("Schedule Register Call");
            return Execute(value, Method.Post, (s, v) => s.Register(v));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ReuniaoExact value) => Post(value);

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ReuniaoExact value)
        {
            _logger.LogDebug("Schedule Update Call");
            value.Reuniao.Id = id;
            return Execute(value, Method.Put, (s, v) => s.Update(v));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            _logger.LogDebug("Schedule Delete Call");
            return Execute("Schedule", id, Method.Delete, value, (s, v, a) => s.Delete(v, a));
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Events Fields Call");
            return Execute("Events", string.Empty, Method.Fields, value, (s, v, a) => s.Fields(a));
        }
    }
}
