using ONLINE_SHOP.Domain.Framework.Exceptions;

namespace ONLINE_SHOP.Domain.Framework.ValueObjects;

public sealed record EntityUuid(Guid Value) : ValueObject
{
    public static implicit operator Guid?(EntityUuid self) => self?.Value;
    public static implicit operator Guid(EntityUuid self) => self.Value;

    public static EntityUuid Generate() => FromGuid(Guid.NewGuid());

    public static EntityUuid FromGuid(Guid uuid)
    {
        CheckValidity(uuid);

        return new EntityUuid(uuid);
    }

    public static EntityUuid FromString(string uuid)
    {
        var value = Guid.Parse(uuid);
        CheckValidity(value);
        return new EntityUuid(value);
    }

    public static void CheckValidity(Guid value)
    {
        if (value == default)
            throw new Dexception(Situation.Make(SitKeys.NotAllowed),
                new List<KeyValuePair<string, string>>
                {
                    new(":عملیات:", "ثبت اطلاعات"),
                    new(":موجودیت:", ""),
                    new(":شرایط:", "با شناسه خالی/نامعتبر")
                });
    }
}