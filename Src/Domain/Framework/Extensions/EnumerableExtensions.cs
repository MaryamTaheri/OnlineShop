namespace ONLINE_SHOP.Domain.Framework.Extensions;

public static  class EnumerableExtensions
{
    public static bool HasItem<T>(this IEnumerable<T> list)
    {
        return list != null && list.Any();
    }
}