using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Package;

public class PackageItemConfig : IEntityTypeConfiguration<Domain.Entities.Package.PackageItem>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Package.PackageItem> builder)
    {
        builder.ToTable("packageitem", "deliver");

        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.PackageId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.ProductId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.ProductName)
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


        builder.HasOne(c => c.Package)
            .WithMany(c => c.PackageItems)
            .HasForeignKey(c => c.PackageId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Package_PackageItem");
    }
}