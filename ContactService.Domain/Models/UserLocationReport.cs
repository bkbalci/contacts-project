namespace ContactService.Domain.Models;

public class UserLocationReport
{
    public string Location { get; set; }
    public int UserCount { get; set; }
    public int PhoneCount { get; set; }
}