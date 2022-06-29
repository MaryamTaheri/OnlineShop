using MediatR;

namespace ONLINE_SHOP.Domain.Framework.Services.Handlers;

public interface IApplicationCommandHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IApplicationCommand<TResult>
{

}