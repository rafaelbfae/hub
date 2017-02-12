using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CrmHub.Web.Areas.Api
{
    [Route("api/v1/[controller]")]
    public class ContactController : Controller
    {

        private readonly IContactService _service;
        private readonly ILogger _logger;

        public ContactController(IContactService service, ILogger<ContactController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContatoExact schedule)
        {
            _service.Register(schedule);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContatoExact schedule)
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
