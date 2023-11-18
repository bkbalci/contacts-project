using ContactService.Domain.Core;
using ContactService.Domain.Enums;

namespace ContactService.Domain.Entities;

public class UserContactInfo : IEntity
{
    public int Id { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactTypeValue { get; set; }
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }
}