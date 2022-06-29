
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ONLINE_SHOP.Domain.Framework.Contexts.Listeners;

public interface IBeforeUpdateListener
{
    void OnBeforeUpdate(EntityEntry entry);
}