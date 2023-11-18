using ContactService.Domain.Enums;

namespace ContactService.Domain.Dto;

public class UserContactInfoDto
{
    public ContactType ContactType { get; set; }
    public string ContactTypeValue { get; set; }
    public Guid UserId { get; set; }
}