using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Panel.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>

{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.id);
        builder.Property(x => x.id);
        builder.Property(x=>x.name).IsRequired();
        builder.Property(x=>x.price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x=>x.stock).IsRequired();
        builder.ToTable("Products");
        builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
    }
}