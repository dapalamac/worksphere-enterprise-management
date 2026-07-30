using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkSphere.Application.Interfaces;
using WorkSphere.Application.Services.Auth;
using WorkSphere.Application.Services.Departments;
using WorkSphere.Application.Services.Employees;
using WorkSphere.Application.Services.NotificationService;
using WorkSphere.Application.Services.Positions;
using WorkSphere.Infrastructure.Persistence;
using WorkSphere.Infrastructure.Persistence.Repositories;
using WorkSphere.Infrastructure.Services;



namespace WorkSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext with SQL Server
        services.AddDbContext<WorkSphereDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Add Redis cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "WorkSphere:";
        });

        // Add Hangfire Sql Server storage
        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(
                configuration.GetConnectionString("DefaultConnection"));
        });
        // Add Hangfire server
        services.AddHangfireServer();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();


        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        // Add CacheService
        services.AddScoped<ICacheService, CacheService>();
        // Add NotificationService
        services.AddScoped<INotificationService, NotificationService>();


        return services;
    }
}