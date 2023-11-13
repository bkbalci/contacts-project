using ContactService.Domain.Core;
using ContactService.Domain.Entities;
using ContactService.Domain.Models;

namespace ContactService.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<List<UserLocationReport>> GetReport();
}