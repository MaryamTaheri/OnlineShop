using ONLINE_SHOP.Domain.Events.DataTransferObjects.Package;
using ONLINE_SHOP.Domain.Framework.Contracts.Response;
using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Domain.Framework.Services.Requests;

namespace ONLINE_SHOP.Domain.Contracts.Commands.Deliver;

public class CreatePackageCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public DateTime OrderDate { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobile { get; set; }
    public decimal TotalAmount { get; set; }
    public string TrackingCode { get; set; }
    public List<PackageItemDetailDTO> PackageItems { get; set; }
}