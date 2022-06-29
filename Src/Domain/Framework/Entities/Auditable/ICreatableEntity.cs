namespace ONLINE_SHOP.Domain.Framework.Entities.Auditable;

public interface ICreatableEntity
{
    DateTime CreatedAt { get; set; }
    Guid CreatedBy { get; set; }
}