using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Package;

public class PackageConfig : IEntityTypeConfiguration<Domain.Entities.Package.Package>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Package.Package> builder)
    {
        builder.ToTable("package", "deliver");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

         builder.Property(c => c.OrderId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

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

        builder.Property(c => c.TrackingCode)
       .HasColumnType("VARCHAR(50)")
       .HasMaxLength(50);


        builder.MapAuditableColumns(); 
    }
}