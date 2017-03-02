using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces.Integration;
using CrmHub.Application.Models.Exact.Roots;
using CrmHub.Application.Models.Exact;
using CrmHub.Web.Areas.Api.Base;
using Microsoft.AspNetCore.Authorization;
using CrmHub.Application.Interfaces;

namespace CrmHub.Web.Areas.Api
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class CompanyController : HubController<ICompanyService>
    {
        public CompanyController(ICompanyService service, ILogger<CompanyController> logger, ILoggerApiService loggerApi) : base(service, logger, loggerApi)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmpresaExact value)
        {
            var user = HttpContext.User;
            _logger.LogDebug("Accounts Register Call - User: {0}", User.Identity.Name);
            return Execute(value, Method.Post, (s, v) => s.Register(v));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EmpresaExact value)
        {
            _logger.LogDebug("Accounts Update Call");
            value.Empresa.Id = id;
            return Execute(value, Method.Put, (s, v) => s.Update(v));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] Autenticacao value)
        {
            _logger.LogDebug("Accounts Delete Call");
            return Execute("Accounts", id, Method.Delete, value, (s, v, a) => s.Delete(v, a));
        }

        [HttpPost]
        [Route("fields")]
        public IActionResult Fields([FromBody] Autenticacao value)
        {
            _logger.LogDebug("Accounts Fields Call");
            return Execute("Accounts", string.Empty, Method.Fields, value, (s, v, a) => s.Fields(a));
        }
    }
}
