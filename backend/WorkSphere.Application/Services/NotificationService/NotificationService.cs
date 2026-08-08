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

    public async Task SendWelcomeEmail(Guid employeeId)
    {
        _logger.LogInformation(
            "Iniciando envío de correo para el empleado {EmployeeId}",
            employeeId);

        await Task.Delay(5000);

        _logger.LogInformation(
          "Correo enviado correctamente al empleado {EmployeeId}",
          employeeId);

        //throw new Exception("Error simulado");
    }
}
