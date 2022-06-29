using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Order;

public class WeatherConfig : IEntityTypeConfiguration<Domain.Entities.Order.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
    {
        builder.ToTable("order", "order");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.BasketId)
            .HasColumnType("VARCHAR(100)")
            .HasMaxLength(100);

        builder.Property(c => c.OrderDate)
        .HasColumnType("DateTime");

        builder
            .Property(c => c.CustomerId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.CustomerFirstName)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100);

        builder.Property(c => c.CustomerLastName)
       .HasColumnType("VARCHAR(100)")
       .HasMaxLength(100);

        builder.Property(c => c.CustomerMobile)
        .HasColumnType("CHAR(11)")
        .HasMaxLength(11);

        builder.Property(c => c.TotalAmount)
        .HasColumnType("DECIMAL(18,0)")
        .IsRequired();

        builder.Property(c => c.DiscountAmount)
        .HasColumnType("DECIMAL(18,0)")
        .HasDefaultValue(0)
        .IsRequired();

        builder.Property(c => c.DiscountPercent)
        .HasColumnType("DECIMAL(18,0)")
        .HasDefaultValue(0)
        .IsRequired();

        builder.Property(c => c.CancelDeadline)
       .HasColumnType("DateTime");

        builder.Property(c => c.TrackingCode)
       .HasColumnType("VARCHAR(50)")
       .HasMaxLength(50);

       builder.Property(c => c.State)
            .HasColumnType("SMALLINT")
            .IsRequired();


        builder.MapAuditableColumns(); // ?????????????????
    }
}