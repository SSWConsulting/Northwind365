using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Employees;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("EmployeeID")
            .HasConversion(x => x.Value, x => new EmployeeId(x))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.BirthDate)
            .HasColumnType("datetime");

        builder.ComplexProperty(e => e.Address, AddressConfiguration.BuildAction);

        builder.Property(e => e.Extension)
            .HasMaxLength(4);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.HireDate)
            .HasColumnType("datetime");

        builder.Property(e => e.HomePhone)
            .HasMaxLength(24);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Notes)
            .HasColumnType("ntext");

        builder.Property(e => e.Photo)
            .HasColumnType("image");

        builder.Property(e => e.PhotoPath)
            .HasMaxLength(255);

        builder.Property(e => e.Title)
            .HasMaxLength(50);

        builder.Property(e => e.TitleOfCourtesy)
            .HasMaxLength(25);

        builder.HasOne(d => d.Manager)
            .WithMany(p => p.DirectReports)
            .HasForeignKey(d => d.ReportsTo)
            .HasConstraintName("FK_Employees_Employees");

        builder.HasMany(e => e.Territories)
            .WithMany(e => e.Employees)
            .UsingEntity<EmployeeTerritory>();
    }
}