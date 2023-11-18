using ReportService.Domain.Enums;

namespace ReportService.Domain.Dto;

public class ReportDto
{
    
    public Guid UUID { get; set; }

    public DateTime RequestDate { get; set; }

    public ReportStatus ReportStatus { get; set; }
}