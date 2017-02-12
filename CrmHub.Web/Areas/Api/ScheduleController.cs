using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Web.Areas.Api
{
    [Route("api/v1/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _service;
        private readonly ILogger _logger;

        public ScheduleController(IScheduleService service, ILogger<ScheduleController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReuniaoExact schedule)
        {
            _service.Register(schedule);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ReuniaoExact schedule)
        {
            schedule.Reuniao.Id = id;
            _service.Update(schedule);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
