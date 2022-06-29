using ONLINE_SHOP.Domain.Entities.Order;
using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Order;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderitem", "order");

        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.OrderId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.ProductId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.ProductName)
            .HasColumnType("VARCHAR(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.ProductCategory)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(c => c.ProductPrice)
        .HasColumnType("DECIMAL(18,0)")
        .IsRequired();

        builder.Property(c => c.ProductProfitPrice)
        .HasColumnType("DECIMAL(18,0)")
        .IsRequired();

        builder.Property(c => c.ProductCount)
        .HasColumnType("DECIMAL(18,0)")
        .IsRequired();

        builder.Property(c => c.Type)
        .HasColumnType("SMALLINT")
        .IsRequired();

        builder.MapAuditableColumns();


        builder.HasOne(c => c.Order)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Order_OrderItem");
    }
}