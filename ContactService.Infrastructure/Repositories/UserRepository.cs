using ContactService.Domain.Entities;
using ContactService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}