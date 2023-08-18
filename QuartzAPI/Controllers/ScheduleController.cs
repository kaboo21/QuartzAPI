using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuartzAPI.Services;

namespace QuartzAPI.Controllers
{

    //TEST: 0/4 * * ? * * *
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduler;

        public ScheduleController(IScheduleService scheduler)
        {
            _scheduler = scheduler;
        }
        [HttpGet]
        [Route("trigger-job")]
        public IActionResult TriggerJob(string cronStr) 
        {
            _scheduler.ScheduleNotificationJob(cronStr);

            return Ok();
        }

        [HttpGet]
        [Route("stop-job")]
        public IActionResult StopJob()
        {
            _scheduler.StopNotificationJob();
            return Ok();
        }
    }
}
