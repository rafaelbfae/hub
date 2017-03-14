using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrmHub.Application.Interfaces;
using System.Linq;
using CrmHub.Domain.Filters.Base;
using CrmHub.Domain.Interfaces.Filters;

namespace CrmHub.Web.Controllers
{
    [Produces("application/json")]
    public class LoggerController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly ILoggerApiService _loggerApiService;

        public LoggerController(ILogger<LoggerController> logger, ILoggerApiService loggerApiService)
        {
            this._logger = logger;
            this._loggerApiService = loggerApiService;
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
            var result = _loggerApiService.GetById(id);
            return base.Json(new
            {
                send = result.Send,
                response = result.Response
            });
        }


        [HttpGet]
        public IActionResult All(int pDraw, int pLength, int pStart, int pOrder, string pDir, string pSearch)
        {
            var result = _loggerApiService.GetList(new DataTableFilter()
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