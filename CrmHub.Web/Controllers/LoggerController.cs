using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces;
using System.Linq;
using CrmHub.Domain.Filters.Base;
using CrmHub.Domain.Interfaces.Filters;
using CrmHub.Domain.Models;
using CrmHub.Application.Models.Exact.Roots;

namespace CrmHub.Web.Controllers
{
    [Produces("application/json")]
    public class LoggerController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly ILoggerService _loggerService;

        public LoggerController(ILogger<LoggerController> logger, ILoggerService loggerService)
        {
            this._logger = logger;
            this._loggerService = loggerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SendResponse(int id)
        {
            var result = _loggerService.GetById(id);
            return base.Json(new
            {
                send = result.Send,
                response = result.Response
            });
        }

        [HttpPut]
        public IActionResult Resend(int id, [FromBody] LogApi value)
        {
            if (value.Type.Equals("Ready")) return Ok(value);

            _logger.LogDebug("Resend");
            var success = _loggerService.Resent(id, value);
            return base.Json(new
            {
                data = value
            });

        }

        [HttpGet]
        public IActionResult All(int pDraw, int pLength, int pStart, int pOrder, string pDir, string pSearch)
        {
            var result = _loggerService.GetList(new DataTableFilter()
            {
                Length = pLength,
                Start = pStart,
                Order = pOrder,
                Dir = pDir,
                Search = pSearch
            });
            return base.Json(new
            {
                draw = pDraw,
                recordsTotal = result.Count(),
                recordsFiltered = result.Count(),
                data = result
            });
        }
    }
}