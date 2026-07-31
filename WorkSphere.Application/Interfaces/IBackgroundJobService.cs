using System.Linq.Expressions;

namespace WorkSphere.Application.Interfaces;

public interface IBackgroundJobService
{
    string EnqueueEmail<T>(
        Expression<Func<T, Task>> methodCall);

    string EnqueueCritical<T>(
        Expression<Func<T, Task>> methodCall);

    string EnqueueReport<T>(
        Expression<Func<T, Task>> methodCall);
}
