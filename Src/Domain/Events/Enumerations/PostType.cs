using System.ComponentModel;

namespace ONLINE_SHOP.Domain.Events.Enumerations;

public enum PostType
{
    [Description("پست معمولی")] 
    NORMAL = 1,

    [Description("پست پیشتاز")] 
    EXPRESS = 2
}