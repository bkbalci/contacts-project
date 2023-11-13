using System.Text.Json;
using ContactService.Application.Services;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Application.Services;
using ReportService.Domain.Enums;

namespace ReportService.Consumer;

public class Worker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, QueueService queueService, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _queueName = queueService.QueueName;
        _connection = queueService.CreateChannel();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body;
            var reportGuid = System.Text.Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"Received message: {reportGuid}");
            using (var scope = _serviceProvider.CreateScope())
            {
                var reportsService = scope.ServiceProvider.GetRequiredService<ReportsService>();
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                var reportRequestResult = await reportsService.GetAsync(reportGuid);
                if (reportRequestResult.IsSuccessful)
                {
                    Console.WriteLine($"Report found: {reportGuid}");
                    var reportRequest = reportRequestResult.Data;
                    if (reportRequest.ReportStatus != ReportStatus.Completed)
                    {
                        Console.WriteLine($"Report is being generated: {reportGuid}");
                        var reportResult = await userService.GetReport();
                        if (reportResult.IsSuccessful)
                        {
                            Console.WriteLine($"Report is generated: {reportGuid}");
                            var report = reportResult.Data;
                            reportRequest.ReportStatus = ReportStatus.Completed;
                            reportRequest.Content = report.Select(x => x.ToBsonDocument()).ToList();
                            Console.WriteLine($"Updating report status: {reportGuid}");
                            var result = await reportsService.UpdateAsync(reportRequest.UUID, reportRequest);
                            Console.WriteLine($"Report status is updated: {reportGuid}");
                            if (result.IsSuccessful)
                            {
                                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                                Console.WriteLine($"Acknowledged: {reportGuid}");
                            }
                        }
                    }
                }

            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    await Task.Delay(1000, stoppingToken);
        //}
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        await base.StopAsync(cancellationToken);
    }
}
