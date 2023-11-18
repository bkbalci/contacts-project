using ContactProject.Core.ReportModels;
using ContactService.Domain.Core;
using ContactService.Domain.Entities;

namespace ContactService.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<List<UserLocationReport>> GetReport();
}