using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ReportService.Domain.Models;

namespace ReportService.Application.Services;

public class QueueService
{
    private readonly RabbitMqSettings _rabbitMqSettings;
    public string QueueName { get; private set; }
    public QueueService(IOptions<RabbitMqSettings> options)
    {
        _rabbitMqSettings = options.Value;
        QueueName = _rabbitMqSettings.QueueName;
    }

    public ConnectionFactory GetConnectionFactory()
    {
        return new ConnectionFactory()
        {
            UserName = _rabbitMqSettings.Username,
            Password = _rabbitMqSettings.Password,
            HostName = _rabbitMqSettings.HostName
        };
    }
    public IConnection CreateChannel()
    {
        var connection = GetConnectionFactory();
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
    public void Send(Guid reportGuid)
    {
        var factory = GetConnectionFactory();
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _rabbitMqSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = reportGuid.ToString();
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: _rabbitMqSettings.QueueName,
            basicProperties: null,
            body: body);
    }
}