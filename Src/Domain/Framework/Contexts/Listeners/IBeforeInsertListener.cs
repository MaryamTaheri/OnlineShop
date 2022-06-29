using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ONLINE_SHOP.Domain.Framework.Contexts.Listeners;

public interface IBeforeInsertListener
{
    void OnBeforeInsert(EntityEntry entry);
}