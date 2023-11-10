using ContactProject.Core.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Services;
using ReportService.Domain.Entities;
using ReportService.Domain.Enums;

namespace ReportService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : BaseController
{
    private readonly ReportsService _reportsService;
    private readonly QueueService _queueService;

    public ReportController(ReportsService reportsService, QueueService queueService)
    {
        _reportsService = reportsService;
        _queueService = queueService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var response = await _reportsService.CreateAsync(new Report()
        {
            ReportStatus = ReportStatus.Preparing,
            RequestDate = DateTime.Now
        });
        _queueService.Send(response.Data.UUID);
        return CreateActionResult(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _reportsService.GetAsync();
        return CreateActionResult(response);
    }
    
    [HttpGet("{uuid}")]
    public async Task<IActionResult> Get(Guid uuid)
    {
        var response = await _reportsService.GetAsync(uuid);
        return CreateActionResult(response);
    }
}