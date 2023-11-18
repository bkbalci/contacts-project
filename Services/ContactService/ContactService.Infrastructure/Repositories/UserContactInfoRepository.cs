using ContactService.Domain.Entities;
using ContactService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Repositories;

public class UserContactInfoRepository : BaseRepository<UserContactInfo>, IUserContactInfoRepository
{
    public UserContactInfoRepository(DbContext dbContext) : base(dbContext)
    {
    }
}