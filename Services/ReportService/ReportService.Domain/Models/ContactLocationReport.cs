﻿namespace ReportService.Domain.Models;

public class ContactLocationReport
{
    public string Location { get; set; }
    public int ContactCount { get; set; }
    public int PhoneCount { get; set; }
}