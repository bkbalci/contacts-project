namespace ContactService.Domain.Dto;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string CompanyName { get; set; }
}