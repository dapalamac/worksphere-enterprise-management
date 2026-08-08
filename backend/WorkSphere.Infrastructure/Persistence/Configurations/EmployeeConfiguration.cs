using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.Email)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(e => e.Phone)
               .HasMaxLength(20);

        builder.Property(e => e.Salary)
               .HasPrecision(18, 2);

        builder.HasOne(e => e.Department)
               .WithMany(d => d.Employees)
               .HasForeignKey(e => e.DepartmentId);

        builder.HasOne(e => e.Position)
               .WithMany(p => p.Employees)
               .HasForeignKey(e => e.PositionId);
    }
}