using ContactService.Domain.Enums;

namespace ContactService.Domain.Dto;

public class AddContactInfoDto
{
    public Guid UserId { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactTypeValue { get; set; }
}