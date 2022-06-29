using ONLINE_SHOP.Domain.Framework.ValueObjects;
using ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.FluentApi.SqlServer.Customer;

public class CustomerConfig : IEntityTypeConfiguration<Domain.Entities.Customer.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Customer.Customer> builder)
    {
        builder.ToTable("customer", "customer");

        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.FirstName)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100);

        builder.Property(c => c.LastName)
       .HasColumnType("VARCHAR(100)")
       .HasMaxLength(100);

        builder.Property(c => c.Mobile)
        .HasColumnType("CHAR(11)")
        .HasMaxLength(11);

        builder.MapAuditableColumns();
    }
}