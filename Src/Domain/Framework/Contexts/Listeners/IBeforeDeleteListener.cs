using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ONLINE_SHOP.Domain.Framework.Contexts.Listeners;

public interface IBeforeDeleteListener
{
    void OnBeforeDelete(EntityEntry entry);
}