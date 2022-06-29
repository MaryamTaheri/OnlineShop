using MediatR;

namespace ONLINE_SHOP.Domain.Framework.Services;

public interface IApplicationQuery<out TResult> : IRequest<TResult>
{
        
}