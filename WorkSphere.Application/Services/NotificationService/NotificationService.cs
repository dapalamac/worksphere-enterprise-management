using Microsoft.Extensions.Logging;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Application.Services.NotificationService;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendWelcomeEmail(int employeeId)
    {

        try
        {
            _logger.LogInformation(
            "Iniciando envío de correo para el empleado {EmployeeId}",
            employeeId);

            await Task.Delay(5000);

            throw new Exception("Error simulado");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enviando correo");

            throw;
        }
    }
}
