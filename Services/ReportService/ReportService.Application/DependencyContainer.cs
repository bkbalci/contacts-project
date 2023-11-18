using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using ReportService.Application.Services;
using ReportService.Domain.Models;

namespace ReportService.Application;

public static class DependencyContainer
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
        services.AddSingleton<ReportsService>();
        services.AddSingleton<QueueService>();
        
        var objectSerializer = new ObjectSerializer(type => true); 
        BsonSerializer.RegisterSerializer(objectSerializer);
        
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}