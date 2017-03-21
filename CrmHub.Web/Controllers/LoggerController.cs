using CrmHub.Application.Interfaces;
using CrmHub.Domain.Filters.Base;
using CrmHub.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

namespace CrmHub.Web.Controllers
{
    [Authorize]
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
        public IActionResult Resend(string id, [FromBody] LogApi value)
        {
            try
            {
                if (value.Type.Equals("Success")) return Ok(value);

                _logger.LogDebug("Resend");
                value.User = HttpContext.User.Identity.Name;
                var success = _loggerService.Resent(id, value);

                return base.Json(new
                {
                    success = success,
                    data = value
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, base.Json( new { success = false, data = ex.Message}));
            }
        }

        [HttpGet]
        public IActionResult All(int pDraw, int pLength, int pStart, int pOrder, string pDir, string pSearch)
        {
            var tableFilter = new DataTableFilter()
            {
                Length = pLength,
                Start = pStart,
                Order = pOrder,
                Dir = pDir,
                Search = pSearch
            };
            var result = _loggerService.GetList(tableFilter);
            return base.Json(new
            {
                draw = pDraw,
                recordsTotal = tableFilter.Total,
                recordsFiltered = tableFilter.Total,
                data = result
            });
        }
    }
}