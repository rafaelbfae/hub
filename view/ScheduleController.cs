using System;

using Microsoft.AspNetCore.Mvc;

using CrmHub.Model;
using CrmHub.Model.Schedule;

namespace CrmHub.Schedule 
{
    [Route("Schedule")]
    public class ScheduleController : Controller
    {        

        #region Public Methods

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Ok");
        }

        [HttpPost]
        public IActionResult Schedule([FromBody]ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.Schedule(v));
        }

        [HttpPost]
        public IActionResult ReSchedule([FromBody]ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.ReSchedule(v));
        }

        [HttpPost]
        public IActionResult CancelSchedule([FromBody]ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.CancelSchedule(v));
        }

        [HttpPost]
        public IActionResult FeedBackSchedule([FromBody]ScheduleValue value) 
        {
            return Execute(value, (c, v) => c.FeedBackSchedule(v));
        }

        #endregion

        #region Private Methods

        private IActionResult Execute(ScheduleValue value, Func<HubController, ScheduleValue, bool> function) 
        {
            HubController controller = new HubController();
            if (function(controller, value))
                return Ok();
            return NotFound();
        }

        #endregion
    }
}