using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class GuidCreatableAggregateRoot : CreatableAggregateRoot<EntityUuid>
{
}