using System.Text;
using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.Extensions;
using ONLINE_SHOP.Domain.Framework.Logging;
using RabbitMQ.Client;

namespace ONLINE_SHOP.Infrastructure.Framework.Events;

public class RabbitMqPublisher : IMessagePublisher
{
    public ILog Logger { get; }
    private readonly IConnection _connection;

    public RabbitMqPublisher()
    {
        // var factory = new ConnectionFactory
        // {
        //     HostName = GlobalConfig.Config["RabbitMq:HostName"],
        //     Port = int.Parse(GlobalConfig.Config["RabbitMq:Port"]),
        //     UserName = GlobalConfig.Config["RabbitMq:UserName"],
        //     Password = GlobalConfig.Config["RabbitMq:Password"]
        // };

        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        _connection = factory.CreateConnection();

        Logger = LogManager.GetLogger<RabbitMqPublisher>();
    }

    public void Publish(BusEvent domainEvent)
    {
        var payload = new Dictionary<string, string>();
        using (var channel = _connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: domainEvent.ExchangeName, ExchangeType.Direct);
            channel.ExchangeDeclare(exchange: domainEvent.ExchangeName + "_BC", ExchangeType.Fanout);
            payload.Add("ExchangeName", domainEvent.ExchangeName);
            payload.Add("ExchangeNameBc", domainEvent.ExchangeName + "_BC");
            if (domainEvent.Routes is {Length: > 0})
                foreach (var project in domainEvent.Routes)
                {
                    var busName = project.ToUpper() + "__" + domainEvent.ExchangeName;
                    channel.QueueDeclare(busName, true, false, autoDelete: false, arguments: null);
                    payload.Add("QueueName", busName);
                    channel.QueueBind(queue: busName, exchange: domainEvent.ExchangeName, routingKey: busName);
                    channel.QueueBind(queue: busName, exchange: domainEvent.ExchangeName + "_BC", routingKey: busName);
                    payload.Add("BindName", domainEvent.ExchangeName);
                    payload.Add("BindNameBc", domainEvent.ExchangeName + "_BC");
                }

            Logger.Info($"New exchanges, queue and bindings created. {payload}, event-management, rabbitmq-handling");
            Logger.Info(
                $"New event is going to be published.EventType= {domainEvent.EventType},ExchangeName= {domainEvent.ExchangeName},Routes= {domainEvent.Routes.Serialize()}, event-management, rabbitmq-messages");
            
            if (domainEvent.Routes is {Length: 0})
                channel.BasicPublish(domainEvent.ExchangeName + "_BC", string.Empty, null, Encoding.UTF8.GetBytes(domainEvent.Serialize()));
            else
                foreach (var route in domainEvent.Routes)
                    channel.BasicPublish(domainEvent.ExchangeName, route.ToUpper() + "__" + domainEvent.ExchangeName, null,
                        Encoding.UTF8.GetBytes(domainEvent.Serialize()));
        }
    }
}