using Microsoft.EntityFrameworkCore;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Infrastructure.Persistence.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(WorkSphereDbContext context)
    {
        Console.WriteLine("===== SEED EJECUTADO =====");

        if (await context.Users.AnyAsync())
            return;

        var admin = new User
        {
            Name = "Administrador",
            Email = "admin@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "Admin"
        };

        context.Users.Add(admin);

        var employee = new User
        {
            Name = "Juan Pérez",
            Email = "employee@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Role = "Employee"
        };

        context.Users.Add(employee);

        await context.SaveChangesAsync();
    }
}