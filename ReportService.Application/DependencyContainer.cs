using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Application.Services;

namespace ReportService.Application;

public static class DependencyContainer
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ReportsService>();
        services.AddSingleton<QueueService>();
    }
}