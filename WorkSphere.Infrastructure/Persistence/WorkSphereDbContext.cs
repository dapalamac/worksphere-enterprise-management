using Microsoft.EntityFrameworkCore;
using WorkSphere.Application.Interfaces;
using WorkSphere.Domain.Common;
using WorkSphere.Domain.Entities;


namespace WorkSphere.Infrastructure.Persistence;

public class WorkSphereDbContext : DbContext
{
    private readonly ICurrentUserService _currentUser;

    public WorkSphereDbContext(
    DbContextOptions<WorkSphereDbContext> options,
    ICurrentUserService currentUser)
    : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Position> Positions => Set<Position>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkSphereDbContext).Assembly);

        modelBuilder.Entity<Employee>()
               .HasQueryFilter(e => !e.IsDeleted);

        modelBuilder.Entity<Department>()
            .HasQueryFilter(d => !d.IsDeleted);

        modelBuilder.Entity<Position>()
            .HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<User>()
            .HasQueryFilter(u => !u.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(
       CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = _currentUser.UserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedBy = _currentUser.UserId;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.Entity.IsDeleted = true;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = _currentUser.UserId;
                entry.State = EntityState.Modified;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}


