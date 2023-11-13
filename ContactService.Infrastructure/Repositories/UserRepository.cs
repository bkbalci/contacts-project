using ContactService.Domain.Entities;
using ContactService.Domain.Enums;
using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbContext _dbContext;
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserLocationReport>> GetReport()
    {
        var query = _dbContext.Set<UserContactInfo>()
            .Include(x => x.User)
            .Where(x => x.ContactType == ContactType.Location)
            .GroupBy(x => x.ContactTypeValue)
            .Select(x => new UserLocationReport
            {
                Location = x.Key,
                UserCount = x.Select(y => y.UserId).Distinct().Count(),
                PhoneCount = _dbContext.Set<UserContactInfo>().Count(y =>
                    x.Select(z => z.UserId).Contains(y.UserId) && y.ContactType == ContactType.Phone)
            })
            .AsNoTracking();
        return await query.ToListAsync();
    }
}