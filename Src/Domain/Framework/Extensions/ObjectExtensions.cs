using System.Text.Json;
using Newtonsoft.Json;

namespace ONLINE_SHOP.Domain.Framework.Extensions;

public static class ObjectExtensions
{
    public static string Serialize(this object obj)
    {
        try
        {
            return System.Text.Json.JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
        catch
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        }
    }
}