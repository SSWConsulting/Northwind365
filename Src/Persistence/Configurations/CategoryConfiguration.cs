using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Northwind.Domain.Categories;

namespace Northwind.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

        builder.Property(e => e.Id).HasColumnName("CategoryID");

        builder.Property(e => e.CategoryName)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(e => e.Description).HasColumnType("ntext");

        builder.Property(e => e.Picture).HasColumnType("image");
    }
}