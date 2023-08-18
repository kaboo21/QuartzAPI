namespace QuartzAPI.Services
{
    public interface IScheduleService
    {
        Task ScheduleNotificationJob(string cronStr);
        Task StopNotificationJob();
    }
}