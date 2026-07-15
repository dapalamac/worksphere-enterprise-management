using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkSphere.Domain.Entities;


namespace WorkSphere.Infrastructure.Persistence;

public class WorkSphereDbContext : DbContext
{
    public WorkSphereDbContext(DbContextOptions<WorkSphereDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Position> Positions => Set<Position>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkSphereDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}


