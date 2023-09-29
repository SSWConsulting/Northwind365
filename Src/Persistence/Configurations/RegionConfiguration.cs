﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Northwind.Domain.Employees;

namespace Northwind.Persistence.Configurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(e => e.RegionId)
            .IsClustered(false);

        builder.Property(e => e.RegionId)
            .HasColumnName("RegionID")
            .ValueGeneratedNever();

        builder.Property(e => e.RegionDescription)
            .IsRequired()
            .HasMaxLength(50);
    }
}
