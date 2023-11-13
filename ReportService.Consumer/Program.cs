using ReportService.Application;
using ReportService.Consumer;
using DependencyContainer = ContactService.Application.DependencyContainer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.AddApplication(configuration);
        DependencyContainer.AddApplication(services, configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
