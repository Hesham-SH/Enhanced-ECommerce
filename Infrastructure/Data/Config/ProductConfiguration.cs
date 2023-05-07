using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Price).HasColumnType("money");
            builder.HasOne(p => p.ProductBrand).WithMany()
                .HasForeignKey(k => k.ProductBrandId);
            builder.HasOne(p => p.ProductType).WithMany()
                .HasForeignKey(k => k.ProductTypeId);
        }
    }
}