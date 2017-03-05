using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ScheduleController : HubController<IScheduleService>
    {
        public ScheduleController(IScheduleService service, ILogger<ScheduleController> logger) : base(service, logger)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReuniaoExact value)
        {
            _logger.LogDebug("Schedule Register Call");
            return Execute(value, (v, c) => v.Register(c));
        }

        [HttpPut]
        public IActionResult Put([FromBody] ReuniaoExact value)
        {
            return Post(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ReuniaoExact value)
        {
            _logger.LogDebug("Schedule Update Call");
            value.Reuniao.Id = id;
            return Execute(value, (v, c) => v.Update(c));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Schedule Delete Call");
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
                _logger.LogDebug("Schedule Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }
    }
}
