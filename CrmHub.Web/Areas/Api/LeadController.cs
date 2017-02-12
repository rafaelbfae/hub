using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Interfaces.Integration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
            _service.Register(schedule);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LeadExact schedule)
        {
            _service.Update(schedule);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
