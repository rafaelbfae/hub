using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact;
using CrmHub.Application.Interfaces;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Web.Areas.Api
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _service;
        private readonly ILogger _logger;

        public ScheduleController(IScheduleService service, ILogger<ScheduleController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { id = 12 });
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReuniaoExact schedule)
        {
            _service.Schedule(schedule);
            return Ok(new { id = 12 });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ReuniaoExact schedule)
        {
            _service.ReSchedule(schedule);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
