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
            _logger.LogDebug("Contact Register Call");
            _service.Register(value);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContatoExact value)
        {
            _logger.LogDebug("Contact Update Call");
            _service.Update(value);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogDebug("Contact Delete Call");
            //_service.Delete(value);
            return Ok(_service.MessageController().GetAllMessage());
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Contact Fields Call");
            _service.Fields(value);
            return Ok(_service.MessageController().GetAllMessage());
        }
    }
}
