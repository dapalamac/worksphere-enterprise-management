using Hangfire;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Infrastructure.BackgroundJobs;

public static class HangfireJobs
{
    public static void RegisterRecurringJobs()
    {
        RegisterDailyEmail();
        RegisterCleanupJob();
    }

    private static void RegisterDailyEmail()
    {
        RecurringJob.AddOrUpdate<INotificationService>(
            "daily-email",
            service => service.SendWelcomeEmail(1),
            Cron.Minutely);
    }

    private static void RegisterCleanupJob()
    {
        // Aquí registraríamos otro Job
    }
}
