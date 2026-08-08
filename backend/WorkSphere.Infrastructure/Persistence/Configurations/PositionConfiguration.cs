using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkSphere.Domain.Entities;

namespace WorkSphere.Infrastructure.Persistence.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.Description)
               .HasMaxLength(300);
    }
}