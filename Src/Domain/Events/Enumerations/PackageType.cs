using System.ComponentModel;

namespace ONLINE_SHOP.Domain.Events.Enumerations;

public enum PackageType
{
    [Description("عادی")] 
    NORMAL = 1,

    [Description("شکستنی")] 
    BREAKABLE = 2
}