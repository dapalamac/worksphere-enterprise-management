using Hangfire.Common;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

public class JobLoggingFilter : JobFilterAttribute, IServerFilter
{
    private const string StopwatchKey = "Stopwatch";

    private readonly ILogger<JobLoggingFilter> _logger;

    public JobLoggingFilter(ILogger<JobLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnPerforming(PerformingContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Items[StopwatchKey] = stopwatch;

        _logger.LogInformation(
            "Job {JobId} - {Service}.{Method} iniciado",
            context.BackgroundJob.Id,
            context.BackgroundJob.Job.Type.Name,
            context.BackgroundJob.Job.Method.Name);
    }

    public void OnPerformed(PerformedContext context)
    {
        var stopwatch = (Stopwatch)context.Items[StopwatchKey];

        stopwatch.Stop();

        if (context.Exception != null)
        {
            _logger.LogError(
                context.Exception,
                "Job {JobId} - {Service}.{Method} falló después de {ElapsedMilliseconds} ms",
                context.BackgroundJob.Id,
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name,
                stopwatch.ElapsedMilliseconds);

            return;
        }

        _logger.LogInformation(
            "Job {JobId} - {Service}.{Method} terminó correctamente en {ElapsedMilliseconds} ms",
            context.BackgroundJob.Id,
            context.BackgroundJob.Job.Type.Name,
            context.BackgroundJob.Job.Method.Name,
            stopwatch.ElapsedMilliseconds);
    }
}