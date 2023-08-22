using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuartzAPI.Models;
using QuartzAPI.Services;

namespace QuartzAPI.Controllers
{

    //TEST: 0/4 * * ? * * *
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduler)
        {
            _scheduleService = scheduler;
        }
        [HttpGet]
        [Route("trigger-job")]
        public async Task<IActionResult> TriggerJob(string cronStr) 
        {
            await _scheduleService.ScheduleNotificationJob(cronStr);

            return Ok();
        }

        [HttpGet]
        [Route("stop-job")]
        public async Task<IActionResult> StopJob()
        {
            await _scheduleService.StopNotificationJob();
            return Ok();
        }

        [HttpPost]
        [Route("cron-model")]
        public async Task<IActionResult> ScheduleJobByCronModel(CronModel cronModel)
        {
            if(cronModel.IsScheduled)
            {
                await _scheduleService.ScheduleNotificationJob(cronModel.ToString());
            }
            else
            {
                await _scheduleService.StopNotificationJob();
            }

            Console.WriteLine($"RescheduleJobCronModel - {cronModel}");
            return Ok();
        }
    }
}
