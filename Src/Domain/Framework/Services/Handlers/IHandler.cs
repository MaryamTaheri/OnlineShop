using ONLINE_SHOP.Domain.Framework.Logging;

namespace ONLINE_SHOP.Domain.Framework.Services.Handlers;

public interface IHandler
{
    public ILog Logger { get; }
}