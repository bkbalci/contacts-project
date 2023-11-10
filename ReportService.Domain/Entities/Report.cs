using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReportService.Domain.Enums;

namespace ReportService.Domain.Entities;

public class Report
{
    [BsonId]
    public Guid UUID { get; set; }

    public DateTime RequestDate { get; set; }

    public ReportStatus ReportStatus { get; set; }

    public BsonDocument Content { get; set; }
}