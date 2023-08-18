using Quartz;
using QuartzAPI.Models;

namespace QuartzAPI.Jobs
{
    public class NotificationJob : IJob
    {
        public readonly JobKey Key;

        public NotificationJob(IConfiguration config)
        {
            var job = config.GetSection(JobModel.JobSection).Get<JobModel>();
            Key = new JobKey(job.Name, job.Group);

        }

        public Task Execute(IJobExecutionContext context)
        {
            //context.CancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"{DateTime.Now} - Job executed");

            return Task.CompletedTask;
        }

    }
}
