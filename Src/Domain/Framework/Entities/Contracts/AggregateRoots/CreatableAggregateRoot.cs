using ONLINE_SHOP.Domain.Framework.Entities.Auditable;

namespace ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class CreatableAggregateRoot : AggregateRoot, ICreatableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}

public abstract class CreatableAggregateRoot<TKey> : AggregateRoot<TKey>, ICreatableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}