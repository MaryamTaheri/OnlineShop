using System.ComponentModel.DataAnnotations;
using ONLINE_SHOP.Domain.Events.DataTransferObjects.Order;
using ONLINE_SHOP.Domain.Framework.Exceptions;

namespace ONLINE_SHOP.Domain.Contracts.API.Order;

public class CreateOrderContract : IValidatableObject
{
    public string BasketId { get; set; }  //Basket Id 
    public DateTime OrderDate { get; set; }
    public string CustomerMobile { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    //public string TrackingCode { get; set; }  //from basket service ???
    public OrderItemDTO[] Products { get; set; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BasketId))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                        new List<KeyValuePair<string, string>> { new(":پیام:", "شماره سبد را ارسال نمایید.") });

        if (OrderDate == default)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                        new List<KeyValuePair<string, string>> { new(":پیام:", "تاریخ ثیت را ارسال نمایید.") });

        if (string.IsNullOrWhiteSpace(CustomerMobile))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                        new List<KeyValuePair<string, string>> { new(":پیام:", "شماره مبایل مشتری را ارسال نمایید.") });

        if (Products.Length == 0 ||
            Products.Any(p => p.ProductId == default) ||
            Products.Any(p => p.ProductCount == 0))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                        new List<KeyValuePair<string, string>> { new(":پیام:", "اطلاعات کالاها را صحیح ارسال نمایید.") });

        //can save in db,appsetting, const class
        var startTime = new TimeSpan(8, 0, 0); //8 o'clock
        var endTime = new TimeSpan(19, 0, 0); //19 o'clock
        var currentTime = DateTime.Now.TimeOfDay;

        if (!(currentTime >= startTime && currentTime < endTime))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                            new List<KeyValuePair<string, string>> { new(":پیام:", "تاریخ ثبت سفارش را صححیح ارسال نمایید.") });

        yield break;
    }
}
