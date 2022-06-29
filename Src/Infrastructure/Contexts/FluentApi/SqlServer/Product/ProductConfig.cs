using ONLINE_SHOP.Domain.Entities.Order;
using ONLINE_SHOP.Domain.Entities.Product;
using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Product;

public class ProductConfig : IEntityTypeConfiguration<Domain.Entities.Product.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product.Product> builder)
    {
        builder.ToTable("product", "product");

        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.Name)
            .HasColumnType("VARCHAR(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Category)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(c => c.Price)
        .HasColumnType("DECIMAL(18,0)")
        .IsRequired();

        builder.Property(c => c.Type)
        .HasColumnType("SMALLINT")
        .IsRequired();

        builder.MapAuditableColumns();
    }
}