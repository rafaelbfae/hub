using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Web.Areas.Api.Base;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Interfaces;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;

namespace CrmHub.Web.Areas.Api
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class ContactController : HubController<IContactService>
    {
        public ContactController(IContactService service, ILogger<ContactController> logger, ILoggerService loggerApi) : base(service, logger, loggerApi)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContatoExact value)
        {
            _logger.LogDebug("Contact Register Call");
            return Execute(value, Method.Post, (s, v) => s.Register(v));
        }

        [HttpPut("{email}")]
        public IActionResult Put(string email, [FromBody] ContatoExact value)
        {
            _logger.LogDebug("Contact Update Call");
            value.Contato.Id = email;
            return Execute(value, Method.Put, (s, v) => s.Update(v));
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email, [FromBody] Autenticacao value)
        {
            _logger.LogDebug("Contact Delete Call");
            return Execute("ContatoExact", email, Method.Delete, value, (s, v, a) => s.Delete(v, a));
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Contact Fields Call");
            return Execute("ContatoExact", string.Empty, Method.Fields, value, (s, v, a) => s.Fields(a));
        }
    }
}
