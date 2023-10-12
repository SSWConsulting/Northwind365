using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Employees;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
{
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
        builder.HasKey(e => e.Id)
            .IsClustered(false);

        builder.Property(e => e.Id)
            .HasColumnName("TerritoryID")
            .HasMaxLength(20)
            .HasConversion(e => e.Value, e => new TerritoryId(e))
            .ValueGeneratedNever();

        builder.Property(e => e.RegionId)
            .HasColumnName("RegionID")
            .HasConversion(e => e.Value, e => new RegionId(e));

        builder.Property(e => e.TerritoryDescription)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(d => d.Region)
            .WithMany(p => p.Territories)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Territories_Region");
    }
}