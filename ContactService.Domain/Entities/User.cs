using ContactService.Domain.Core;

namespace ContactService.Domain.Entities;

public class User : IEntity
{
    public Guid UUID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }

    public virtual ICollection<UserContactInfo>? ContactInfos { get; set; }
}