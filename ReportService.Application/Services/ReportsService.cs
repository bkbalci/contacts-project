using ContactService.Domain.Dto;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ReportService.Domain.Dto;
using ReportService.Domain.Entities;
using ReportService.Domain.Models;

namespace ReportService.Application.Services;

public class ReportsService
{
    private readonly IMongoCollection<Report> _reportsCollection;

    public ReportsService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(
            mongoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbSettings.Value.DatabaseName);

        _reportsCollection = mongoDatabase.GetCollection<Report>(
            mongoDbSettings.Value.ReportsCollectionName);
    }

    public async Task<ApiResponse<List<Report>>> GetAsync()
    {
        var reports = await _reportsCollection.Find(_ => true).ToListAsync();
        return ApiResponse<List<Report>>.Success(200, reports);
    }

    public async Task<ApiResponse<Report?>> GetAsync(Guid uuid)
    {
        var report = await _reportsCollection.Find(x => x.UUID == uuid).FirstOrDefaultAsync();
        return report != null ? ApiResponse<Report?>.Success(200, report) : ApiResponse<Report?>.Fail(404, "Not found");
    }
    
    public async Task<ApiResponse<List<ReportDto>>> GetMappedAsync()
    {
        var reports = await _reportsCollection.Find(_ => true).ToListAsync();
        return ApiResponse<List<ReportDto>>.Success(200, reports.Adapt<List<ReportDto>>());
    }

    public async Task<ApiResponse<ReportDto?>> GetMappedAsync(Guid uuid)
    {
        var report = await _reportsCollection.Find(x => x.UUID == uuid).FirstOrDefaultAsync();
        return report != null ? ApiResponse<ReportDto?>.Success(200, report.Adapt<ReportDto>()) : ApiResponse<ReportDto?>.Fail(404, "Not found");
    }

    public async Task<ApiResponse<Report?>> GetAsync(string uuid)
    {
        if (Guid.TryParse(uuid, out var _guid))
        {
            return await GetAsync(_guid);
        }
        else
        {
            return ApiResponse<Report?>.Fail(500, "Invalid GUID!");
        }
    }
    public async Task<ApiResponse<Report>> CreateAsync(Report report)
    {
        await _reportsCollection.InsertOneAsync(report);
        return ApiResponse<Report>.Success(201, report);
    }

    public async Task<ApiResponse<NoContent>> UpdateAsync(Guid uuid, Report report)
    {
        var result = await _reportsCollection.ReplaceOneAsync(x => x.UUID == uuid, report);
        return result.IsAcknowledged ? ApiResponse<NoContent>.Success(201) : ApiResponse<NoContent>.Fail(500, "An error occured.");
    }
}