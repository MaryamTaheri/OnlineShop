using ONLINE_SHOP.Domain.Framework.Entities.Contracts.AggregateRoots;
using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.Extensions;
using ONLINE_SHOP.Domain.Framework.Logging;
using ONLINE_SHOP.Domain.Framework.Repositories;
using ONLINE_SHOP.Domain.Framework.Services.Requests;
using Microsoft.AspNetCore.Http;

namespace ONLINE_SHOP.Domain.Framework.Models;

public class CommandModelBase : ICommandModel
{
    private readonly ICommandRepositoryBase _repository;
    private readonly IMessagePublisher _publisher;
    public ILog Logger { get; }

    public CommandModelBase(ICommandRepositoryBase repository, IMessagePublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
        Logger = LogManager.GetLogger<CommandModelBase>();
    }

    protected void PublishToBus(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
            PublishToBus(new BusEvent
            {
                ExchangeName = @event.ExchangeName,
                Routes = @event.Routes,
                EventType = @event.GetType().ToString(),
                Payload = @event.Serialize(),
                Retry = 1
            });
    }

    protected void PublishToBus(BusEvent @event)
        => _publisher.Publish(@event);

    protected async Task HandleTransactionAsync(CommandBase command, GuidAuditableAggregateRoot aggregateRoot, CancellationToken cancellationToken)
        => await HandleTransactionAsync(command, new[] {aggregateRoot}, cancellationToken);

    protected async Task HandleTransactionAsync(CommandBase command, IEnumerable<GuidAuditableAggregateRoot> aggregateRoots, CancellationToken cancellationToken)
    {
        switch (command.DbOrder)
        {
            case TransactionOrders.SingleUpdate:
                await _repository.SaveChangesAsync(cancellationToken);
                PublishToBus(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.SingleTransaction:
                command.Transaction = await _repository.StartTransAsync(cancellationToken: cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
                await command.Transaction.CommitAsync(cancellationToken);

                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                PublishToBus(command.BusEvents);
                break;

            case TransactionOrders.StartTransaction:
                command.Transaction = await _repository.StartTransAsync(cancellationToken: cancellationToken);

                await _repository.SaveChangesAsync(cancellationToken);
                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.ParticipateInTransaction:
                await _repository.SaveChangesAsync(cancellationToken);
                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.CommitTransaction:
                await _repository.SaveChangesAsync(cancellationToken);
                await command.Transaction.CommitAsync(cancellationToken);

                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                PublishToBus(command.BusEvents);
                break;

            case TransactionOrders.RollbackTransaction:
                await command.Transaction.RollbackAsync(cancellationToken);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}