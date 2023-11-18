namespace ReportService.Domain.Models;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ReportsCollectionName { get; set; } = null!;
}