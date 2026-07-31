using Hangfire;
using System.Linq.Expressions;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Infrastructure.BackgroundJobs;

public class HangfireBackgroundJobService : IBackgroundJobService
{
    public string Enqueue<T>(
        Expression<Func<T, Task>> methodCall)
    {
        return BackgroundJob.Enqueue(methodCall);
    }

}