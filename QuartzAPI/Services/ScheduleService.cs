using Quartz;
using QuartzAPI.Jobs;
using QuartzAPI.Models;

namespace QuartzAPI.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IConfiguration _config;
        private readonly JobModel _jobModel;
        private readonly TriggerModel _triggerModel;

        public ScheduleService(ISchedulerFactory schedulerFactory, IConfiguration config)
        {
            _schedulerFactory = schedulerFactory;
            _config = config;
            _jobModel = _config.GetSection(JobModel.JobSection).Get<JobModel>();
            _triggerModel = _config.GetSection(TriggerModel.TriggerSection).Get<TriggerModel>();
        }


        public async Task ScheduleNotificationJob(string cronStr)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var oldTrigger = await scheduler.GetTrigger(new TriggerKey(_triggerModel.Name, _triggerModel.Group));

            if (oldTrigger == null)
            {
                await StartSchduler(scheduler, cronStr);
                return;
            }

            var builder = oldTrigger.GetTriggerBuilder();



            builder = builder.WithCronSchedule(cronStr);

            var newTrigger = builder.Build();

            await scheduler.RescheduleJob(oldTrigger.Key, newTrigger);
        }

        public async Task StopNotificationJob()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobKey = new JobKey(_jobModel.Name, _jobModel.Group);

            await scheduler.DeleteJob(jobKey);
        }


        private async Task StartSchduler(IScheduler scheduler, string cronStr)
        {
            var job = JobBuilder.Create<NotificationJob>()
                       .WithIdentity(_jobModel.Name, _jobModel.Group)
                       .Build();

            var trigger = TriggerBuilder.Create()
                            .WithIdentity(_triggerModel.Name, _triggerModel.Group)
                            .WithCronSchedule(cronStr)
                            .Build();

            await scheduler.ScheduleJob(job, trigger);
            await scheduler.Start();
        }

        private void StopSchduler()
        {

        }
    }
}
