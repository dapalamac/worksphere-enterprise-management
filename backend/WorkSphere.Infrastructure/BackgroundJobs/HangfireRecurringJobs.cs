using Hangfire;

namespace WorkSphere.Infrastructure.BackgroundJobs;

public static class HangfireRecurringJobs
{
    public static void RegisterRecurringJobs()
    {
        RegisterDailyEmail();
        RegisterCleanupJob();
    }

    [Queue("reports")]
    private static void RegisterDailyEmail()
    {
        //RecurringJob.AddOrUpdate<INotificationService>(
        //    "daily-email",
        //    service => service.SendWelcomeEmail(""),
        //    Cron.Minutely);
    }


    [Queue("emails")]
    private static void RegisterCleanupJob()
    {
        // Aquí registraríamos otro Job
    }
}
