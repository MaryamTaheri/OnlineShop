using System.ComponentModel;

namespace ONLINE_SHOP.Domain.Events.Enumerations;

public enum OrderStates
{
    [Description("در حال انتظار")]
    Pending = 1,

    [Description("تحویل داده شده")]
    Delivered = 2,

    //Optional
    [Description("کنسل شده")]
    Cancelled = 3
}