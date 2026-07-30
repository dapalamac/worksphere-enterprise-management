namespace WorkSphere.Application.Interfaces;

public interface INotificationService
{
    Task SendWelcomeEmail(int employeeId);
}
