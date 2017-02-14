using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;

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
        public IActionResult Post([FromBody] ContatoExact value)
        {
            _service.Register(value);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContatoExact value)
        {
            _service.Update(value);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //_service.Delete(value);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _service.Fields(value);
            return Ok(_service.MessageController().GetAllMessageToJson());
        }
    }
}
