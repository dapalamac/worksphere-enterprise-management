namespace WorkSphere.Application.Interfaces;

public interface INotificationService
{
    Task SendWelcomeEmail(Guid employeeId);
}
