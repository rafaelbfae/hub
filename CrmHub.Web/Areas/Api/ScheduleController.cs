using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using System.Linq;
using System;

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
            _logger.LogDebug("Schedule Register Call");
            return Execute(schedule, (v, c) => v.Register(c));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] ReuniaoExact schedule)
        {
            _logger.LogDebug("Schedule Update Call");
            schedule.Reuniao.Id = id;
            return Execute(schedule, (v, c) => v.Update(c));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Schedule Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessage());
            }

            return ErrorValidation();
        }

        private IActionResult Execute(ReuniaoExact value, Func<IScheduleService, ReuniaoExact, bool> function, bool json = true)
        {
            if (ModelState.IsValid)
            {
                function(_service, value);
                return json ? Ok(_service.MessageController().GetAllMessageToJson()) : Ok(_service.MessageController().GetAllMessage());
            }

            return ErrorValidation();
        }

        private IActionResult ErrorValidation()
        {
            _logger.LogDebug("Model Error Validation");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(errors);
        }
    }
}
