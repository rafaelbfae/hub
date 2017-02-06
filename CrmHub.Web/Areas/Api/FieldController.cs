using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrmHub.Application.Models.Exact;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CrmHub.Web.Areas.Api
{
    [Route("api/v1/[controller]")]
    public class FieldController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new { id = 12 });
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] ScheduleSend envio)
        //{
        //    return Ok(new { id = 12 });
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] ScheduleSend envio)
        //{
        //    return new NoContentResult();
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new NoContentResult();
        }
    }
}
