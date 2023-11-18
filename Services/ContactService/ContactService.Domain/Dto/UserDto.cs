using System.Text.Json.Serialization;

namespace ContactService.Domain.Dto;

public class UserDto
{
    public Guid UUID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<UserContactInfoDto>? ContactInfos { get; set; }
}