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
    public class ContactController : HubController<IContactService>
    {
        public ContactController(IContactService service, ILogger<ContactController> logger) : base(service, logger)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContatoExact value)
        {
            _logger.LogDebug("Contact Register Call");
            return Execute(value, (v, c) => v.Register(c));
        }

        [HttpPut("{email}")]
        public IActionResult Put(string email, [FromBody] ContatoExact value)
        {
            _logger.LogDebug("Contact Update Call");
            value.Contato.Id = email;
            return Execute(value, (v, c) => v.Update(c));
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email, [FromBody] Autenticacao value)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Contact Delete Call");
                _service.Delete(email, value);
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
                _logger.LogDebug("Contact Fields Call");
                _service.Fields(value);
                return Ok(_service.MessageController().GetAllMessageToJson());
            }

            return ErrorValidation();
        }
    }
}
