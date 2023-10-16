using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Categories;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;

namespace Northwind.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("ProductID")
            .HasConversion(e => e.Value, e => new ProductId(e))
            .ValueGeneratedOnAdd();

        builder.Property(e => e.CategoryId)
            .HasColumnName("CategoryID")
            .HasConversion(e => e!.Value.Value, e => new CategoryId(e));

        builder.Property(e => e.ProductName)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(e => e.QuantityPerUnit)
            .HasMaxLength(30);

        builder.Property(e => e.ReorderLevel)
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.SupplierId)
            .HasColumnName("SupplierID")
            .HasConversion(x => x!.Value.Value, x => new SupplierId(x));

        builder.Property(e => e.UnitPrice)
            .HasColumnType("money")
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.UnitsInStock)
            .HasDefaultValueSql("((0))");

        builder.Property(e => e.UnitsOnOrder)
            .HasDefaultValueSql("((0))");

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(d => d.CategoryId);

        builder.HasOne(e => e.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(e => e.SupplierId);
    }
}