using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Framework.Entities.Contracts;

public abstract class GuidAuditableEntity : AuditableEntity<EntityUuid>
{
}