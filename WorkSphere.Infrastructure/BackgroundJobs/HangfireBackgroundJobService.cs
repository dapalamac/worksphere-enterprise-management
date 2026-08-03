using Hangfire;
using Hangfire.States;
using System.Linq.Expressions;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Infrastructure.BackgroundJobs;

public class HangfireBackgroundJobService : IBackgroundJobService
{
    private readonly IBackgroundJobClient _backgroundJobClient;


    public HangfireBackgroundJobService(
        IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public string EnqueueEmail<T>(
        Expression<Func<T, Task>> methodCall)
    {
        return Enqueue(QueueNames.Emails, methodCall);
    }

    public string EnqueueCritical<T>(
        Expression<Func<T, Task>> methodCall)
    {
        return Enqueue(QueueNames.Critical, methodCall);
    }

    public string EnqueueReport<T>(
        Expression<Func<T, Task>> methodCall)
    {
        return Enqueue(QueueNames.Reports, methodCall);
    }

    // 👇 ESTE ES EL MÉTODO PRIVADO
    private string Enqueue<T>(
    string queue,
    Expression<Func<T, Task>> methodCall)
    {
        return _backgroundJobClient.Create(
            methodCall,
            new EnqueuedState(queue));
    }
}