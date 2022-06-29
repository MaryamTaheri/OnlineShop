using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;
using ONLINE_SHOP.Domain.Framework.ValueObjects;

namespace ONLINE_SHOP.Domain.Entities.Package;

public partial class Package : GuidAuditableAggregateRoot
{
    public EntityUuid OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    
    public EntityUuid CustomerId { get; set; }
    [NotMapped]
    public string CustomerName => $"{CustomerFirstName} {CustomerLastName}";
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    

    public decimal TotalAmount { get; set; }
    public string TrackingCode { get; set; }

        
    public Collection<PackageItem> PackageItems { get; set; }
}