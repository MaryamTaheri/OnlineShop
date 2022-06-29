using ONLINE_SHOP.Domain.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONLINE_SHOP.Infrastructure.Contexts.Common.Extension;

public static partial class Extensions
{
    public static void MapAuditableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.MapCreatableColumns();
        modelBuilder.MapModifiableColumns();
        modelBuilder.MapRemovableColumns();
    }

    public static void MapCreatableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.Property("CreatedAt")
            .HasColumnType("DateTime")
            .HasDefaultValueSql("getutcdate()")
            .IsRequired();

        modelBuilder.Property("CreatedBy")
            .HasColumnType("UniqueIdentifier")
            .IsRequired();
    }

    public static void MapModifiableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.Property("ModifiedAt")
            .HasColumnType("DateTime")
            .IsRequired(false);

        modelBuilder.Property("ModifiedBy")
            .HasColumnType("UniqueIdentifier")
            .IsRequired(false);
    }

    public static void MapRemovableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.Property("RemovedAt")
            .HasColumnType("DateTime")
            .IsRequired(false);

        modelBuilder.Property("RemovedBy")
            .HasColumnType("UniqueIdentifier")
            .IsRequired(false);
    }

    public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("ONLINE_SHOP.Infrastructure", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x =>
                    x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        foreach (var item in typesToRegister)
        {
            dynamic service = Activator.CreateInstance(item);

            modelBuilder.ApplyConfiguration(service);
        }
    }

    public static void NeedToRegisterEntitiesConfig<T>(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("ONLINE_SHOP.Domain", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x => x == typeof(T)))
            .ToList();

        foreach (var item in typesToRegister)
            modelBuilder.Entity(item);
    }
}